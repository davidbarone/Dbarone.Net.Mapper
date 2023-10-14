namespace Dbarone.Net.Mapper;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;

/// <summary>
/// Defines the build-time metadata of a single type.
/// </summary>
public class BuildType
{
    /// <summary>
    /// The type the metadata relates to.
    /// </summary>
    public Type Type { get; set; } = default!;

    /// <summary>
    /// Defines the options for the type.
    /// </summary>
    public MapperOptions Options { get; set; } = default!;

    /// <summary>
    /// Defines the members on the type.
    /// </summary>
    public IList<BuildMember> Members { get; set; } = new List<BuildMember>();

    /// <summary>
    /// Provides the member resolving strategy for this type.
    /// </summary>
    public IMemberResolver MemberResolver { get; set; } = default!;

    /// <summary>
    /// Resolves a member/unary expression to a member configuration.
    /// </summary>
    /// <param name="expr">A unary expression to select a member.</param>
    /// <returns>Returns the <see cref="BuildMember" /> instance matching the member selected.</returns>
    public BuildMember? GetMemberRule(Expression expr)
    {
        return this.Members.FirstOrDefault(x => x.MemberName == expr.GetMemberPath());
    }

    /// <summary>
    /// Validates the type configuration.
    /// </summary>
    public void Validate()
    {
    }

    /// <summary>
    /// Returns the active member internal names used by this type.
    /// </summary>
    /// <returns></returns>
    public string[] GetActiveInternalMemberNames() {
        return this.MemberConfiguration.Where(m => m.Ignore == false).Select(m => m.InternalMemberName).ToArray();
    }

     /// <summary>
    /// Provides a member filtering rule.
    /// </summary>
    public MemberFilterDelegate? MemberFilterRule { get; set; } = null;
   
    public void ValidateMap() {

    }
}