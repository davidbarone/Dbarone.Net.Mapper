using Dbarone.Net.Mapper;

public class MatchingTypeMapperProvider : IMapperProvider
{
    public bool CanCreateMapFor(Type from, Type to)
    {
        return from == to;
    }

    public MapperDelegate GetMapFor(Type from, Type to, MapperBuilder builder)
    {
        MapperDelegate mapping = (s, d) =>
            {
                d = s;
                return d;
            };

        return mapping;
    }
}