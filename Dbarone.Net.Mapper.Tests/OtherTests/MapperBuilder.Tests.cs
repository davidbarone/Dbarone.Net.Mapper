using Dbarone.Net.Mapper;
using System.Collections;

public class MapperBuilderTests {


    [Theory]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(ArrayList), true)]
    [InlineData(typeof(IEnumerable), true)]
    [InlineData(typeof(List<>), true)]
    [InlineData(typeof(List<int>), true)]
    public void TestBuild_IEnumerablePropertySetCorrectly(Type type, bool expected) {
        MapperBuilder builder = new MapperBuilder(new MapperConfiguration()
            .RegisterType(type)
        );
        builder.Build();
        var buildType = builder.GetBuildTypeFor(type);
        Assert.Equal(expected, buildType.IsEnumerable);
    }
}