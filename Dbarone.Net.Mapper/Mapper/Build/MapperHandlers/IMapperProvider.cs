using Dbarone.Net.Mapper;

public interface IMapperProvider {

    public bool CanCreateMapFor(Type from, Type to);

    public MapperDelegate GetMapFor(Type from, Type to, MapperBuilder builder);
}
