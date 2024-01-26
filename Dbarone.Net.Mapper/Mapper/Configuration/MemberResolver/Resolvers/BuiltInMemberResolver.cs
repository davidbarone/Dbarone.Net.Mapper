namespace Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;

/// <summary>
/// General resolver for builtin types.
/// </summary>
public class BuiltinMemberResolver : IMemberResolver
{
    /// <summary>
    /// Gets the type members for builtin types.
    /// </summary>
    /// <param name="type">The type to get the members for.</param>
    /// <param name="options">The options.</param>
    /// <returns>An array of member names.</returns>
    public string[] GetTypeMembers(Type type, MapperOptions options)
    {
        throw new NotSupportedException("Builtin types do not have members.");
    }

    private IDictionary<string, MemberInfo> GetMembers(Type type, MapperOptions options)
    {
        throw new NotSupportedException("Builtin types do not have members.");
    }

    /// <summary>
    /// Set to true for dictionary and dynamic types, where the member information
    /// must be deferred until mapping time. If set to false, the member information
    /// is obtained at build time.
    /// </summary>
    public bool DeferBuild => false;

    /// <summary>
    /// Returns a delete that creates instances for reference types.
    /// </summary>
    /// <param name="type">The type to create the delegate for.</param>
    /// <param name="args">The arguments provides to the constructor.</param>
    /// <returns>Returns a delegate that can create an instance.</returns>
    public virtual CreateInstance CreateInstance(Type type, params object?[]? args)
    {
        List<ParameterExpression> parameters = new List<ParameterExpression>();

        // args array (optional)
        parameters.Add(Expression.Parameter(typeof(object[]), "args"));

        return Expression.Lambda<CreateInstance>(Expression.New(type), parameters).Compile();
    }

    /// <summary>
    /// Returns a getter delegate that gets a member value for an object.
    /// </summary>
    /// <param name="type">The type to get the getter for.</param>
    /// <param name="memberName">The member name.</param>
    /// <param name="options">The mapper options provided for the type.</param>
    /// <returns>Returns a getter object which, when invoked, will get a member value from an object.
    /// Returns a null reference if getter does not exist.</returns>
    public Getter GetGetter(Type type, string memberName, MapperOptions options)
    {
        throw new NotSupportedException("Builtin types do not have members.");
    }

    /// <summary>
    /// Returns a setter delegate that sets a member value for an object. 
    /// </summary>
    /// <param name="type">The type to get the setter for.</param>
    /// <param name="memberName">The member name</param>
    /// <param name="options">The mapper options provided for the type.</param>
    /// <returns>Returns a setter object which, when invoked, will set a member value for an object.
    /// Returns a null reference if setter does not exist.</returns>
    public Setter GetSetter(Type type, string memberName, MapperOptions options)
    {
        throw new NotSupportedException("Builtin types do not have members.");
    }

    /// <summary>
    /// Gets a member type.
    /// </summary>
    /// <param name="type">The type containing the member.</param>
    /// <param name="memberName">The member name.</param>
    /// <param name="options">The mapper options provided for the type.</param>
    /// <returns>Returns the member type.</returns>
    public Type GetMemberType(Type type, string memberName, MapperOptions options)
    {
        throw new NotSupportedException("Builtin types do not have members.");
    }

    /// <summary>
    /// Gets members on an instance. This method is normally implemented only if DeferMemberResolution is set to true.
    /// </summary>
    /// <param name="obj">The object instance.</param>
    /// <returns>A string array of member names.</returns>
    public string[] GetInstanceMembers(object obj)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Set to true if the current IMemberResolver can resolve members of the specified type.
    /// </summary>
    /// <param name="type">The type to resolve members for.</param>
    /// <returns>Returns true if the current IMemberResolver can resolve members of the specified type.</returns>
    public virtual bool CanResolveMembersForType(Type type)
    {
        return type.IsBuiltInType();
    }

    /// <summary>
    /// Returns true if types supported by this resolver have members.
    /// </summary>
    public virtual bool HasMembers => false;
}