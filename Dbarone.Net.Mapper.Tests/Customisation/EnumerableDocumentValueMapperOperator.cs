using Dbarone.Net.Document;
using Dbarone.Net.Mapper;
using Dbarone.Net.Extensions;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Maps IEnumerable types to a <see cref="DocumentValue"/> type.
/// </summary>
public class EnumerableDocumentValueMapperOperator : MapperOperator
{
    private MapperOperator? runtimeOperator = null;

    /// <summary>
    /// Creates a new <see cref="EnumerableToDocumentValueMapperOperator"/> instance.
    /// </summary>
    /// <param name="builder">The <see cref="MapperBuilder"/> instance.</param>
    /// <param name="sourceType">The source <see cref="BuildType"/> instance.</param>
    /// <param name="targetType">The target <see cref="BuildType"/> instance.</param>
    /// <param name="parent">An optional parent <see cref="MapperOperator"/> instance.</param>
    /// <param name="onLog">Optional logging callback.</param>
    public EnumerableDocumentValueMapperOperator(MapperBuilder builder, BuildType sourceType, BuildType targetType, MapperOperator? parent = null, MapperOperatorLogDelegate? onLog = null) : base(builder, sourceType, targetType, parent, onLog)
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
    /// The <see cref="ObjectSourceMapperOperator"/> operator is used to map objects declared as type 'object' at runtime. 
    /// </summary>
    /// <returns>Returns true when the source declared type is 'object'.</returns>
    public override bool CanMap()
    {
        return SourceType.MemberResolver.HasMembers
            && TargetType.Type == typeof(DocumentValue);
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
            // Switch target type to use DictionaryDocument
            this.runtimeOperator = Builder.GetMapperOperator(new SourceTarget(sourceRunTimeType, typeof(DictionaryDocument)), this);
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