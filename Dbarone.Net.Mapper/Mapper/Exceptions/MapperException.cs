namespace Dbarone.Net.Mapper;

/// <summary>
/// Mapper exception class. Used for all exceptions thrown by Dbarone.Net.Mapper.
/// </summary>
public class MapperException : Exception
{
    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public MapperException()
    {
    }

    /// <summary>
    /// Create a mapper exception with message.
    /// </summary>
    /// <param name="message"></param>
    public MapperException(string message)
        : base(message)
    {
    }
}