using System.Net;

namespace Dbarone.Net.Mapper;

/// <summary>
/// The MapperRuntimeException class is used for all exceptions throw during the runtime mapping process.
/// </summary>
public class MapperRuntimeException : Exception
{
    /// <summary>
    /// Exception constructor.
    /// </summary>
    public MapperRuntimeException(string message) : base(message)
    {
    }
}