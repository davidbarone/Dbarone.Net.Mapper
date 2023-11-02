
using System.Reflection;

namespace Dbarone.Net.Mapper;

public class ConverterMapperProvider : IMapperProvider
{
    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        SourceDestination sourceDestination = new SourceDestination(from.Type, to.Type);
        return builder.Configuration.Config.Converters.ContainsKey(sourceDestination);
    }

    public MapperDelegate GetMapFor(BuildType from, BuildType to, MapperBuilder builder, string path, List<MapperBuildError> errors)
    {
        SourceDestination sourceDestination = new SourceDestination(from.Type, to.Type);
        var converter = builder.Configuration.Config.Converters[sourceDestination];

        // Member types differ, but converter exists - convert then assign value to destination object.
        MapperDelegate mapping = (s, d) =>
        {
            var converted = converter.Convert(s);
            return converted;
        };
        return mapping;
    }
}