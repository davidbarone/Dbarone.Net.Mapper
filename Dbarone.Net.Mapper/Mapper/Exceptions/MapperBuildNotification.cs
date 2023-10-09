namespace Dbarone.Net.Mapper;

/// <summary>
/// A notification / message raised during the mapper build process.
/// </summary>
public class MapperBuildNotification {
    
    /// <summary>
    /// The source type relating to the mapper build notification.  
    /// </summary>
    public Type SourceType { get; set; } = default!;
    
    /// <summary>
    /// The destination type relating to the mapper build notification.
    /// </summary>
    public Type DestinationType { get; set; } = default!;
    
    /// <summary>
    /// The optional member relating to the mapper build notification.
    /// </summary>
    public string MemberName { get; set; } = default!;
    
    /// <summary>
    /// The message relating to the mapper build notification 
    /// </summary>
    public string Message { get; set; } = default!;
}