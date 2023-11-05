using Dbarone.Net.Mapper;

public class EnumerableMapperProvider
{

    [Fact]
    public void MapGenericLists()
    {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int[]>()
            .RegisterType<float[]>()
        );

        int[] ints = new int[] { 1, 2, 3, 4, 5 };
        var floats = mapper.Map<int[], float[]>(ints);
        Assert.Equal((float)1, floats.First());
    }
}