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
    public IList<MapperMemberConfiguration> MemberConfiguration { get; set; } = new List<MapperMemberConfiguration>();

    /// <summary>
    /// Provides the member resolving strategy for this type.
    /// </summary>
    public IMemberResolver MemberResolver { get; set; } = default!;

    /// <summary>
    /// Resolves a member/unary expression to a member configuration.
    /// </summary>
    /// <param name="expr">A unary expression to select a member.</param>
    /// <returns>Retuens the <see cref="MapperMemberConfiguration" /> instance matching the member selected.</returns>
    public MapperMemberConfiguration? GetMemberRule(Expression expr)
    {
        return this.MemberConfiguration.FirstOrDefault(x => x.MemberName == expr.GetMemberPath());
    }

    /// <summary>
    /// Validates the type configuration.
    /// </summary>
    public void Validate()
    {
        // Check no duplicate internal names
        var duplicates = this.MemberConfiguration
            .GroupBy(g => g.InternalMemberName)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key).ToList();

        if (duplicates.Any())
        {
            var duplicateValues = duplicates.Aggregate("", (current, next) => current + " " + $"[{next}]");
            throw new MapperException($"The following internal member names have been used for multiple members on type: {this.Type}:{duplicateValues}.");
        }
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