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
        SourceTarget sourceTarget = new SourceTarget(fromType, toType);
        var mapperOperator = Builder.GetMapperOperator(sourceTarget);
        var to = mapperOperator.Map(obj, null);
        return to;
    }

    /// <summary>
    /// Maps / transforms an object from one type to another.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="obj">Returns an object of the target type.</param>
    /// <returns></returns>
    public TTarget? Map<TSource, TTarget>(TSource? obj)
    {
        return (TTarget?)Map(typeof(TSource), typeof(TTarget), obj);
    }

    /// <summary>
    /// Gets the mapping execution plan.
    /// </summary>
    /// <param name="from">The from type.</param>
    /// <param name="to">The to type.</param>
    /// <returns>Returns the mapping execution plan.</returns>
    public MapperOperator GetMapperOperator(Type from, Type to)
    {
        SourceTarget sourceTarget = new SourceTarget(from, to);
        var mapperOperator = Builder.GetMapperOperator(sourceTarget);
        return mapperOperator;
    }

    /// <summary>
    /// Gets the mapping execution plan.
    /// </summary>
    /// <typeparam name="TSource">The from type.</typeparam>
    /// <typeparam name="TTarget">The to type.</typeparam>
    /// <returns>Returns the mapping execution plan.</returns>
    public MapperOperator GetMapperOperator<TSource, TTarget>()
    {
        return GetMapperOperator(typeof(TSource), typeof(TTarget));
    }
}