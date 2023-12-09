namespace Dbarone.Net.Mapper;

/// <summary>
/// Represents a particular path within a source + target ruleset. Used as key to store mapping rules.
/// </summary>
public class SourceTargetPath {
    
    /// <summary>
    /// The source and target types.
    /// </summary>
    public SourceTarget SourceTarget {get; init;}

    /// <summary>
    /// The path within the source / target map.
    /// </summary>
    public string Path { get; init; }

    /// <summary>
    /// Creates a new SourceTargetPath instance.
    /// </summary>
    /// <param name="sourceTarget">The source and target tuple.</param>
    /// <param name="path">The path.</param>
    public SourceTargetPath(SourceTarget sourceTarget, string path) {
        this.SourceTarget = sourceTarget;
        this.Path = path;
    }

    /// <summary>
    /// Overrides implementation of GetHashCode.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        int hash = 17;

        // Source + Target cannot be null.
        hash = hash * 23 + SourceTarget.GetHashCode();
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

        // If parameter cannot be cast to SourceTarget return false.
        SourceTargetPath? sdp = obj as SourceTargetPath;
        if ((System.Object?)sdp == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (this.SourceTarget.Equals(sdp.SourceTarget)) && (this.Path.Equals(sdp.Path));
    }
}