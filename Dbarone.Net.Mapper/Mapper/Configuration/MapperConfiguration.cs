namespace Dbarone.Net.Mapper;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;
using System.Reflection;

/// <summary>
/// Creates configuration for a <see cref="ObjectMapper" /> mapper object. Before being able to map any objects and types, you must create
/// a mapper configuration, and from this generate a <see cref="ObjectMapper" /> object.
/// </summary>
public class MapperConfiguration
{
    private IDictionary<Type, MapperTypeConfiguration> TypeConfiguration { get; set; } = new Dictionary<Type, MapperTypeConfiguration>();
    private IDictionary<Tuple<Type, Type>, Tuple<MapperTypeConfiguration, MapperTypeConfiguration>> MapConfiguration { get; set; } = new Dictionary<Tuple<Type, Type>, Tuple<MapperTypeConfiguration, MapperTypeConfiguration>>();
    private IDictionary<Tuple<Type, Type>, ITypeConverter> Converters { get; set; } = new Dictionary<Tuple<Type, Type>, ITypeConverter>();

    internal MapperConfiguration() { }

    #region Configuration

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

    #endregion

    #region ctor

    /// <summary>
    /// Creates a new <see cref="MapperConfiguration" /> instance.
    /// </summary>
    /// <returns>returns a new <see cref="MapperConfiguration" /> instance.</returns>
    public static MapperConfiguration Create()
    {
        return new MapperConfiguration();
    }

    #endregion

    #region Register Types

    /// <summary>
    /// Registers a collection of types.
    /// </summary>
    /// <param name="types">An array of Types.</param>
    /// <param name="options">An optional <see cref="MapperOptions" /> object containing configuration details for all the types to be registered.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
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
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterType<T>(MapperOptions? options = null)
    {
        return RegisterType(typeof(T), options);
    }

