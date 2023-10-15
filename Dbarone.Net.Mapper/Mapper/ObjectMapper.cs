namespace Dbarone.Net.Mapper;
using Dbarone.Net.Extensions;
using System.Linq.Expressions;

/// <summary>
/// The ObjectMapper class provides mapping functions to transform objects from one type to another. 
/// </summary>
public class ObjectMapper
{
    private MapperConfiguration Configuration { get; set; }
    private MapperBuilder Builder { get; set; }

    internal ObjectMapper(MapperConfiguration configuration)
    {
        this.Configuration = configuration;
        this.Builder = new MapperBuilder(this.Configuration);
    }

    /// <summary>
    /// Maps / transforms an object from one type to another.
    /// </summary>
    /// <param name="fromType">The type to transform the object from.</param>
    /// <param name="toType">The type to transform the object to.</param>
    /// <param name="obj">The object being transformed from. Must be assignable to `fromType`.</param>
    /// <returns>Returns a mapped object of type `toType`.</returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="MapperException"></exception>
    public object MapOne(Type fromType, Type toType, object? obj)
    {


        MapperTypeConfiguration fromTypeConfiguration = configuration[fromType];
        MapperTypeConfiguration toTypeConfiguration = configuration[toType];

        var newInstance = toTypeConfiguration.CreateInstance();

        // for dictionary + dynamic types, we need to get the source members now
        if (fromTypeConfiguration.MemberResolver.DeferMemberResolution)
        {
            var members = fromTypeConfiguration.MemberResolver.GetInstanceMembers(obj);
            var memberConfig = members.Select(m => new MapperMemberConfiguration
            {
                MemberName = m,
                DataType = fromTypeConfiguration.MemberResolver.GetMemberType(m),
                Getter = fromTypeConfiguration.MemberResolver.GetGetter(m),
                Setter = fromTypeConfiguration.MemberResolver.GetSetter(m)
            }).ToList();
            fromTypeConfiguration.MemberConfiguration = memberConfig;
            foreach (var item in fromTypeConfiguration.MemberConfiguration)
            {
                item.SetInternalMemberName(fromTypeConfiguration.Options.MemberRenameStrategy);
            }
            fromTypeConfiguration.Validate();
        }

        // Get members to map. By default, the members come from the 'to' side.
        // However, if the to type is deferred resolution, then the members come from the 'from' side.
        List<string> internalMemberNames = null;
        if (!toTypeConfiguration.MemberResolver.DeferMemberResolution)
        {
            internalMemberNames = toTypeConfiguration.MemberConfiguration.Where(r => r.Ignore == false).Select(m => m.InternalMemberName).ToList();
        }
        else
        {
            internalMemberNames = fromTypeConfiguration.MemberConfiguration.Where(r => r.Ignore == false).Select(m => m.InternalMemberName).ToList();
        }

        // If to type is dictionary / dynamic (DeferMemberResolution = true)
        foreach (var internalMemberName in internalMemberNames)
        {
            // Get from rule
            var toRule = toTypeConfiguration.MemberConfiguration.FirstOrDefault(mc => mc.InternalMemberName.Equals(internalMemberName, StringComparison.CurrentCultureIgnoreCase));
            var fromRule = fromTypeConfiguration.MemberConfiguration.FirstOrDefault(mc => mc.InternalMemberName.Equals(internalMemberName, StringComparison.CurrentCultureIgnoreCase));

            if (fromRule == null && (toTypeConfiguration.Options.EndPointValidation & MapperEndPoint.Destination) == MapperEndPoint.Destination)
            {
                throw new Exception($"Cannot find member: {toRule.InternalMemberName} in source mapping configuration.");
            }
            var fromObj = fromRule.Getter.Invoke(obj!);

            // Get type of from member:
            if (toRule.DataType != fromRule.DataType)
            {
                // Check for custom type converter:
                var validConverterKey = this.customTypeConverters.Keys.FirstOrDefault(k => k.Item1 == fromRule.DataType && k.Item2 == toRule.DataType);
                if (validConverterKey != null)
                {
                    ITypeConverter typeConverter = this.customTypeConverters[validConverterKey];
                    var convertedObject = typeConverter.Convert(fromObj);
                    toRule.Setter.Invoke(newInstance, convertedObject!);
                }
                else
                {
                    throw new MapperException($"Cannot map from type: {fromRule.DataType} to type: {toRule.DataType}. Are you missing a Type Converter?");
                }
            }


        }
        return newInstance;
    }

    /// <summary>
    /// Maps / transforms an object from one type to another.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="obj">Returns an object of the destination type.</param>
    /// <returns></returns>
    public TDestination? MapOne<TSource, TDestination>(TSource? obj)
    {
        return (TDestination?)MapOne(typeof(TSource), typeof(TDestination), obj);
    }

    /// <summary>
    /// Maps a collection, list or array of items.
    /// </summary>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <typeparam name="TDestination">The type of the destination object.</typeparam>
    /// <param name="obj">The source object. Must be an enumerable, collection, list or array of type U.</param>
    /// <returns></returns>
    public IEnumerable<TDestination?> MapMany<TSource, TDestination>(IEnumerable<TSource?> obj)
    {
        foreach (var item in obj)
        {
            yield return MapOne<TSource, TDestination>(item);
        }
    }

}