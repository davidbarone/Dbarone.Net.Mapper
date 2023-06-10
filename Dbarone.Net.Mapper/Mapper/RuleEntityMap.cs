namespace Dbarone.Net.Mapper;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

public class RuleEntityMap
{
    public Type FromType { get; set; }

    public Type ToType { get; set; }

    public bool ShouldIncludeFields { get; set; }

    public bool ShouldIncludeNonPublic { get; set; }
    
    public NamingConvention SourceMemberNamingConvention { get; set; }

    public NamingConvention DestinationMemberNamingConvention { get; set; }

    public Func<string, string> SourceMemberNameReplacer { get; set; }

    public List<RuleMemberMap> MemberRules { get; } = new List<RuleMemberMap>();

    /// <summary>
    /// Resolve expression to get member mapped
    /// </summary>
    public RuleMemberMap? GetMemberRule(Expression expr)
    {
        return this.MemberRules.FirstOrDefault(x => x.MemberName == GetPath(expr));
    }

}