    /// <summary>
    /// Registers a single type.
    /// </summary>
    /// <param name="type">The type to be registered.</param>
    /// <param name="options">An optional <see cref="MapperOptions" /> object containing configuration details for the type being registered.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterType(Type type, MapperOptions? options = null)
    {
        if (options == null)
        {
            options = new MapperOptions();
        }

        IMemberResolver memberResolver;
        if (type.IsValueType) {
            memberResolver = new StructMemberResolver();
        }
        else if (type.IsDictionaryType())
        {
            memberResolver = new DictionaryMemberResolver();
        }
        else {
            memberResolver = new ClassMemberResolver();
        }

        var members = GetTypeMembers(type, options);

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

    #endregion

    #region Calculations

    /// <summary>
    /// Registers a calculation for a type.
    /// </summary>
    /// <typeparam name="TSource">The source entity type.</typeparam>
    /// <typeparam name="TReturn">The type of the return value for the calculated member.</typeparam>
    /// <param name="memberName">The calculated member name.</param>
    /// <param name="calculation">The calculation.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterCalculation<TSource, TReturn>(string memberName, Func<TSource?, TReturn?> calculation)
    {
        var type = typeof(TSource);
        var typeConfig = this.TypeConfiguration[type];
        MapperMemberConfiguration calc = new MapperMemberConfiguration()
        {
            MemberName = memberName,
            DataType = typeof(TReturn),
            InternalMemberName = memberName,
            Ignore = false,
            Getter = new TypeConverter<TSource, TReturn>(calculation).Convert,
            Setter = null,
            Calculation = new TypeConverter<TSource, TReturn>(calculation)
        };
        typeConfig.MemberConfiguration.Add(calc);
        return this;
    }

    #endregion

    #region Register Map

    /// <summary>
    /// Registers a specific type-to-type configuration. When registering via <see cref="RegisterType" /> only 1 endpoint
    /// is specified, and the <see cref="ObjectMapper" /> automatically joins members based on member name. In cases
    /// where a specific mapping between 2 types is required, this method can be used to provide custom behaviour.
    /// </summary>
    /// <typeparam name="TSource">The source type to map.</typeparam>
    /// <typeparam name="TDestination">The destination type to map.</typeparam>
    /// <param name="sourceOptions">An optional <see cref="MapperOptions" /> object containing configuration details for the source type being registered.</param>
    /// <param name="destinationOptions">An optional <see cref="MapperOptions" /> object containing configuration details for the destination type being registered.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterMap<TSource, TDestination>(MapperOptions sourceOptions, MapperOptions destinationOptions)
    {
        RegisterMap(typeof(TSource), sourceOptions, typeof(TDestination), destinationOptions);
        return this;
    }

    /// <summary>
    /// Registers a specific type-to-type configuration. When registering via <see cref="RegisterType" /> only 1 endpoint
    /// is specified, and the <see cref="ObjectMapper" /> automatically joins members based on member name. In cases
    /// where a specific mapping between 2 types is required, this method can be used to provide custom behaviour.
    /// </summary>
    /// <param name="sourceType">The source type participating in the map.</param>
    /// <param name="sourceOptions">The source options.</param>
    /// <param name="destinationType">The destination type participating in the map.</param>
    /// <param name="destinationOptions">The destination options.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
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

    #endregion 

    #region Ignore Members

    /// <summary>
    /// Defines a member that will not be mapped.
    /// </summary>
    /// <typeparam name="T">The source object type.</typeparam>
    /// <param name="member">A unary member expression to select a member on the source object type.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration Ignore<T>(Expression<Func<T, object>> member)
    {
        var type = typeof(T);
        return this.ApplyMemberAction(member, (p) =>
        {
            p.Ignore = true;
        });
    }

    /// <summary>
    /// Sets one or more members on a type to be ignored for mapping purposes.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="members">The list of members to ignore</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration Ignore(Type type, params string[] members)
    {
        var typeConfig = this.TypeConfiguration.First(c => c.Key == type).Value;
        foreach (var member in members)
        {
            var memberConfig = typeConfig.MemberConfiguration.First(m => m.MemberName.Equals(member, StringComparison.Ordinal));
            memberConfig.Ignore = true;
        }
        return this;
    }

    #endregion

    #region Converters

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

    #endregion

    #region Rename members

    /// <summary>
    /// Defines a custom name for a member when mapping to other types.
    /// </summary>
    /// <typeparam name="T">The source object type.</typeparam>
    /// <param name="member">A unary member expression to select a member on the source object type.</param>
    /// <param name="newName">The new name for the member.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public MapperConfiguration Rename<T>(Expression<Func<T, object>> member, string newName)
    {
        if (newName.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(newName));

        return this.ApplyMemberAction(member, (p) =>
        {
            p.InternalMemberName = newName;
        });
    }

    /// <summary>
    /// Defines a custom name for a member when mapping to other types.
    /// </summary>
    /// <param name="type">The type containing the member.</param>
    /// <param name="member">The member to rename.</param>
    /// <param name="newName">The new name for the member.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public MapperConfiguration Rename(Type type, string member, string newName)
    {
        var typeConfig = this.TypeConfiguration.First(c => c.Key == type).Value;
        var memberConfig = typeConfig.MemberConfiguration.First(m => m.MemberName.Equals(member, StringComparison.Ordinal));
        memberConfig.InternalMemberName = newName;
        return this;
    }

    #endregion

    #region Private Members`

    private IEnumerable<MemberInfo> GetTypeMembers(Type type, MapperOptions options)
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
    /// Maps a member from source to destination using lambda expressions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="fromMember"></param>
    /// <param name="toMember"></param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration MapMember<T, U>(Expression<Func<T, object>> fromMember, Expression<Func<U, object>> toMember)
    {
        throw new NotSupportedException();
    }

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
    /// Validates the mapper configuration
    /// </summary>
    public void Validate()
    {
        // ensure all mapping rules are connected

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
                // only modify internal name if not already pre-set.
                if (item.InternalMemberName.IsNullOrWhiteSpace())
                {
                    if (v.Options.MemberRenameStrategy != null)
                    {
                        var newName = v.Options.MemberRenameStrategy.RenameMember(item.MemberName);
                        item.InternalMemberName = newName;
                    }
                    else
                    {
                        // default - make internal name = member name
                        item.InternalMemberName = item.MemberName;
                    }
                }
            }

            // Check no duplicate internal names
            var duplicates = v.MemberConfiguration
                .GroupBy(g => g.InternalMemberName)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key).ToList();

            if (duplicates.Any())
            {
                var duplicateValues = duplicates.Aggregate("", (current, next) => current + " " + $"[{next}]");
                throw new MapperException($"The following internal member names have been used for multiple members on type: {v.Type}:{duplicateValues}.");
            }
        }

        return new ObjectMapper(this.TypeConfiguration, this.Converters);
    }

    #endregion
}