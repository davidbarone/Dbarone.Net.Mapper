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
    public string? MemberName { get; set; } = default!;
    
    /// <summary>
    /// The message relating to the mapper build error. 
    /// </summary>
    public string Message { get; set; } = default!;

    /// <summary>
    /// Creates a new MapperBuildError instance.
    /// </summary>
    /// <param name="sourceType">The source type.</param>
    /// <param name="destinationType">The destination type.</param>
    /// <param name="path">The path within the mapping.</param>
    /// <param name="memberName">The optional member name</param>
    /// <param name="message">The error message.</param>
    public MapperBuildError(Type sourceType, Type destinationType, string path, string? memberName, string message) {
        this.SourceType = sourceType;
        this.DestinationType = destinationType;
        this.Path = path;
        this.MemberName = memberName;
        this.Message = message;
    }
}