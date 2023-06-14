namespace Dbarone.Net.Mapper;
using Dbarone.Net.Extensions;

public class ObjectMapper
{
    private IDictionary<Type, MapperTypeConfiguration> configuration;
    internal ObjectMapper(IDictionary<Type, MapperTypeConfiguration> configuration)
    {
        this.configuration = configuration;
    }

    public object MapOne(Type fromType, Type toType, object obj)
    {
        MapperTypeConfiguration fromTypeConfiguration = configuration[fromType];
        MapperTypeConfiguration toTypeConfiguration = configuration[toType];

        var newInstance = toTypeConfiguration.CreateInstance();

        foreach (var toRule in toTypeConfiguration.MemberConfiguration.Where(r => r.Ignore == false))
        {
            // Get from rule
            var ruleName = toRule.InternalMemberName;
            var fromRule = fromTypeConfiguration.MemberConfiguration.FirstOrDefault(mc => mc.InternalMemberName.Equals(ruleName, StringComparison.CurrentCultureIgnoreCase));
            if (fromRule == null && (toTypeConfiguration.Options.AssertMapEndPoint & MapperEndPoint.Destination) == MapperEndPoint.Destination)
            {
                throw new Exception($"Cannot find member: {toRule.InternalMemberName} in source mapping configuration.");
            }
            var fromObj = fromRule!.Getter.Invoke(obj!);

            // Get type of from
            if (toRule.DataType.IsBuiltInType() && fromRule.DataType.IsBuiltInType())
                // built-in type? just set value on to object
                toRule.Setter.Invoke(newInstance, fromObj);
            else if (this.configuration.Keys.Contains(toRule.DataType) && this.configuration.Keys.Contains(fromRule.DataType))
            {
                // from / to are both objects that can be mapped
                var childObject = MapOne(fromRule.DataType, toRule.DataType, fromObj);
                toRule.Setter.Invoke(newInstance, childObject);
            }
        }
        return newInstance;
    }

    public U? MapOne<T, U>(T? obj)
    {
        return (U)MapOne(typeof(T), typeof(U), obj);
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