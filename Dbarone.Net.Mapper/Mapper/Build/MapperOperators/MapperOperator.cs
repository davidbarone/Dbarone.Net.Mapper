using System.Reflection;
using Dbarone.Net.Extensions;

using Dbarone.Net.Mapper;

/// <summary>
/// Base class for all mapper operator types.
/// </summary>
public abstract class MapperOperator
{
    protected MapperBuilder Builder { get; set; }
    protected BuildType From { get; set; }
    protected BuildType To { get; set; }

    public MapperOperator(MapperBuilder builder, BuildType from, BuildType to)
    {
        this.Builder = builder;
        this.From = from;
        this.To = to;
    }

    /// <summary>
    /// Sets the priority of the node.
    /// </summary>
    public virtual int Priority => 0;

    /// <summary>
    /// Returns true if the current class can map the from / to types.
    /// </summary>
    /// <param name="from">The from type.</param>
    /// <param name="to">The to type.</param>
    /// <returns>Returns true if the current mapper can map the types.</returns>
    public abstract bool CanMap();

    /// <summary>
    /// When implemented in a class, should return a <see cref="MapperDelegate"/> object that
    /// can map an object of 'from' type to 'to' type. This method should also perform any
    /// build-time validation, and add any errors to the errors collection.
    /// </summary>
    /// <returns></returns>
    public abstract MapperDelegate GetMap();

    protected virtual IDictionary<string, MapperOperator> GetChildren() {
        return new Dictionary<string, MapperOperator>();
    }

    /// <summary>
    /// Factory method to create a new MapperOperator instance based on from / to types.
    /// </summary>
    /// <param name="builder">The current <see cref="MapperBuilder"/> instance.</param>
    /// <param name="from">The from type.</param>
    /// <param name="to">The to type.</param>
    /// <returns>A <see cref="MapperOperator"/> instance that can map from / to types.</returns>
    /// <exception cref="MapperBuildException">Throws an exception if no suitable mapper found.</exception>
    public static MapperOperator Create(MapperBuilder builder, BuildType from, BuildType to) {
        var operatorTypes = AppDomain.CurrentDomain.GetTypesAssignableFrom(typeof(MapperOperator)).Where(t=>!t.IsAbstract);
        List<MapperOperator> mapperOperators = new List<MapperOperator>();
        foreach (var type in operatorTypes) {
            var mapperOperator = (MapperOperator)Activator.CreateInstance(type, builder, from, to);
            mapperOperators.Add(mapperOperator);
            mapperOperators = mapperOperators.OrderBy(o => o.Priority).ToList();
        }

        // Find the first mapper operator that can map the types
        foreach (var mapperOperator in mapperOperators) {
            if (mapperOperator.CanMap()) {
                return mapperOperator;
            }
        }

        // Shouldn't get here.
        throw new MapperBuildException(from.Type, MapperEndPoint.Source, "", $"Cannot map to ${to.Type.Name} type.");
    }

    /// <summary>
    /// Pretty-prints the current mapper operator. Used to display the full mapping operation.
    /// </summary>
    /// <returns>A string value containing the complete mapping operation.</returns>
    public string PrettyPrint() {
        return "";
    }
}