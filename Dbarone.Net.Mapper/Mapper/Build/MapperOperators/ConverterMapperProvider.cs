
using System.Reflection;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Maps types based on the presence of a special converter function added to configuration.
/// </summary>
public class ConverterMapperOperator : MapperOperator
{
    public ConverterMapperOperator(MapperBuilder builder, BuildType from, BuildType to) : base(builder, from, to) { }

    public override int Priority => 30;

    public override bool CanMap()
    {
        SourceDestination sourceDestination = new SourceDestination(From.Type, To.Type);
        return Builder.Configuration.Config.Converters.ContainsKey(sourceDestination);
    }

    public override MapperDelegate GetMap()
    {
        SourceDestination sourceDestination = new SourceDestination(From.Type, To.Type);
        var converter = Builder.Configuration.Config.Converters[sourceDestination];

        // Member types differ, but converter exists - convert then assign value to destination object.
        MapperDelegate mapping = (s, d) =>
        {
            var converted = converter.Convert(s);
            return converted;
        };
        return mapping;
    }
}