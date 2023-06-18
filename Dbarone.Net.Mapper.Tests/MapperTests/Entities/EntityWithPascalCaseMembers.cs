namespace Dbarone.Net.Mapper.Tests;

public class EntityWithPascalCaseMembers
{
    public int EntityId { get; set; }
    public string EntityName { get; set; } = default!;
}