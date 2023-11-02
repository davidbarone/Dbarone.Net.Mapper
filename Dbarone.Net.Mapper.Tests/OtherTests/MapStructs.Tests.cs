namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Mapper;

public struct VectorStructA
{
    public int X { get; set; }
    public int Y { get; set; }
}

public struct VectorStructB
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class MapStructsTests
{
    [Fact]
    public void Mapper_WhenMappingStructsDifferentTypes_ShouldMap()
    {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<VectorStructA>()
            .RegisterType<VectorStructB>()
        );

        var v1 = new VectorStructA() { X = 3, Y = 4 };
        var v2 = mapper.Map<VectorStructA, VectorStructB>(v1);
        Assert.Equal(3, v2.X);
    }

    [Fact]
    public void Mapper_WhenRegisteringStructsSameTypes_ShouldMap()
    {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<VectorStructA>()
            .RegisterType<VectorStructB>()
        );

        var v1 = new VectorStructA() { X = 3, Y = 4 };
        var v2 = mapper.Map<VectorStructA, VectorStructA>(v1);
        Assert.Equal(3, v2.X);
    }

}