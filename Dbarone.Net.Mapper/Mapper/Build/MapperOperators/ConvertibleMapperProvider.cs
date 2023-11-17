
using System.Reflection;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Maps types that support the IConvertible interface. This is typically for built-in types.
/// </summary>
public class ConvertibleMapperOperator : MapperOperator
{
    public ConvertibleMapperOperator(MapperBuilder builder, BuildType from, BuildType to) : base(builder, from, to) { }

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

    public override bool CanMap()
    {
        return typeof(IConvertible).IsAssignableFrom(From.Type) && this.ValidToTypes.Contains(To.Type);
    }

    public override MapperDelegate GetMap()
    {
        // Member types differ, but converter exists - convert then assign value to destination object.
        MapperDelegate mapping = (s, d) =>
        {
            var iconv = s as IConvertible;
            var converted = iconv.ToType(To.Type, null);
            return converted;
        };
        return mapping;
    }
}