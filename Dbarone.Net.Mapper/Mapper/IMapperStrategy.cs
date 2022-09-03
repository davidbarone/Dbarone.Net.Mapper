namespace Dbarone.Net.Mapper;

/// <summary>
/// Mapper strategy interface. All mapper strategies must implement this interface.
/// </summary>
public interface IMapperStrategy
{
    /// <summary>
    /// A mapper strategy must implement the MapTypes method.
    /// </summary>
    /// <param name="T">The source type.</param>
    /// <param name="U">The target type.</param>
    /// <returns>A list of PropertyMap rules defining the mapping relationship between the two types.</returns>
    public IList<PropertyMap> MapTypes(Type T, Type U);
}