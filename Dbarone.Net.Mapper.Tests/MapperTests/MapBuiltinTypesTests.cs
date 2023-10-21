using Dbarone.Net.Mapper;

public class MapBuiltinTypesTests {

    [Fact]
    public void Map_Int_ShouldMap() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int>()
        );

        var a = 123;
        var b = mapper.MapOne<int, int>(a);
        Assert.Equal(a, b);
    }
}