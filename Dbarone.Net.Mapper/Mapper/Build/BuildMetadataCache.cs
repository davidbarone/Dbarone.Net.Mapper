namespace Dbarone.Net.Mapper;

/// <summary>
/// Contains cache of all build-time metada for mappings. 
/// </summary>
public class BuildMetadataCache {
    
    /// <summary>
    /// Stores the build-time type information for the mapper.
    /// </summary>
    public Dictionary<Type, BuildType> Types { get; set; } = new Dictionary<Type, BuildType>();

    /// <summary>
    /// Stores the build-time mapping rules between source and destination type pairs.
    /// </summary>
    public Dictionary<SourceDestination, Dictionary<SourceDestinationPath, SourceDestinationPathRules>> MapRules { get; set; } = new Dictionary<SourceDestination, Dictionary<SourceDestinationPath, SourceDestinationPathRules>>();
}