using Dbarone.Net.Mapper;

public interface IMapperHandler {

    public bool CanCreateMapsFor(Type from, Type to);

    public MapperDelegate[] GetMapsFor(Type from, Type to, MapperBuilder builder)
}
