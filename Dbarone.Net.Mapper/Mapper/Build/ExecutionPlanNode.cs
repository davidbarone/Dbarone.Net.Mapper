using System.Text.Json;
namespace Dbarone.Net.Mapper;

/// <summary>
/// Represents a mapping execution plan. Each execution plan node represents a mapper operation.
/// </summary>
public class ExecutionPlanNode
{
    public string Path { get; set; }
    public string MapperOperation { get; set; }
    public string FromType { get; set; }
    public string FromMemberResolver { get; set; }
    public string ToType { get; set; }
    public string ToMemberResolver { get; set; }
    public ExecutionPlanNode Parent { get; set; }

    public IDictionary<string, ExecutionPlanNode> Children { get; set; }

    /// <summary>
    /// Creates a new ExecutionPlanNode.
    /// </summary>
    /// <param name="mapperOperation">The mapping operation used.</param>
    /// <param name="fromType">The from type.</param>
    /// <param name="fromMemberResolver">The from member resolver.</param>
    /// <param name="toType">The to type.</param>
    /// <param name="toMemberResolver">The to member resolver.</param>
    /// <param name="children">The child nodes.</param>
    public ExecutionPlanNode(string path, string mapperOperation, string fromType, string fromMemberResolver, string toType, string toMemberResolver, ExecutionPlanNode parent)
    {
        this.Path = path;
        this.MapperOperation = mapperOperation;
        this.FromType = fromType;
        this.FromMemberResolver = fromMemberResolver;
        this.ToType = toType;
        this.ToMemberResolver = toMemberResolver;
        this.Parent = parent;
        this.Children = new Dictionary<string, ExecutionPlanNode>();
    }

    public void AddChild(string key, ExecutionPlanNode child) {
        this.Children[key] = child;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}