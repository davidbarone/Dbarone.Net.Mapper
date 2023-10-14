/// <summary>
/// Mapper rules defined for a particular Source + destination tuple, and path. 
/// </summary>
public class SourceDestinationPathRules {

    /// <summary>
    /// The source and destination types the rule belongs to.
    /// </summary>
    public SourceDestinationTuple SourceDestination { get; set; } = default!;
    
    /// <summary>
    /// The path within the mapping graph that the rules belong to.
    /// </summary>
    public string Path { get; set; } = default!;
    
    /// <summary>
    /// The mapper rules.
    /// </summary>
    public IEnumerable<MapperDelegate> Maps { get; set; } = default!;
}