using Dbarone.Net.Mapper;

public class ImplcitOperatorTests {

    [Fact]
    public void Map_Int_To_Float_Should_Map() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int>()
            .RegisterType<float>()
        );

        var a = 123;
        var b = mapper.MapOne<int, float>(a);
        Assert.Equal((float)a, b);
    }
}