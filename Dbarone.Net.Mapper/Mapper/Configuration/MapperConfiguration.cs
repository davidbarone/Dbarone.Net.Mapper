namespace Dbarone.Net.Mapper;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;
using System.Reflection;

/// <summary>
/// Creates configuration for a <see cref="ObjectMapper" /> mapper object.
/// </summary>
public class MapperConfiguration
{
    private IDictionary<Type, MapperTypeConfiguration> TypeConfiguration { get; set; } = new Dictionary<Type, MapperTypeConfiguration>();
    private IDictionary<Tuple<Type, Type>, Tuple<MapperTypeConfiguration, MapperTypeConfiguration>> MapConfiguration { get; set; } = new Dictionary<Tuple<Type, Type>, Tuple<MapperTypeConfiguration, MapperTypeConfiguration>>();

    private IDictionary<Tuple<Type, Type>, ITypeConverter> Converters { get; set; } = new Dictionary<Tuple<Type, Type>, ITypeConverter>();

    internal MapperConfiguration() { }

    /// <summary>
    /// Returns the number of types configured.
    /// </summary>
    /// <returns>Returns the number of types configured.</returns>
    public int GetTypeConfigurationCount()
    {
        return TypeConfiguration.Keys.Count();
    }

    /// <summary>
    /// Gets the <see cref="MapperTypeConfiguration" /> configuration for a specific type.
    /// </summary>
    /// <param name="type">The type to get the configuration for.</param>
    /// <returns>Returns a <see cref="MapperTypeConfiguration" /> object representing the specified configuration.</returns>
    public MapperTypeConfiguration GetTypeConfiguration(Type type)
    {
        return TypeConfiguration[type];
    }

    /// <summary>
    /// Creates a new <see cref="MapperConfiguration" /> instance.
    /// </summary>
    /// <returns>returns a new <see cref="MapperConfiguration" /> instance.</returns>
    public static MapperConfiguration Create()
    {
        return new MapperConfiguration();
    }

    /// <summary>
    /// Adds a type converter. Type converters are used to convert simple / native types where the members in the
    /// source and destinations have different types.
    /// </summary>
    /// <typeparam name="T">The type of the source member.</typeparam>
    /// <typeparam name="U">The type of the destination member.</typeparam>
    /// <param name="converter">A converter func.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterConverter<T, U>(Func<T, U> converter)
    {
        TypeConverter<T, U> typeConverter = new TypeConverter<T, U>(converter);
        Tuple<Type, Type> k = new Tuple<Type, Type>(typeof(T), typeof(U));
        this.Converters[k] = typeConverter;
        return this;
    }

    /// <summary>
    /// Registers a collection of types.
    /// </summary>
    /// <param name="types">An array of Types.</param>
    /// <param name="options">An optional <see cref="MapperOptions" /> object containing configuration details for all the types to be registered.</param>
    public MapperConfiguration RegisterTypes(Type[] types, MapperOptions? options = null)
    {
        foreach (var type in types)
        {
            RegisterType(type, options);
        }
        return this;
    }

    /// <summary>
    /// Registers a type using generic types.
    /// </summary>
    /// <typeparam name="T">The type of the entity to register.</typeparam>
    /// <param name="options">An optional <see cref="MapperOptions" /> object containing configuration details for the type being registered.</param>
    /// <example>
    /// The following example shows how to register a type:
    /// <code>
    /// // Configure mapper
    /// var mapper = MapperConfiguration
    ///     .Create()
    ///     .RegisterType&lt;CustomerEntity&gt;()
    ///     .RegisterType&lt;CustomerModel&gt;()
    ///     .Build();
    ///     
    /// // Map object
    /// var obj2 = mapper.MapOne&lt;CustomerEntity, CustomerModel&gt;(obj1); 
    /// </code>
    /// </example>
    /// <returns></returns>
    public MapperConfiguration RegisterType<T>(MapperOptions? options = null)
    {
        return RegisterType(typeof(T), options);
    }

    /// <summary>
    /// Registers a single type.
    /// </summary>
    /// <param name="type">The type to be registered.</param>
    /// <param name="options">An optional <see cref="MapperOptions" /> object containing configuration details for the type being registered.</param>
    public MapperConfiguration RegisterType(Type type, MapperOptions? options = null)
    {
        if (options == null)
        {
            options = new MapperOptions();
        }

        // Check whether type is a normal class or a dictionary type.
        var isDict = type.IsDictionaryType();
        IMemberResolver memberResolver = new ClassMemberResolver();
        if (isDict)
        {
            memberResolver = new DictionaryMemberResolver();
        }

        var members = GetMembers(type, options);

        TypeConfiguration[type] = new MapperTypeConfiguration
        {
            Type = type,
            Options = options,
            CreateInstance = memberResolver.CreateInstance(type, null),
            MemberConfiguration = members.Select(m => new MapperMemberConfiguration
            {
                MemberName = m.Name,
                DataType = m.MemberType == MemberTypes.Property ? (m as PropertyInfo)!.PropertyType : (m as FieldInfo)!.FieldType,
                Getter = memberResolver.GetGetter(type, m),
                Setter = memberResolver.GetSetter(type, m)
            }).ToList()
        };
        return this;
    }

