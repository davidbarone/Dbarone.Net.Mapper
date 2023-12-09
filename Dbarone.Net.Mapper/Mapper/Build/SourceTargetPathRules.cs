namespace Dbarone.Net.Mapper;

/// <summary>
/// Mapper rules defined for a particular Source + target tuple, and path. 
/// </summary>
public class SourceTargetPathRules {

    /// <summary>
    /// The source and target types the rule belongs to.
    /// </summary>
    public SourceTarget SourceTarget { get; set; } = default!;
    
    /// <summary>
    /// The path within the mapping graph that the rules belong to.
    /// </summary>
    public string Path { get; set; } = default!;
    
    /// <summary>
    /// The mapper rules.
    /// </summary>
    public IEnumerable<MapperDelegate> Maps { get; set; } = default!;
}