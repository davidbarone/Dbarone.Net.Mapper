using Dbarone.Net.Mapper;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Operator for when the source type is object. This includes dynamic property types (which are generally
/// represented as Object data type). For these types, we defer the actual mapping until runtime.
/// </summary>
public class ObjectSourceMapperOperator : MapperOperator
{
    private MapperOperator? runtimeOperator = null;

    /// <summary>
    /// Creates a new <see cref="ObjectSourceMapperOperator"/> instance.
    /// </summary>
    /// <param name="builder">The <see cref="MapperBuilder"/> instance.</param>
    /// <param name="sourceType">The source <see cref="BuildType"/> instance.</param>
    /// <param name="targetType">The target <see cref="BuildType"/> instance.</param>
    /// <param name="parent">An optional parent <see cref="MapperOperator"/> instance.</param>
    /// <param name="onLog">Optional logging callback.</param>
    public ObjectSourceMapperOperator(MapperBuilder builder, BuildType sourceType, BuildType targetType, MapperOperator? parent = null, MapperOperatorLogDelegate? onLog = null) : base(builder, sourceType, targetType, parent, onLog)
    {
    }

    /// <summary>
    /// GetChildren implementation for <see cref="ObjectSourceMapperOperator"/>.
    /// </summary>
    /// <returns>Returns the children operators.</returns>
    /// <exception cref="MapperBuildException"></exception>
    protected override IDictionary<string, MapperOperator> GetChildren()
    {
        return new Dictionary<string, MapperOperator>{
            {"*", this.runtimeOperator!}
        };
    }

    /// <summary>
    /// Overrides the priority of the <see cref="ObjectSourceMapperOperator"/> instance.
    /// </summary>
    public override int Priority => 10;

    /// <summary>
    /// The <see cref="ObjectSourceMapperOperator"/> operator is used to map objects declared as type 'object' at runtime. 
    /// </summary>
    /// <returns>Returns true when the source declared type is 'object'.</returns>
    public override bool CanMap()
    {
        return SourceType.Type == typeof(object);
    }

    private void GetRuntimeOperator(object? source)
    {
        if (source == null)
        {
            throw new Exception("null value invalid.");
        }
        var sourceRunTimeType = source.GetType();
        if (this.runtimeOperator == null)
        {
            this.runtimeOperator = Builder.GetMapperOperator(new SourceTarget(sourceRunTimeType, this.TargetType.Type), this);
        }
    }

    /// <summary>
    /// Mapping implementation for <see cref="ObjectSourceMapperOperator"/> type. 
    /// </summary>
    /// <param name="source">The source object.</param>
    /// <returns>Returns a mapped object.</returns>
    /// <exception cref="MapperBuildException">Returns a <see cref="MapperBuildException"/> in the event of any failure to map the object.</exception>
    protected override object? MapInternal(object? source)
    {
        GetRuntimeOperator(source);
        return this.Children["*"].Map(source);
    }
}