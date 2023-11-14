namespace Dbarone.Net.Mapper;

public class BuildPlanNode {
    public IMapperProvider MapperProvider { get; set; }
    public string Path { get; set; }
    public Type FromType { get; set; }
    public IMemberResolver FromMemberResolver { get; set; }
    public Type ToType { get; set; }
    public IMemberResolver ToMemberResolver { get; set; }
    public BuildPlanNode[] Children { get; set; }
}