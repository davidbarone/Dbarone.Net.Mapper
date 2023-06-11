namespace Dbarone.Net.Mapper;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;

/// <summary>
/// Defines the configuration of a single type.
/// </summary>
public class MapperTypeConfiguration
{
    /// <summary>
    /// The type the configuration relates to.
    /// </summary>
    public Type Type { get; set; } = default!;

    /// <summary>
    /// Defines the options for the map registration
    /// </summary>
    public MapperOptions Options { get; set; } = default!;

    /// <summary>
    /// Defines the member configuration.
    /// </summary>
    public List<MapperMemberConfiguration> MemberConfiguration { get; set; } = new List<MapperMemberConfiguration>();

    /// <summary>
    /// Resolves a member/unary expression to a member configuration.
    /// </summary>
    public MapperMemberConfiguration? GetMemberRule(Expression expr)
    {
        return this.MemberConfiguration.FirstOrDefault(x => x.MemberName == expr.GetMemberPath());
    }

}