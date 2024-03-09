using Dbarone.Net.Mapper;

/// <summary>
/// Extension methods for mapping objects.
/// </summary>
public static class MapperExtensions
{
    /// <summary>
    /// Maps the current object to another type. using automatic type registration.
    /// </summary>
    /// <typeparam name="T">The type to map the current object to.</typeparam>
    /// <param name="obj">The object to map.</param>
    /// <returns>Returns a mapped object.</returns>
    public static T MapTo<T>(this object obj)
    {
        ObjectMapper mapper = new ObjectMapper(new MapperConfiguration().SetAutoRegisterTypes(true));
        var toType = typeof(T);
        return (T)mapper.Map(toType, obj)!;
    }

    /// <summary>
    /// Maps the current object to another type. using automatic type registration.
    /// </summary>
    /// <param name="obj">The object to map.</param>
    /// <param name="toType">The type to map to.</param>
    /// <returns>Returns a mapped object.</returns>
    public static object MapTo(this object obj, Type toType)
    {
        ObjectMapper mapper = new ObjectMapper(new MapperConfiguration().SetAutoRegisterTypes(true));
        return mapper.Map(toType, obj)!;
    }
}