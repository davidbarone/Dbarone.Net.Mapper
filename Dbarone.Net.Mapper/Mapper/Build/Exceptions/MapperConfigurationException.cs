using System.Net;

namespace Dbarone.Net.Mapper;

/// <summary>
/// The MapperConfigurationException class is used for all exceptions throw during the configuration process.
/// </summary>
public class MapperConfigurationException : Exception
{
    /// <summary>
    /// Exception constructor.
    /// </summary>
    public MapperConfigurationException(string message) : base(message)
    {
    }
}