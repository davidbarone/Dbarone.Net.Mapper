namespace Dbarone.Net.Mapper;
using Dbarone.Net.Extensions;

/// <summary>
/// Defines a set of mapping options.
/// </summary>
public class MapperOptions
{
    /// <summary>
    /// Set to true to include mapping of fields as well as properties. Default is false.
    /// </summary>
    public bool IncludeFields { get; set; } = false;

    /// <summary>
    /// Set to true to include private fields and properties. Default is false.1
    /// </summary>
    public bool IncludePrivateMembers { get; set; } = false;

    /// <summary>
    /// The default casing convention for members of the type. Default is CaseType.None.
    /// </summary>
    public CaseType MemberNameCaseType { get; set; } = CaseType.None;

    /// <summary>
    /// Optional member name translation function
    /// </summary>
    public Func<string, string> MemberNameTranslation { get; set; } = s => s;

    /// <summary>
    /// Defines implicit assertion of mapping rules prior to any map function call. Defaults to 'None'.
    /// </summary>
    public MapperEndPoint AssertMapEndPoint { get; set; } = MapperEndPoint.None;
}