namespace Dbarone.Net.Mapper;

using System;
using System.Reflection;

/// <summary>
/// Defines the rules for a single member mapping.
/// </summary>
public class MemberRule
{
    /// <summary>
    /// Member name for the mapping rule.
    /// </summary>
    public string MemberName { get; set; } = default!;

    /// <summary>
    /// Member data type.
    /// </summary>
    public Type DataType { get; set; } = default!;

    public string MemberOverrideName { get; set; } = default!;

    /// <summary>
    /// Delegate method to get the value from the instance.
    /// </summary>
    public Getter Getter { get; set; } = default!;

    /// <summary>
    /// Delegate method to set the value to the instance.
    /// </summary>
    public Setter Setter { get; set; } = default!;
}
