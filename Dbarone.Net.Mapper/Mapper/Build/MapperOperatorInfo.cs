using System.Text.Json;
namespace Dbarone.Net.Mapper;

/// <summary>
/// Represents a mapping execution plan. Each execution plan node represents a mapper operation.
/// </summary>
public class MapperOperatorInfo
{
    /// <summary>
    /// The path of the mapper operator.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Text name of the <see cref="MapperOperator"/>.
    /// </summary>
    public string MapperOperator { get; set; }

    /// <summary>
    /// The From type name.
    /// </summary>
    public string FromType { get; set; }

    /// <summary>
    /// The From member resolver name.
    /// </summary>
    public string FromMemberResolver { get; set; }

    /// <summary>
    /// The To type name.
    /// </summary>
    public string ToType { get; set; }

    /// <summary>
    /// The To member resolver name.
    /// </summary>
    public string ToMemberResolver { get; set; }

    /// <summary>
    /// Creates a new <see cref="MapperOperatorInfo"/>.
    /// </summary>
    /// <param name="path">The mapping operator path.</param>
    /// <param name="mapperOperator">The mapping operator used.</param>
    /// <param name="fromType">The from type.</param>
    /// <param name="fromMemberResolver">The from member resolver.</param>
    /// <param name="toType">The to type.</param>
    /// <param name="toMemberResolver">The to member resolver.</param>
    public MapperOperatorInfo(string path, string mapperOperator, string fromType, string fromMemberResolver, string toType, string toMemberResolver)
    {
        this.Path = path;
        this.MapperOperator = mapperOperator;
        this.FromType = fromType;
        this.FromMemberResolver = fromMemberResolver;
        this.ToType = toType;
        this.ToMemberResolver = toMemberResolver;
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