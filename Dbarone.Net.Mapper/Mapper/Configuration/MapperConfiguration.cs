namespace Dbarone.Net.Mapper;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;
using System.Reflection;

/// <summary>
/// 
/// </summary>
public class MapperConfiguration
{
    private IDictionary<Type, MapperTypeConfiguration> TypeConfiguration { get; set; } = new Dictionary<Type, MapperTypeConfiguration>();
    private IDictionary<Tuple<Type, Type>, Tuple<MapperTypeConfiguration, MapperTypeConfiguration>> MapConfiguration { get; set; } = new Dictionary<Tuple<Type, Type>, Tuple<MapperTypeConfiguration, MapperTypeConfiguration>>();

    internal MapperConfiguration() { }

    /// <summary>
    /// Creates a new MapperConfiguration instance.
    /// </summary>
    /// <returns></returns>
    public static MapperConfiguration Create()
    {
        return new MapperConfiguration();
    }

    /// <summary>
    /// Registers a collection of types.
    /// </summary>
    /// <param name="types"></param>
    /// <param name="options"></param>
    public MapperConfiguration RegisterTypes(Type[] types, MapperOptions options)
    {
        foreach (var type in types)
        {
            TypeConfiguration[type] = new MapperTypeConfiguration
            {
                Type = type,
                Options = options
            };
        }
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
    /// Registers a single type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="options"></param>
    public MapperConfiguration RegisterType(Type type, MapperOptions options)
    {
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
            MemberConfiguration = members.Select(m => new MapperMemberConfiguration
            {
                MemberName = m.Name,
                DataType = m.MemberType==MemberTypes.Property ? (m as PropertyInfo)!.PropertyType : (m as FieldInfo)!.FieldType,
                Getter = memberResolver.GetGetter(type, m.Name),
                Setter = memberResolver.GetSetter(type, m.Name)
            })
        };
        return this;
    }

    /// <summary>
    /// Registers a single type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="options"></param>
    public MapperConfiguration RegisterType<T>(MapperOptions options)
    {
        TypeConfiguration[typeof(T)] = new MapperTypeConfiguration
        {
            Type = typeof(T),
            Options = options
        };
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

    public MapperConfiguration RegisterMap<T, U>(MapperOptions sourceOptions, MapperOptions destinationOptions)
    {
        RegisterMap(typeof(T), sourceOptions, typeof(U), destinationOptions);
        return this;
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
    /// Builds a configured object mapper.
    /// </summary>
    /// <returns></returns>
    public ObjectMapper<object, object> Build()
    {
        return null;
    }
}