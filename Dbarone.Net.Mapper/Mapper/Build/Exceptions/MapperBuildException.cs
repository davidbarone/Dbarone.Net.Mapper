using System.Net;

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

    /// <summary>
    /// Exception constructor.
    /// </summary>
    public MapperBuildException(List<MapperBuildError> errors) : base("An error has occurred during the build phase. Refer to the inner Errors property for details.")
    {
        this.Errors = errors;
    }

    /// <summary>
    /// Exception constructor.
    /// </summary>
    public MapperBuildException(Type type, MapperEndPoint endPoint, string path, string memberName, string message) : base("An error has occurred during the build phase. Refer to the inner Errors property for details.")
    {
        this.Errors = new List<MapperBuildError>{
            new MapperBuildError(type, endPoint, path, memberName, message)
        };
    }
}