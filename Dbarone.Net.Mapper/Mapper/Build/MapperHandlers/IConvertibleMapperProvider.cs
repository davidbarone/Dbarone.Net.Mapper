
using System.Reflection;

namespace Dbarone.Net.Mapper;

public class IConvertibleMapperProvider : IMapperProvider
{
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

    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        return typeof(IConvertible).IsAssignableFrom(from.Type) && this.ValidToTypes.Contains(to.Type);
    }

    public MapperDelegate GetMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        // Member types differ, but converter exists - convert then assign value to destination object.
        MapperDelegate mapping = (s, d) =>
        {
            var iconv = s as IConvertible;
            var converted = iconv.ToType(to.Type, null);
            return converted;
        };
        return mapping;
    }
}