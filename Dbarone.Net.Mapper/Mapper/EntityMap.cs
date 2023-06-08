
public class EntityMap {

    public Type FromType { get; set; }

    public Type ToType { get; set; }

    public NamingConvention SourceMemberNamingConvention { get; set; }

    public NamingConvention DestinationMemberNamingConvention { get; set; }

    public List<MemberRule> MemberRules { get; } = new List<MemberRule>();

}