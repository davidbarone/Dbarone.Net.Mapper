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
    /// Returns true if the type is nullable value type.
    /// </summary>
    public bool IsNullable => this.Type.IsNullable();

    /// <summary>
    /// If the type is a nullable value type, returns the underlying type.
    /// </summary>
    public Type? NullableUnderlyingType => this.IsNullable ? Nullable.GetUnderlyingType(this.Type) : null;

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
    /// Provides a member filtering rule.
    /// </summary>
    public MemberFilterDelegate? MemberFilterRule { get; set; } = null;
}