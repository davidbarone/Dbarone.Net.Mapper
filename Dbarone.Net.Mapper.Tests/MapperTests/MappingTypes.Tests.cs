namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Mapper;

public class MappingTypesTests
{
    [Fact]
    public void Mapper_WhenMappingStructsDifferentTypes_ShouldMap()
    {
        var mapper = MapperConfiguration.Create()
            .RegisterType<VectorStructA>()
            .RegisterType<VectorStructB>()
            .Build();

        var v1 = new VectorStructA(){X = 3, Y = 4};
        var v2 = mapper.MapOne<VectorStructA, VectorStructB>(v1);
        Assert.Equal(3, v2.X);
    }

    [Fact]
    public void Mapper_WhenRegisteringStructsSameTypes_ShouldMap()
    {
        var mapper = MapperConfiguration.Create()
            .RegisterType<VectorStructA>()
            .Build();

        var v1 = new VectorStructA(){X = 3, Y = 4};
        var v2 = mapper.MapOne<VectorStructA, VectorStructA>(v1);
        Assert.Equal(3, v2.X);
    }

}