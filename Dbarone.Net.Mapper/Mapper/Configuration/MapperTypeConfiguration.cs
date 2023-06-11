namespace Dbarone.Net.Mapper;

/// <summary>
/// Defines the configuration of a single type.
/// </summary>
public class MapperTypeConfiguration
{
    public Type Type { get; set; }

    /// <summary>
    /// Defines the options for the map registration
    /// </summary>
    public MapperOptions Options { get; set; }

}