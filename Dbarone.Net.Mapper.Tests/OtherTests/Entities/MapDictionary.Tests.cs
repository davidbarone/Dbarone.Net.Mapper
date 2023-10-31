namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Mapper;
using Dbarone.Net.Extensions.Object;
using System.Linq.Expressions;
using System.Collections;

public class DictTestClass
{
    public int IntValue { get; set; } = int.MaxValue;
}

public class MapDictionaryTests
{
    
    [Fact]
    public void DictToDict()
    {
        Dictionary<string, object> dict1 = new Dictionary<string, object>();
        dict1["MemberA"] = DateTime.Now;
        dict1["MemberB"] = int.MaxValue;
        dict1["MemberC"] = "foobar";

        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<Dictionary<string, object>>());

        var dict2 = mapper.MapOne<Dictionary<string, object>, Dictionary<string, object>>(dict1);
        Assert.True(dict2.ValueEquals(dict1));
    }

    [Fact]
    public void TestClassToDict() {
        ClassA a = new ClassA();
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<Dictionary<string, object>>()
            .RegisterType<DictTestClass>());

        var dict = mapper.MapOne<ClassA, Dictionary<string, object>>(a);
        Assert.Equal(a.IntValue, dict["IntValue"]);
    }
}