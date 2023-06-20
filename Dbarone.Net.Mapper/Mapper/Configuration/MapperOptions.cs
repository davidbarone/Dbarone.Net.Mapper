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
    /// Optional member renaming strategy.
    /// </summary>
    public IMemberRenameStrategy? MemberRenameStrategy { get; set; } = null;

    /// <summary>
    /// Defines implicit assertion of mapping rules prior to any map function call. Defaults to 'None'.
    /// </summary>
    public MapperEndPoint EndPointValidation { get; set; } = MapperEndPoint.None;
}