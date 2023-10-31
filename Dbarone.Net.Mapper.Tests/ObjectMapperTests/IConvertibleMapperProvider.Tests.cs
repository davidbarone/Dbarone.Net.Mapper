using Dbarone.Net.Mapper;

public class IConvertibleMapperProviderTests {

    [Fact]
    public void Map_Int_To_Float_Should_Map() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int>()
            .RegisterType<long>()
        );

        var a = 123;
        var b = mapper.MapOne<int, long>(a);
        Assert.Equal((long)a, b);
    }
}