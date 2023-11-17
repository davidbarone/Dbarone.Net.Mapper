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
public class AssignableMapperOperator : MapperOperator
{
    public AssignableMapperOperator(MapperBuilder builder, BuildType from, BuildType to) : base(builder, from, to) { }

    public override int Priority => 10;
    
    public override bool CanMap()
    {
        return To.Type.IsAssignableFrom(From.Type);
    }

    public override MapperDelegate GetMap()
    {
        MapperDelegate mapping = (s, d) =>
            {
                d = s;
                return d;
            };

        return mapping;
    }
}