/// <summary>
/// Mapper exception class.
/// </summary>
public class MapperException : Exception
{
    public MapperException()
    {
    }

    public MapperException(string message)
        : base(message)
    {
    }
}