using Dbarone.Net.Mapper;

public class MatchingTypeMapperHandler : IMapperHandler
{
    public bool CanCreateMapsFor(Type from, Type to)
    {
        return from == to;
    }

    public MapperDelegate[] GetMapsFor(Type from, Type to, MapperBuilder builder)
    {
        MapperDelegate mapping = (s, d) =>
            {
                d = s;
                return d;
            };

        return new MapperDelegate[] { mapping };
    }
}