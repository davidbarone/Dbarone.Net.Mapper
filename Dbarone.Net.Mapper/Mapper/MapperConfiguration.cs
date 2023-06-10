namespace Dbarone.Net.Mapper;

/// <summary>
/// 
/// </summary>
public class MapperConfiguration {
    private IList<MapperBuilder<Type, Type>> ObjectMappers = new List<MapperBuilder<Type, Type>>();

    public MapperConfiguration Create()
    {
        return new MapperConfiguration();
    }

    public MapperConfiguration Add()
    {
        return null;
    }
}