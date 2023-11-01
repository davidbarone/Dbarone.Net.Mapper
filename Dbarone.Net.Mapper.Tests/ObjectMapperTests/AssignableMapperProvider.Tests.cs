using Dbarone.Net.Mapper;

public class AssignableMapperProvider {

    [Fact]
    public void MapSameTypes() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int>()
        );

        var a = 123;
        var b = mapper.MapOne<int, int>(a);
        Assert.Equal(a, b);
    }

    [Fact]
    public void MapValueToNullable() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int>()
            .RegisterType<int?>()
        );

        var a = 123;
        var b = mapper.MapOne<int, int?>(a);
        Assert.Equal(a, b.Value);
    }

}