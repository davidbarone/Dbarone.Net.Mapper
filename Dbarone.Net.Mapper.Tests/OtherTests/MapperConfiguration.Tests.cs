using Dbarone.Net.Mapper;

public class MapperConfigurationTests
{
    [Fact]
    public void TestAutoRegisterTypes()
    {
        // Given
        var mapper = new ObjectMapper(
            new MapperConfiguration()
            .SetAutoRegisterTypes(true));               // No need to register individual types here.

        int[] ints = new int[] { 1, 2, 3, 4, 5 };
        var floats = mapper.Map<int[], float[]>(ints);  // Will automatically register necessary types here
        Assert.Equal((float)1, floats.First());
    }
}