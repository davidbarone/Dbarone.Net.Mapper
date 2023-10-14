namespace Dbarone.Net.Mapper;

/// <summary>
/// The MapperBuild exception class is used for all exceptions throw during the build process of the mapper.
/// </summary>
public class MapperBuildException : Exception
{
    /// <summary>
    /// The list of notifications occurring during the build process.
    /// </summary>
    public List<MapperBuildError> Errors { get; set; } = new List<MapperBuildError>();

    /// <summary>
    /// Exception constructor.
    /// </summary>
    public MapperBuildException(string message, List<MapperBuildError> errors) : base(message)
    {
        this.Errors = errors;
    }
}