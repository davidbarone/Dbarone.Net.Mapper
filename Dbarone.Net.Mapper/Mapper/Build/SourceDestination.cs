namespace Dbarone.Net.Mapper;

/// <summary>
/// Represents a source + destination tuple.
/// </summary>
public class SourceDestination
{

    /// <summary>
    /// The source type.
    /// </summary>
    public Type Source { get; init; }

    /// <summary>
    /// The destination type.
    /// </summary>
    public Type Destination { get; init; }

    /// <summary>
    /// Creates a new <see cref="SourceDestination"/>. 
    /// </summary>
    /// <param name="source">The source type.</param>
    /// <param name="destination">The destination type.</param>
    public SourceDestination(Type source, Type destination)
    {
        this.Source = source;
        this.Destination = destination;
    }

    /// <summary>
    /// Overrides implementation of GetHashCode.
    /// See: https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode
    /// </summary>
    /// <returns>Returns an integer which can be used to compare 2 instances.</returns>
    public override int GetHashCode()
    {
        int hash = 17;

        // Source + Destination cannot be null.
        hash = hash * 23 + Source.GetHashCode();
        hash = hash * 23 + Destination.GetHashCode();
        return hash;
    }

    /// <summary>
    /// Overrides implementation of Equals.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
         // If parameter is null return false.
        if (obj == null)
        {
            return false;
        }

        // If parameter cannot be cast to SourceDestination return false.
        SourceDestination sd = obj as SourceDestination;
        if ((System.Object?)sd == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (this.Source == sd.Source) && (this.Destination == sd.Destination);
    }
}