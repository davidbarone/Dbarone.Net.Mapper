using Dbarone.Net.Mapper;

public class MatchingTypeMapperProvider {

    [Fact]
    public void Map_Int_Should_Map() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int>()
        );

        var a = 123;
        var b = mapper.MapOne<int, int>(a);
        Assert.Equal(a, b);
    }
}