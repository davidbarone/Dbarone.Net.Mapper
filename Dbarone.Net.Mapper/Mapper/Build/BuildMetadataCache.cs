
using Dbarone.Net.Mapper;

/// <summary>
/// Contains cache of all build-time metada for mappings. 
/// </summary>
public class BuildMetadataCache {
    public IDictionary<Type, BuildType> Types { get; set; }

    public IDictionary<SourceDestination, IDictionary<SourceDestinationPath, SourceDestinationPathRules>> MapRules {get; set;}
}