    /// <summary>
    /// Registers a specific type-to-type configuration. When registering via <see cref="RegisterType" /> only 1 endpoint
    /// is specified, and the <see cref="ObjectMapper" /> automatically joins members based on member name. In cases
    /// where a specific mapping between 2 types is required, this method can be used to provide custom behaviour.
    /// </summary>
    /// <typeparam name="T">The source type to map.</typeparam>
    /// <typeparam name="U">The destination type to map.</typeparam>
    /// <param name="sourceOptions">An optional <see cref="MapperOptions" /> object containing configuration details for the source type being registered.</param>
    /// <param name="destinationOptions">An optional <see cref="MapperOptions" /> object containing configuration details for the destination type being registered.</param>
    /// <returns></returns>
    public MapperConfiguration RegisterMap<T, U>(MapperOptions sourceOptions, MapperOptions destinationOptions)
    {
        RegisterMap(typeof(T), sourceOptions, typeof(U), destinationOptions);
        return this;
    }

    public MapperConfiguration RegisterMap(Type sourceType, MapperOptions sourceOptions, Type destinationType, MapperOptions destinationOptions)
    {
        var key = new Tuple<Type, Type>(
            sourceType,
            destinationType
        );
        var value = new Tuple<MapperTypeConfiguration, MapperTypeConfiguration>(
            new MapperTypeConfiguration
            {
                Type = sourceType,
                Options = sourceOptions
            },
            new MapperTypeConfiguration
            {
                Type = destinationType,
                Options = destinationOptions
            }
        );
        return this;
    }



    private IEnumerable<MemberInfo> GetMembers(Type type, MapperOptions options)
    {
        BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
        if (options.IncludePrivateMembers)
        {
            bindingFlags |= BindingFlags.NonPublic;
        }
        var members = type.GetMembers(bindingFlags).Where(m => m.MemberType == MemberTypes.Property || (options.IncludeFields && m.MemberType == MemberTypes.Field));
        return members;
    }

    /// <summary>
    /// Defines a member that will not be mapped.
    /// </summary>
    public MapperConfiguration Ignore<T>(Expression<Func<T, object>> member)
    {
        var type = typeof(T);
        return this.ApplyMemberAction(member, (p) =>
        {
            p.Ignore = true;
        });
    }

    /// <summary>
    /// Defines a custom name for a member when mapping to other types.
    /// </summary>
    public MapperConfiguration Rename<T>(Expression<Func<T, object>> member, string newName)
    {
        if (newName.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(newName));

        return this.ApplyMemberAction(member, (p) =>
        {
            p.InternalMemberName = newName;
        });
    }

    /// <summary>
    /// Maps a member from source to destination using lambda expressions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="fromMember"></param>
    /// <param name="toMember"></param>
    /// <returns></returns>
    public MapperConfiguration MapMember<T, U>(Expression<Func<T, object>> fromMember, Expression<Func<U, object>> toMember)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Selects a member, then applies an action to the member mapping rule.
    /// </summary>
    private MapperConfiguration ApplyMemberAction<T>(Expression<Func<T, object>> member, Action<MapperMemberConfiguration> action)
    {
        var type = typeof(T);

        if (member == null) throw new ArgumentNullException(nameof(member));

        var memb = this.TypeConfiguration[type].GetMemberRule(member);

        if (memb == null)
        {
            throw new ArgumentNullException($"Member '{member.GetMemberPath()}' not found in type.");
        }

        action(memb);
        return this;
    }

    /// <summary>
    /// Takes all the configuration and builds an <see cref="ObjectMapper" /> object that can then be used to map objects.
    /// </summary>
    /// <returns>Returns a configured <see cref="ObjectMapper" /> object.</returns>
    public ObjectMapper Build()
    {
        // Set InternalMemberName
        foreach (var k in this.TypeConfiguration.Keys)
        {
            var v = this.TypeConfiguration[k];
            foreach (var item in v.MemberConfiguration)
            {
                if (item.InternalMemberName.IsNullOrWhiteSpace())
                {
                    item.InternalMemberName = v.Options.MemberNameTranslation.Invoke(item.MemberName);
                }
            }
        }
        return new ObjectMapper(this.TypeConfiguration, this.Converters);
    }
}