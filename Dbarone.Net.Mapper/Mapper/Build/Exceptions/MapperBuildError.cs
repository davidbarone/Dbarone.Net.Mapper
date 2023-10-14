namespace Dbarone.Net.Mapper;

/// <summary>
/// A single build-time error raised during the mapper build process.
/// </summary>
public class MapperBuildError {
    
    /// <summary>
    /// The source type relating to the mapper build error.  
    /// </summary>
    public Type SourceType { get; set; } = default!;
    
    /// <summary>
    /// The destination type relating to the mapper build error.
    /// </summary>
    public Type DestinationType { get; set; } = default!;

    /// <summary>
    /// The member path relating to the mapper build error.
    /// </summary>
    public string Path { get; set; } = default!;

    /// <summary>
    /// The optional member relating to the mapper build error.
    /// </summary>
    public string MemberName { get; set; } = default!;
    
    /// <summary>
    /// The message relating to the mapper build error. 
    /// </summary>
    public string Message { get; set; } = default!;
}