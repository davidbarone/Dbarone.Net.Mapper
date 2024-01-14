using Microsoft.VisualBasic;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Stores all configuration information.
/// </summary>
public class Config
{
    /// <summary>
    /// If set to true, types will be automatically registered on demand with
    /// default configuration if not registered in advance
    /// </summary>
    public bool AutoRegisterTypes { get; set; }

    /// <summary>
    /// List of default resolvers used to provide mapper services for types.
    /// </summary>
    public IList<IMemberResolver> DefaultResolvers
    {
        get
        {
            IList<IMemberResolver> resolvers = new List<IMemberResolver>();

            // Add core resolvers - note order is important. Types check member resolvers in order below.
            if (!resolvers.Select(r => r.GetType()).Contains(typeof(DynamicMemberResolver)))
            {
                resolvers.Add(new DynamicMemberResolver());
            }
            if (!resolvers.Select(r => r.GetType()).Contains(typeof(StructMemberResolver)))
            {
                resolvers.Add(new StructMemberResolver());
            }
            if (!resolvers.Select(r => r.GetType()).Contains(typeof(DictionaryMemberResolver)))
            {
                resolvers.Add(new DictionaryMemberResolver());
            }
            if (!resolvers.Select(r => r.GetType()).Contains(typeof(ClassMemberResolver)))
            {
                resolvers.Add(new ClassMemberResolver());
            }
            if (!resolvers.Select(r => r.GetType()).Contains(typeof(InterfaceMemberResolver)))
            {
                resolvers.Add(new InterfaceMemberResolver());
            }
            return resolvers;
        }
    }

    /// <summary>
    /// List of resolvers used to provide mapper services for types.
    /// </summary>
    public IList<IMemberResolver> Resolvers { get; set; } = new List<IMemberResolver>();

    /// <summary>
    /// Registered types that can participate in mappings.
    /// </summary>
    public IDictionary<Type, ConfigType> Types { get; set; } = new Dictionary<Type, ConfigType>();

    /// <summary>
    /// Calculated members allow for dervived members to be added to types. 
    /// </summary>
    public IList<ConfigCalculation> Calculations { get; set; } = new List<ConfigCalculation>();

    /// <summary>
    /// Individual member include / exclude rules.
    /// </summary>
    public IList<ConfigMemberInclusion> MemberInclusions { get; set; } = new List<ConfigMemberInclusion>();

    /// <summary>
    /// Converters enable a member to be converted from one type to another.
    /// </summary>
    public IDictionary<SourceTarget, ITypeConverter> Converters { get; set; } = new Dictionary<SourceTarget, ITypeConverter>();

    /// <summary>
    /// Member filter rules provide a function to determine which members to include or exclude from mapping.
    /// </summary>
    public IDictionary<Type, MemberFilterDelegate> MemberFilters { get; set; } = new Dictionary<Type, MemberFilterDelegate>();

    /// <summary>
    /// Member renames are functions which rename members.
    /// </summary>
    public IList<ConfigMemberRename> MemberRenames { get; set; } = new List<ConfigMemberRename>();
}