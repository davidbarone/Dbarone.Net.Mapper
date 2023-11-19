namespace Dbarone.Net.Mapper;

public class MapperOperatorTests {
       [Fact]
    public void GetMap1() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int>()
            .RegisterType<float>()
        );

        var map = mapper.GetMap<int, float>();
    }

}