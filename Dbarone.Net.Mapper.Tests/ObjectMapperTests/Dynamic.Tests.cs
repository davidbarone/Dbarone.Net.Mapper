
using System.Dynamic;
using Dbarone.Net.Extensions;
using Dbarone.Net.Extensions.Object;
using Dbarone.Net.Mapper;

public class Vector
{
    public int x { get; set; }
    public int y { get; set; } = default!;
}

public class EntityWithDynamicProperty {
    public int EntityId { get; set; }
    public string EntityName { get; set; }
    public dynamic DynamicValue { get; set; }
}

public class DynamicTests
{

    [Fact]
    public void TestClassToDynamic()
    {
        var v = new Vector()
        {
            x = 123,
            y = 456
        };

        ObjectMapper mapper = new ObjectMapper(new MapperConfiguration().SetAutoRegisterTypes(true));

        var dict = mapper.Map<Vector, dynamic>(v);
        Assert.Equal(123, dict.x);
    }

    [Fact]
    public void TestDynamicToClass()
    {
        dynamic exp = new ExpandoObject();

        exp.x = 123;
        exp.y = 456;

        ObjectMapper mapper = new ObjectMapper(new MapperConfiguration().SetAutoRegisterTypes(true));
        var v = mapper.Map<dynamic, Vector>(exp);
        Assert.Equal(123, v.x);
    }
}