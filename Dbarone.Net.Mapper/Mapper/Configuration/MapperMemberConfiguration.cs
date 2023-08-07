namespace Dbarone.Net.Mapper;
using Dbarone.Net.Extensions;

using System;
using System.Reflection;

/// <summary>
/// Defines the rules for a single member mapping.
/// </summary>
public class MapperMemberConfiguration
{
    /// <summary>
    /// Member name for the mapping rule.
    /// </summary>
    public string MemberName { get; set; } = default!;

    /// <summary>
    /// Member data type of the member.
    /// </summary>
    public Type DataType { get; set; } = default!;

    /// <summary>
    /// The internal member name. Mapping from source to destination is done via matching internal names.
    /// </summary>
    public string InternalMemberName { get; set; } = default!;

    public void SetInternalMemberName(IMemberRenameStrategy? memberRenameStrategy = null, bool force = false)
    {
        // only modify internal name if not already pre-set.
        if (this.InternalMemberName.IsNullOrWhiteSpace() || force)
        {
            if (memberRenameStrategy != null)
            {
                var newName = memberRenameStrategy.RenameMember(this.MemberName);
                this.InternalMemberName = newName;
            }
            else
            {
                // default - make internal name = member name
                this.InternalMemberName = this.MemberName;
            }
        }
    }

    /// <summary>
    /// Set to true to ignore this member in the mapping configuration.
    /// </summary>
    internal bool? Ignore { get; set; } = null;

    /// <summary>
    /// Delegate method to get the value from the instance. Returns a <see cref="Getter" /> object.
    /// </summary>
    public Getter Getter { get; set; } = default!;

    /// <summary>
    /// Delegate method to set the value to the instance. Returns a <see cref="Setter" /> object.
    /// </summary>
    public Setter Setter { get; set; } = default!;

    /// <summary>
    /// Set to true if a calculation.
    /// </summary>
    public bool IsCalculation => Calculation != null;

    /// <summary>
    /// Used to create a custom calculation. Must be a <see cref="ITypeConverter" /> type.
    /// </summary>
    public ITypeConverter Calculation { get; set; } = default!;
}
