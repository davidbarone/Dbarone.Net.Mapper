using System.Text.Json;
namespace Dbarone.Net.Mapper;

/// <summary>
/// Represents a mapping execution plan. Each execution plan node represents a mapper operation.
/// </summary>
public class ExecutionPlanNode
{
    public string MapperOperation { get; set; }
    public string FromType { get; set; }
    public string FromMemberResolver { get; set; }
    public string ToType { get; set; }
    public string ToMemberResolver { get; set; }
    public IEnumerable<ExecutionPlanNode> Children { get; set; }

    /// <summary>
    /// Creates a new ExecutionPlanNode.
    /// </summary>
    /// <param name="mapperOperation">The mapping operation used.</param>
    /// <param name="fromType">The from type.</param>
    /// <param name="fromMemberResolver">The from member resolver.</param>
    /// <param name="toType">The to type.</param>
    /// <param name="toMemberResolver">The to member resolver.</param>
    /// <param name="children">The child nodes.</param>
    public ExecutionPlanNode(string mapperOperation, string fromType, string fromMemberResolver, string toType, string toMemberResolver, IEnumerable<ExecutionPlanNode> children)
    {
        this.MapperOperation = mapperOperation;
        this.FromType = fromType;
        this.FromMemberResolver = fromMemberResolver;
        this.ToType = toType;
        this.ToMemberResolver = toMemberResolver;
        this.Children = children;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}