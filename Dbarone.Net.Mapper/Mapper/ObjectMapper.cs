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

    #region Callbacks

    /// <summary>
    /// Event raised whenever a new mapper operator is built.
    /// </summary>

    private CreateOperatorDelegate? _onCreateOperator;

    public CreateOperatorDelegate? OnCreateOperator
    {
        get
        {
            return _onCreateOperator;
        }
        set
        {
            _onCreateOperator = value;
            this.Builder.OnCreateOperator = value;
        }
    }

    #endregion


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

    /// <summary>
    /// Gets the mapping execution plan.
    /// </summary>
    /// <param name="from">The from type.</param>
    /// <param name="to">The to type.</param>
    /// <returns>Returns the mapping execution plan.</returns>
    public MapperOperator GetOperator(Type from, Type to)
    {
        SourceDestination sourceDestination = new SourceDestination(from, to);
        var mapper = Builder.GetMapper(sourceDestination);
        return mapper;
    }

    /// <summary>
    /// Gets the mapping execution plan.
    /// </summary>
    /// <typeparam name="TSource">The from type.</typeparam>
    /// <typeparam name="TDestination">The to type.</typeparam>
    /// <returns>Returns the mapping execution plan.</returns>
    public MapperOperator GetOperator<TSource, TDestination>()
    {
        return GetOperator(typeof(TSource), typeof(TDestination));
    }
}