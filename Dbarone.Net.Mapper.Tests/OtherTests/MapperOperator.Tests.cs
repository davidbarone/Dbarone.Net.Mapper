namespace Dbarone.Net.Mapper;

public class MapperOperatorTests {
       [Fact]
    public void GetOperator1() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int>()
            .RegisterType<float>()
        );

        var op = mapper.GetOperator<int, float>();
        Assert.NotNull(op);
    }
}