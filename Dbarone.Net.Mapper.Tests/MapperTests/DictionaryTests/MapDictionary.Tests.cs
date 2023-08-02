namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Mapper;
using Dbarone.Net.Extensions.Object;
using System.Linq.Expressions;
using System.Collections;

public class MapDictionaryTests
{
    [Fact]
    public void DictToDict()
    {
        Dictionary<string, object> dict1 = new Dictionary<string, object>();
        dict1["MemberA"] = DateTime.Now;
        dict1["MemberB"] = int.MaxValue;
        dict1["MemberC"] = "foobar";

        var mapper = MapperConfiguration
        .Create()
        .RegisterType<Dictionary<string, object>>()
        .Build();

        var dict2 = mapper.MapOne<Dictionary<string, object>, Dictionary<string, object>>(dict1);

        Assert.True(dict2.ValueEquals(dict1));
    }
}