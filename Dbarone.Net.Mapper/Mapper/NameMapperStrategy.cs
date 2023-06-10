namespace Dbarone.Net.Mapper;
using Dbarone.Net.Extensions;

/// <summary>
/// Default mapper strategy. Maps properties where the names match.
/// </summary>
public class NameMapperStrategy : IMapperStrategy
{
    /// <summary>
    /// Maps source to target types based on matching property names.
    /// </summary>
    /// <param name="T">The source type.</param>
    /// <param name="U">The target type.</param>
    /// <returns>A list of mapper rules.</returns>
    public IList<PropertyMap> MapTypes(Type T, Type U)
    {
        var sourceProperties = T.GetProperties();
        var targetProperties = U.GetProperties();

        var properties = (from s in sourceProperties
                          from t in targetProperties
                          where s.Name == t.Name &&
                              s.CanRead &&
                              t.CanWrite &&
                              s.PropertyType.IsPublic &&
                              t.PropertyType.IsPublic &&
                              (s.PropertyType == t.PropertyType ||
                              s.PropertyType.GetElementType() == t.PropertyType.GetElementType()) &&
                              (
                                  (s.PropertyType.IsValueType &&
                                  t.PropertyType.IsValueType
                                  ) ||
                                  (s.PropertyType == typeof(string) ||
                                  t.PropertyType == typeof(string)
                                  ) ||
                                  (
                                      // source or target is nullable type
                                      (s.PropertyType.IsNullable() && s.PropertyType.GetNullableUnderlyingType()==t.PropertyType) ||
                                      (t.PropertyType.IsNullable() && t.PropertyType.GetNullableUnderlyingType()==s.PropertyType)
                                  )
                              )
                          select new PropertyMap
                          {
                              SourceProperty = s,
                              TargetProperty = t
                          }).ToList();

        return properties;
    }
}