namespace Dbarone.Net.Mapper;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;

/// <summary>
/// Builds a mapping rule.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="U"></typeparam>
public class MapperBuilder<T, U>
{

    /// <summary>
    /// Creates a new MapperBuilder class.
    /// </summary>
    /// <returns></returns>
    public static MapperBuilder<T, U> Create()
    {
        return new MapperBuilder<T, U>();
    }

    /// <summary>
    /// Sets the member naming convention for the source.
    /// </summary>
    /// <param name="convention"></param>
    /// <returns></returns>
    public MapperBuilder<T, U> SetSourceMemberNamingConvention(NamingConvention convention)
    {
        this.Configuration.SourceMemberNamingConvention = convention;
        return this;
    }

    public MapperBuilder<T, U> SetDestinationMemberNamingConvention(NamingConvention convention)
    {
        this.Configuration.DestinationMemberNamingConvention = convention;
        return this;
    }

    /// <summary>
    /// Define which property will not be mapped to document
    /// </summary>
    public MapperBuilder<T, U> Ignore<K>(Expression<Func<T, K>> member)
    {
        return this.ApplyMemberAction(member, (p) =>
        {
            Configuration.MemberRules.Remove(p);
        });
    }

    /// <summary>
    /// Define a custom name for a property when mapping to document
    /// </summary>
    public MapperBuilder<T, U> Rename<K>(Expression<Func<T, K>> member, string newName)
    {
        if (newName.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(newName));

        return this.ApplyMemberAction(member, (p) =>
        {
            p.ReplacementMemberName = newName;
        });
    }

    /// <summary>
    /// Selects a member, then applies an action to the member mapping rule.
    /// </summary>
    private MapperBuilder<T, U> ApplyMemberAction<TK, K>(Expression<Func<TK, K>> member, Action<RuleMemberMap> action)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));

        var memb = Configuration.GetMemberRule(member);

        if (memb == null)
        {
            throw new ArgumentNullException($"Member '{member.GetMemberPath()}' not found in type.");
        }

        action(memb);

        return this;
    }
    public RuleEntityMap Configuration { get; } = new RuleEntityMap();
}