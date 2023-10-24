namespace Dbarone.Net.Mapper;

/// <summary>
/// Represents a particular path within a source + destination ruleset. Used as key to store mapping rules.
/// </summary>
public class SourceDestinationPath {
    
    /// <summary>
    /// The source and destination types.
    /// </summary>
    public SourceDestination SourceDestination {get; init;}

    /// <summary>
    /// The path within the source / destination map.
    /// </summary>
    public string Path { get; init; }

    /// <summary>
    /// Creates a new SourceDestinationPath instance.
    /// </summary>
    /// <param name="sourceDestination">The source and destination tuple.</param>
    /// <param name="path">The path.</param>
    public SourceDestinationPath(SourceDestination sourceDestination, string path) {
        this.SourceDestination = sourceDestination;
        this.Path = path;
    }

    /// <summary>
    /// Overrides implementation of GetHashCode.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        int hash = 17;

        // Source + Destination cannot be null.
        hash = hash * 23 + SourceDestination.GetHashCode();
        hash = hash * 23 + Path.GetHashCode();
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
        SourceDestinationPath? sdp = obj as SourceDestinationPath;
        if ((System.Object?)sdp == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (this.SourceDestination.Equals(sdp.SourceDestination)) && (this.Path.Equals(sdp.Path));
    }
}