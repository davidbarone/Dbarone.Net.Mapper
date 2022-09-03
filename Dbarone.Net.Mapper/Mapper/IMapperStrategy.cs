namespace Dbarone.Net.Mapper;

public interface IMapperStrategy
{
    public IList<PropertyMap> MapTypes(Type T, Type U);
}