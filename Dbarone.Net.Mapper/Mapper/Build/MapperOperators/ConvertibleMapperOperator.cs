
using System.Reflection;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Maps types that support the IConvertible interface. This is typically for built-in types.
/// </summary>
public class ConvertibleMapperOperator : MapperOperator
{
    /// <summary>
    /// Creates a new instance of <see cref="ConvertibleMapperOperator"/>.
    /// </summary>
    /// <param name="builder">The <see cref="MapperBuilder"/> instance.</param>
    /// <param name="from">The From <see cref="BuildType"/> instance.</param>
    /// <param name="to">The To <see cref="BuildType"/> instance.</param>
    /// <param name="parent">An optional parent <see cref="MapperOperator"/> instance.</param>
    public ConvertibleMapperOperator(MapperBuilder builder, BuildType from, BuildType to, MapperOperator? parent = null) : base(builder, from, to, parent) { }

    /// <summary>
    /// Overrides the priority of the <see cref="ConvertibleMapperOperator"/> instance.
    /// </summary>
    public override int Priority => 20;

    private Type[] ValidToTypes = new Type[] {
            typeof(Byte),
            typeof(char),
            typeof(DateTime),
            typeof(decimal),
            typeof(double),
            typeof(Int16),
            typeof(Int32),
            typeof(Int64),
            typeof(sbyte),
            typeof(Single),
            typeof(string),
            typeof(UInt16),
            typeof(UInt32),
            typeof(UInt64)
        };

    /// <summary>
    /// The <see cref="ConvertibleMapperOperator"/> operator is able to map when the From type implements the IConvertible interface, and can convert to the To type. 
    /// </summary>
    /// <returns>Returns true when the From type implements the IConvertible interface, and can convert to the To type.</returns>
    public override bool CanMap()
    {
        return typeof(IConvertible).IsAssignableFrom(From.Type) && this.ValidToTypes.Contains(To.Type);
    }

    /// <summary>
    /// Returns the <see cref="MapperDelegate"/> object that performs the mapping. 
    /// </summary>
    /// <returns>Returns the <see cref="MapperDelegate"/> object that performs the mapping.</returns>
    public override MapperDelegate GetMap()
    {
        // Member types differ, but converter exists - convert then assign value to destination object.
        MapperDelegate mapping = (s, d) =>
        {
            var iconv = s as IConvertible;
            if (iconv == null)
            {
                throw new MapperRuntimeException("Object does not support IConvertible interface.");
            }
            var converted = iconv.ToType(To.Type, null);
            return converted;
        };
        return mapping;
    }
}