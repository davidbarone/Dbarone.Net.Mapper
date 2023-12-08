using Dbarone.Net.Mapper;

namespace Dbarone.Net.Mapper;

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
    /// <summary>
    /// Creates a new <see cref="AssignableMapperOperator"/> instance. 
    /// </summary>
    /// <param name="builder">The <see cref="MapperBuilder"/> instance.</param>
    /// <param name="from">The From <see cref="BuildType"/> instance.</param>
    /// <param name="to">The To <see cref="BuildType"/> instance.</param>
    /// <param name="parent">An optional parent <see cref="MapperOperator"/> instance.</param>
    public AssignableMapperOperator(MapperBuilder builder, BuildType from, BuildType to, MapperOperator? parent = null) : base(builder, from, to, parent) { }

    /// <summary>
    /// Overrides the priority of the <see cref="AssignableMapperOperator"/> instance.
    /// </summary>
    public override int Priority => 10;

    /// <summary>
    /// The <see cref="AssignableMapperOperator"/> operator is able to map when the From object is assignable to the To type. 
    /// </summary>
    /// <returns>Returns true when the From object is assignable to the To type.</returns>
    public override bool CanMap()
    {
        return To.Type.IsAssignableFrom(From.Type);
    }

    /// <summary>
    /// Returns the <see cref="MapperDelegate"/> object that performs the mapping. 
    /// </summary>
    /// <returns>Returns the <see cref="MapperDelegate"/> object that performs the mapping.</returns>
    protected override object? MapInternal(object? source, object? target)
    {
        target = source;
        return target;
    }
}