using Dbarone.Net.Mapper;

public class MatchingTypeMapperProvider : IMapperProvider
{
    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        return from == to;
    }

    public MapperDelegate GetMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        MapperDelegate mapping = (s, d) =>
            {
                d = s;
                return d;
            };

        return mapping;
    }
}