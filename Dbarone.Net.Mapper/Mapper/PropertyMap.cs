namespace Dbarone.Net.Mapper;
using System.Reflection;

/// <summary>
/// Defines the mapping link / relationship between properties of two different types.
/// </summary>
public class PropertyMap
{
    /// <summary>
    /// The property in the source type.
    /// </summary>
    public PropertyInfo SourceProperty { get; set; } = default!;
    
    /// <summary>
    /// The property in the target type.
    /// </summary>
    public PropertyInfo TargetProperty { get; set; } = default!;
}