using System.Reflection;
using Dbarone.Net.Extensions;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Base class for all mapper operator types.
/// </summary>
public abstract class MapperOperator
{
    /// <summary>
    /// Reference to the current MapperBuilder instance.
    /// </summary>
    protected MapperBuilder Builder { get; set; }
    
    /// <summary>
    /// The 'From' <see cref="BuildType"/> object.
    /// </summary>
    protected BuildType From { get; set; }
    
    /// <summary>
    /// The 'To' <see cref="BuildType"/> object.
    /// </summary>
    protected BuildType To { get; set; }

    /// <summary>
    /// Optional parent <see cref="MapperOperator"/> reference.
    /// </summary>
    protected MapperOperator? Parent { get; set; }

    /// <summary>
    /// Create a new <see cref="MapperOperator"/> instance.
    /// </summary>
    /// <param name="builder">A <see cref="MapperBuilder"/> instance.</param>
    /// <param name="from">The 'From' <see cref="BuildType"/>.</param>
    /// <param name="to">The 'To' <see cref="BuildType"/>.</param>
    /// <param name="parent">Optional parent <see cref="MapperOperator"/> instance.</param>
    public MapperOperator(MapperBuilder builder, BuildType from, BuildType to, MapperOperator? parent = null)
    {
        this.Parent = parent;
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
    /// <returns>Returns true if the current mapper can map the from / to types.</returns>
    public abstract bool CanMap();

    /// <summary>
    /// When implemented in a class, should return a <see cref="MapperDelegate"/> object that
    /// can map an object of 'from' type to 'to' type. This method should also perform any
    /// build-time validation, and add any errors to the errors collection.
    /// </summary>
    /// <returns></returns>
    public abstract MapperDelegate GetMap();

    /// <summary>
    /// Function to get the children of the current operation. Must be implemented in subclasses. This function is called by the Children property.
    /// </summary>
    /// <returns>Returns the children operations. If no children for the operation, call the base implementation.</returns>
    protected virtual IDictionary<string, MapperOperator> GetChildren()
    {
        return new Dictionary<string, MapperOperator>();
    }

    private IDictionary<string, MapperOperator> _children = default!;
    
    /// <summary>
    /// Returns the children of the current operation.
    /// </summary>
    public IDictionary<string, MapperOperator> Children
    {
        get
        {
            if (_children == null)
            {
                _children = GetChildren();
            }
            return _children;
        }
    }

    /// <summary>
    /// Factory method to create a new MapperOperator instance based on from / to types.
    /// </summary>
    /// <param name="builder">The current <see cref="MapperBuilder"/> instance.</param>
    /// <param name="from">The from type.</param>
    /// <param name="to">The to type.</param>
    /// <param name="parent">An optional parent operator.</param>
    /// <param name="onCreateOperator">An optional <see cref="CreateOperatorDelegate"/> callback function. This callback function is executed each time an operator is created within the mapping operator graph.</param>
    /// <returns>A <see cref="MapperOperator"/> instance that can map from / to types.</returns>
    /// <exception cref="MapperBuildException">Throws an exception if no suitable mapper found.</exception>
    public static MapperOperator Create(MapperBuilder builder, BuildType from, BuildType to, MapperOperator? parent = null, CreateOperatorDelegate? onCreateOperator = null)
    {
        var operatorTypes = AppDomain.CurrentDomain.GetTypesAssignableFrom(typeof(MapperOperator)).Where(t => !t.IsAbstract);
        List<MapperOperator> mapperOperators = new List<MapperOperator>();
        foreach (var type in operatorTypes)
        {
            var mapperOperator = (MapperOperator?)Activator.CreateInstance(type, builder, from, to, parent);
            if (mapperOperator == null)
            {
                throw new MapperBuildException(from.Type, MapperEndPoint.Source, "", $"Unable to create mapper operator for mapper type: {type.Name}.");
            }
            mapperOperators.Add(mapperOperator);
            mapperOperators = mapperOperators.OrderBy(o => o.Priority).ToList();
        }

        // Find the first mapper operator that can map the types
        foreach (var mapperOperator in mapperOperators)
        {
            if (mapperOperator.CanMap())
            {
                // Throw event
                if (onCreateOperator != null)
                {
                    onCreateOperator(mapperOperator.ToMapperOperatorInfo());
                }
                return mapperOperator;
            }
        }

        // Shouldn't get here.
        throw new MapperBuildException(from.Type, MapperEndPoint.Source, "", $"Cannot map to ${to.Type.Name} type.");
    }

    /// <summary>
    /// Returns a <see cref="MapperOperatorInfo"/> object that contains key information for the operator.
    /// </summary>
    /// <returns>Returns a <see cref="MapperOperatorInfo"/> object representing the mapping operator.</returns>
    public MapperOperatorDiagnostics ToMapperOperatorInfo()
    {
        MapperOperatorDiagnostics diag = new MapperOperatorDiagnostics(
            this.GetPath(),
            this.GetType().Name,
            this.From.Type.Name,
            this.From.MemberResolver.GetType().Name,
            this.To.Type.Name,
            this.To.MemberResolver.GetType().Name
        );
        return diag;
    }

    /// <summary>
    /// Gets the path of the current <see cref="MapperOperator"/>.
    /// </summary>
    /// <returns>Returns a string value representing the path of the current operator.</returns>
    /// <exception cref="Exception">Throws an exception if an invalid child.</exception>
    public string GetPath()
    {
        if (this.Parent == null)
        {
            return "";
        }
        else
        {
            foreach (var key in this.Parent.Children.Keys)
            {
                var item = this.Parent.Children[key];
                if (Object.ReferenceEquals(item, this))
                {
                    return $"{this.Parent.GetPath()}.{key}";
                }
            }
            throw new Exception("shouldn't get here");
        }
    }
}