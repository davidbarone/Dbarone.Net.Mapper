using Dbarone.Net.Mapper;

namespace Dbarone.Net.Mapper.Tests;

public class NullA
{
    public string? Name { get; set; } = null;
    public int? Age { get; set; } = null;
}

public class NullTests
{
    [Fact]
    public void TestNull()
    {
        NullA? a = null;
        var mapper = new ObjectMapper(new MapperConfiguration()
            .SetAutoRegisterTypes(true)
        );

        var b = mapper.Map<NullA, NullA>(null);
        Assert.Null(b);
    }

    [Fact]
    public void TestEnumerableNull1()
    {
        List<NullA>? a = null;
        var mapper = new ObjectMapper(new MapperConfiguration()
            .SetAutoRegisterTypes(true)
        );

        var b = mapper.Map<List<NullA>, List<NullA>>(null);
        Assert.Null(b);
    }

    [Fact]
    public void TestEnumerableNull2()
    {
        List<NullA?>? a = new List<NullA?>();
        a.Add(null);

        var mapper = new ObjectMapper(new MapperConfiguration()
            .SetAutoRegisterTypes(true)
        );

        var b = mapper.Map<List<NullA>, List<NullA>>(a);
        Assert.NotNull(b);
        Assert.Null(b!.First());
    }

    [Fact]
    public void TestNullMember()
    {
        NullA? a = new NullA();
        var mapper = new ObjectMapper(new MapperConfiguration()
            .SetAutoRegisterTypes(true)
        );

        var b = mapper.Map<NullA, NullA>(a);
        Assert.NotNull(b);
        Assert.Null(b!.Name);
    }

}