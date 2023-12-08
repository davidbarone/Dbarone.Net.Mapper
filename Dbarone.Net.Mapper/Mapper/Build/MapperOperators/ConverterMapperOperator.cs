
using System.Reflection;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Maps types based on the presence of a special converter function added to configuration.
/// </summary>
public class ConverterMapperOperator : MapperOperator
{
    /// <summary>
    /// Creates a new <see cref="ConvertibleMapperOperator"/> instance. This operator uses a converter function provided in configuration to map. 
    /// </summary>
    /// <param name="builder">The <see cref="MapperBuilder"/> instance.</param>
    /// <param name="from">The From <see cref="BuildType"/> instance.</param>
    /// <param name="to">The To <see cref="BuildType"/> instance.</param>
    /// <param name="parent">An optional parent <see cref="MapperOperator"/> instance.</param>
    public ConverterMapperOperator(MapperBuilder builder, BuildType from, BuildType to, MapperOperator? parent = null) : base(builder, from, to, parent) { }

    /// <summary>
    /// Overrides the priority of the <see cref="ConverterMapperOperator"/> instance.
    /// </summary>
    public override int Priority => 30;

    /// <summary>
    /// The <see cref="ConverterMapperOperator"/> operator is able to map when a converter function exists between the from and to types. Note that in this case the operator does not recursively map the members. 
    /// </summary>
    /// <returns>Returns true when the From object is assignable to the To type.</returns>
    public override bool CanMap()
    {
        SourceDestination sourceDestination = new SourceDestination(From.Type, To.Type);
        return Builder.Configuration.Config.Converters.ContainsKey(sourceDestination);
    }

    /// <summary>
    /// Returns the <see cref="MapperDelegate"/> object that performs the mapping. 
    /// </summary>
    /// <returns>Returns the <see cref="MapperDelegate"/> object that performs the mapping.</returns>
    protected override object? MapInternal(object? source, object? target)
    {
        SourceDestination sourceDestination = new SourceDestination(From.Type, To.Type);
        var converter = Builder.Configuration.Config.Converters[sourceDestination];

        // Member types differ, but converter exists - convert then assign value to destination object.
        var converted = converter.Convert(source);
        return converted;
    }
}