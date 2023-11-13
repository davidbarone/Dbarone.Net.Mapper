using Dbarone.Net.Mapper;

/// <summary>
/// Maps source type to destination type if source is assignable to destination. Occurs under following scenarios:
/// 1. Source and destination are the same type.
/// 2. Source type is derived directly or indirectly from destination type.
/// 3. Destination type is an interface which source implements.
/// 4. Source is a generic type parameter, and destination represents one of the constraints of source.
/// 5. Source represents a value type, and destination represents a Nullable version of it.
///
/// For more information, refer to: https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignablefrom?view=net-7.0 
/// </summary>
public class AssignableMapperProvider : IMapperProvider
{
    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        return to.Type.IsAssignableFrom(from.Type);
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