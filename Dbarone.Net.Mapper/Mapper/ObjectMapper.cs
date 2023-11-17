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
    public object? Map(Type fromType, Type toType, object? obj)
    {
        SourceDestination sourceDestination = new SourceDestination(fromType, toType);
        var mapper = Builder.GetMapper(sourceDestination);
        var to = mapper.GetMap()(obj, null);
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