namespace Dbarone.Net.Mapper;

/// <summary>
/// The MapperBuild exception class is used for all exceptions throw during the build process of the mapper.
/// </summary>
public class MapperBuildException : Exception
{
    /// <summary>
    /// The list of notifications occurring during the build process.
    /// </summary>
    public List<MapperBuildNotification> Notifications { get; set; } = new List<MapperBuildNotification>();

    /// <summary>
    /// Exception constructor.
    /// </summary>
    public MapperBuildException(string message, List<MapperBuildNotification> notifications) : base(message)
    {
        this.Notifications = notifications;
    }
}