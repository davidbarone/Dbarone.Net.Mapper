namespace Dbarone.Net.Mapper;

public class ObjectMapper
{
    private IDictionary<Type, MapperTypeConfiguration> configuration;
    internal ObjectMapper(IDictionary<Type, MapperTypeConfiguration> configuration)
    {
        this.configuration = configuration;
    }

    public U? MapOne<T, U>(T? obj)
    {
        MapperTypeConfiguration fromType = configuration[typeof(T)];
        MapperTypeConfiguration toType = configuration[typeof(U)];

        var newInstance = toType.CreateInstance();

        foreach (var rule in toType.MemberConfiguration.Where(r => r.Ignore == false))
        {
            // Get from rule
            var ruleName = rule.InternalMemberName;
            var fromRule = fromType.MemberConfiguration.FirstOrDefault(mc => mc.InternalMemberName.Equals(ruleName, StringComparison.CurrentCultureIgnoreCase));
            if (fromRule == null && (toType.Options.AssertMapEndPoint & MapperEndPoint.Destination) == MapperEndPoint.Destination)
            {
                throw new Exception($"Cannot find member: {rule.InternalMemberName} in source mapping configuration.");
            }
            var fromObj = fromRule!.Getter.Invoke(obj!);
            rule.Setter.Invoke(newInstance, fromObj);
        }
        return (U)newInstance;
    }

    /// <summary>
    /// Maps a collection, list or array of items.
    /// </summary>
    /// <typeparam name="T">The type of the source object.</typeparam>
    /// <typeparam name="U">The type of the destination object.</typeparam>
    /// <param name="obj">The source object. Must be an enumerable, collection, list or array of type U.</param>
    /// <returns></returns>
    public IEnumerable<U?> MapMany<T, U>(IEnumerable<T?> obj)
    {
        foreach (var item in obj)
        {
            yield return MapOne<T, U>(item);
        }

    }

}