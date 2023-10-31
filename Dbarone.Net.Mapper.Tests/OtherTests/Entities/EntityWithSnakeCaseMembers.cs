namespace Dbarone.Net.Mapper.Tests;

public class EntityWithSnakeCaseMembers
{
    public int entity_id { get; set; }
    public string entity_name { get; set; } = default!;
}