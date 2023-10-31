using Dbarone.Net.Mapper;

public interface IMapperProvider {

    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder);

    public MapperDelegate GetMapFor(BuildType from, BuildType to, MapperBuilder builder);
}
