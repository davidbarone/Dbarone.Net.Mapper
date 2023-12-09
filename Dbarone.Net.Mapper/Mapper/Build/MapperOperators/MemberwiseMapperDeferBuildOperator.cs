using Dbarone.Net.Mapper;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Mapper operator that is able to map class and struct types on a member level, where the source type has a DeferBuild set to True (i.e. Dictionary and Dynamic types).
/// The operator build occurs when the first object is mapped. It is assumed that all subsequent objects have the same schema as the first object.
/// </summary>
public class MemberwiseMapperDeferBuildOperator : MapperOperator
{
    /// <summary>
    /// Creates a new <see cref="MemberwiseMapperDeferBuildOperator"/> instance.
    /// </summary>
    /// <param name="builder">The <see cref="MapperBuilder"/> instance.</param>
    /// <param name="sourceType">The source <see cref="BuildType"/> instance.</param>
    /// <param name="targetType">The target <see cref="BuildType"/> instance.</param>
    /// <param name="parent">An optional parent <see cref="MapperOperator"/> instance.</param>
    public MemberwiseMapperDeferBuildOperator(MapperBuilder builder, BuildType sourceType, BuildType targetType, MapperOperator? parent = null) : base(builder, sourceType, targetType, parent)
    {
    }

    /// <summary>
    /// GetChildren implementation for <see cref="MemberwiseMapperDeferBuildOperator"/>.
    /// </summary>
    /// <returns>Returns the children operators.</returns>
    /// <exception cref="MapperBuildException"></exception>
    protected override IDictionary<string, MapperOperator> GetChildren()
    {
        Dictionary<string, MapperOperator> children = new Dictionary<string, MapperOperator>();

        // member-wise mapping
        // Get internal member names matching on source + target
        var members = TargetType
                .Members
                .Where(mc => mc.Ignore == false)
                .Select(mc => mc.InternalMemberName).Intersect(
                    SourceType
                    .Members
                    .Where(mc => mc.Ignore == false)
                    .Select(mc => mc.InternalMemberName)
                );

        foreach (var member in members)
        {
            var mSourceType = SourceType.Members.First(m => m.MemberName == member).DataType;
            var mTargetType = TargetType.Members.First(m => m.MemberName == member).DataType;
            var memberMapperOperator = Builder.GetMapperOperator(new SourceTarget(mSourceType, mTargetType), this);
            children[member] = memberMapperOperator;
        }
        return children;
    }

    /// <summary>
    /// Overrides the priority of the <see cref="MemberwiseMapperDeferBuildOperator"/> instance.
    /// </summary>
    public override int Priority => 60;

    /// <summary>
    /// The <see cref="MemberwiseMapperDeferBuildOperator"/> operator is able to map when the source and target types have members, and the source type is a defer build type. 
    /// </summary>
    /// <returns>Returns true when the source and target types have members, and the source type is a defer build type.</returns>
    public override bool CanMap()
    {
        return SourceType.MemberResolver.HasMembers && TargetType.MemberResolver.HasMembers && SourceType.MemberResolver.DeferBuild;
    }

    private void EndPointValidation()
    {
        List<MapperBuildError> errors = new List<MapperBuildError>();
        if ((SourceType.Options.EndPointValidation & MapperEndPoint.Source) == MapperEndPoint.Source)
        {
            // check all source member rules map to target rules.
            var unmappedSourceMembers = SourceType
                .Members
                .Where(m => TargetType
                    .Members
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedSourceMembers)
            {
                errors.Add(new MapperBuildError(SourceType.Type, MapperEndPoint.Source, item.MemberName, "Source end point validation enabled, but source member is not mapped to target."));
            }
        }

        if ((TargetType.Options.EndPointValidation & MapperEndPoint.Target) == MapperEndPoint.Target)
        {
            // check all source member rules map to target rules.
            var unmappedTargetMembers = TargetType
                .Members
                .Where(m => SourceType
                    .Members
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedTargetMembers)
            {
                errors.Add(new MapperBuildError(TargetType.Type, MapperEndPoint.Target, item.MemberName, "Target end point validation enabled, but target member is not mapped from source."));
            }
        }
        if (errors.Any())
        {
            throw new MapperBuildException("Error occurred during end point validation. See inner errors collection for more information.", errors);
        }
    }

    /// <summary>
    /// Mapping implementation for <see cref="MemberwiseMapperDeferBuildOperator"/> type. 
    /// </summary>
    /// <param name="source">The source object.</param>
    /// <param name="target">The optional target object.</param>
    /// <returns>Returns a mapped object.</returns>
    /// <exception cref="MapperBuildException">Returns a <see cref="MapperBuildException"/> in the event of any failure to map the object.</exception>
    protected override object? MapInternal(object? source, object? target)
    {
        EndPointValidation();

        var creator = TargetType.MemberResolver.CreateInstance(TargetType.Type, null);
        var instance = creator();

        foreach (var member in Children.Keys)
        {
            var sourceGetter = SourceType.MemberResolver.GetGetter(SourceType.Type, member, SourceType.Options);
            var targetGetter = TargetType.MemberResolver.GetGetter(TargetType.Type, member, TargetType.Options);
            var targetSetter = TargetType.MemberResolver.GetSetter(TargetType.Type, member, TargetType.Options);

            var memberFrom = sourceGetter(source);
            var memberTo = targetGetter(instance);
            var memberOperator = this.Children[member];
            var memberMapped = memberOperator.Map(memberFrom, memberTo);
            targetSetter(instance, memberMapped);
        }
        return instance;
    }
}