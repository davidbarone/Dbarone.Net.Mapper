using System.Text.Json;
namespace Dbarone.Net.Mapper;

/// <summary>
/// Used by debugging and logging to represent a single mapper operation.
/// </summary>
public class MapperOperatorDiagnostics
{
    /// <summary>
    /// Determines whether the diagnostic event relates to build time or run time.
    /// </summary>
    public MapperOperatorDiagnosticMode Mode { get; set; }

    /// <summary>
    /// The path of the mapper operator.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Text name of the <see cref="MapperOperator"/>.
    /// </summary>
    public string MapperOperator { get; set; }

    /// <summary>
    /// The source type name.
    /// </summary>
    public string SourceType { get; set; }

    /// <summary>
    /// The source member resolver name.
    /// </summary>
    public string SourceMemberResolver { get; set; }

    /// <summary>
    /// The target type name.
    /// </summary>
    public string TargetType { get; set; }

    /// <summary>
    /// The target member resolver name.
    /// </summary>
    public string TargetMemberResolver { get; set; }

    /// <summary>
    /// The number of iterations that the operator has executed.
    /// </summary>
    public int Iterations { get; set; }

    /// <summary>
    /// The total duration of the operation.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// The number of iterations per second.
    /// </summary>
    public int IterationsPerSecond => Iterations * 1000 / Duration.Milliseconds;

    /// <summary>
    /// Creates a new <see cref="MapperOperatorDiagnostics"/>.
    /// </summary>
    /// <param name="path">The mapping operator path.</param>
    /// <param name="mapperOperator">The mapping operator used.</param>
    /// <param name="sourceType">The from type.</param>
    /// <param name="sourceMemberResolver">The from member resolver.</param>
    /// <param name="targetType">The to type.</param>
    /// <param name="targetMemberResolver">The to member resolver.</param>
    public MapperOperatorDiagnostics(string path, string mapperOperator, string sourceType, string sourceMemberResolver, string targetType, string targetMemberResolver)
    {
        this.Path = path;
        this.MapperOperator = mapperOperator;
        this.SourceType = sourceType;
        this.SourceMemberResolver = sourceMemberResolver;
        this.TargetType = targetType;
        this.TargetMemberResolver = targetMemberResolver;
    }

    /// <summary>
    /// Implementation of ToString(). Returns a JSON string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}