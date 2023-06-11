namespace Dbarone.Net.Mapper;

/// <summary>
/// 
/// </summary>
public class MapperConfiguration
{
    private IDictionary<Type, MapperTypeConfiguration> TypeConfiguration { get; set; } = new Dictionary<Type, MapperTypeConfiguration>();
    private IDictionary<Tuple<Type, Type>, Tuple<MapperTypeConfiguration, MapperTypeConfiguration>> MapConfiguration { get; set; } = new Dictionary<Tuple<Type, Type>, Tuple<MapperTypeConfiguration, MapperTypeConfiguration>>();

    private IList<MapperBuilder<Type, Type>> ObjectMappers = new List<MapperBuilder<Type, Type>>();

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

    /// <summary>
    /// Registers a single type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="options"></param>
    public MapperConfiguration RegisterType(Type type, MapperOptions options)
    {
        TypeConfiguration[type] = new MapperTypeConfiguration
        {
            Type = type,
            Options = options
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
}