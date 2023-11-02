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

    public ObjectMapper(MapperConfiguration configuration)
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
    public object? Map(Type fromType, Type toType, object? obj)
    {
        Builder.Build(new SourceDestination(fromType, toType));
        Dictionary<SourceDestinationPath, SourceDestinationPathRules> dynamicMapRules = new Dictionary<SourceDestinationPath, SourceDestinationPathRules>();
        return MapInternal(new SourceDestination(fromType, toType), fromType, toType, obj, "", dynamicMapRules);
    }

    private object? MapInternal(SourceDestination sourceDestination, Type fromType, Type toType, object? obj, string path, IDictionary<SourceDestinationPath, SourceDestinationPathRules> dynamicMapRules)
    {
        var toBuildType = this.Builder.GetBuildTypeFor(toType);
        var fromBuildType = this.Builder.GetBuildTypeFor(fromType);
        var newInstance = this.Builder.GetCreatorFor(toType).Invoke(new object[] { });
        var errors = new List<MapperBuildError>();

        // for dictionary + dynamic types, we need to get the source members now
        if (fromBuildType.MemberResolver.DeferMemberResolution)
        {
            Builder.AddDynamicMembers(fromType, path, obj, errors);
            //Builder.BuildMapRules(sourceDestination, fromBuildType, toBuildType, path, errors);
        }

        // validate
        if (errors.Any())
        {
            throw new MapperBuildException($"Error occurred building dynamic type: {fromBuildType.Type.Name}. Check Errors collection for more information.", errors);
        }

        // Get mapper
        var mapper = Builder.GetMapperFor(sourceDestination);
        var to = mapper(obj, null);
        return to;
    }

    /// <summary>
    /// Maps / transforms an object from one type to another.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="obj">Returns an object of the destination type.</param>
    /// <returns></returns>
    public TDestination? Map<TSource, TDestination>(TSource? obj)
    {
        return (TDestination?)Map(typeof(TSource), typeof(TDestination), obj);
    }
}