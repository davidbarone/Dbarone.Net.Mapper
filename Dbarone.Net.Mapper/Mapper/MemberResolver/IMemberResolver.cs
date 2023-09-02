namespace Dbarone.Net.Mapper;
using System.Reflection;

/// <summary>
/// Interface for describing methods to 
/// </summary>
public interface IMemberResolver
{
    Getter GetGetter(string memberName);
    Setter GetSetter(string memberName);
    CreateInstance CreateInstance(params object[] args);

    /// <summary>
    /// Returns an array of member names
    /// </summary>
    string[] GetMembers();

    /// <summary>
    /// Gets members on an instance. This method is normally implemented only if DeferMemberResolution is set to true.
    /// </summary>
    /// <param name="obj">The object instance.</param>
    /// <returns>A string array of member names.</returns>
    string[] GetInstanceMembers(object obj);

    /// <summary>
    /// Gets a member type.
    /// </summary>
    /// <param name="memberName">The member name</param>
    /// <returns>Returns the member type.</returns>
    Type GetMemberType(string memberName);

    /// <summary>
    /// Set to true for dictionary and dynamic types, where the member information
    /// must be deferred until mapping time.
    /// </summary>
    bool DeferMemberResolution { get; }

    /// <summary>
    /// Set to true if the current IMemberResolver can resolve members of the specified type.
    /// </summary>
    /// <param name="type">The type to resolve members for.</param>
    /// <returns>Returns true if the current IMemberResolver can resolve members of the specified type.</returns>
    bool CanResolveMembersOfType(Type type);
}