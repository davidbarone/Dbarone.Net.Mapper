/// <summary>
/// Mapper exception class.
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