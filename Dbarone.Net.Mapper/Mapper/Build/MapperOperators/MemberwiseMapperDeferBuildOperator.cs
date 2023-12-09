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
    /// <param name="from">The From <see cref="BuildType"/> instance.</param>
    /// <param name="to">The To <see cref="BuildType"/> instance.</param>
    /// <param name="parent">An optional parent <see cref="MapperOperator"/> instance.</param>
    public MemberwiseMapperDeferBuildOperator(MapperBuilder builder, BuildType from, BuildType to, MapperOperator? parent = null) : base(builder, from, to, parent)
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
        var members = To
                .Members
                .Where(mc => mc.Ignore == false)
                .Select(mc => mc.InternalMemberName).Intersect(
                    From
                    .Members
                    .Where(mc => mc.Ignore == false)
                    .Select(mc => mc.InternalMemberName)
                );

        foreach (var member in members)
        {
            var mSourceType = From.Members.First(m => m.MemberName == member).DataType;
            var mTargetType = To.Members.First(m => m.MemberName == member).DataType;
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
        return From.MemberResolver.HasMembers && To.MemberResolver.HasMembers && From.MemberResolver.DeferBuild;
    }

    private void EndPointValidation()
    {
        List<MapperBuildError> errors = new List<MapperBuildError>();
        if ((From.Options.EndPointValidation & MapperEndPoint.Source) == MapperEndPoint.Source)
        {
            // check all source member rules map to target rules.
            var unmappedSourceMembers = From
                .Members
                .Where(m => To
                    .Members
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedSourceMembers)
            {
                errors.Add(new MapperBuildError(From.Type, MapperEndPoint.Source, item.MemberName, "Source end point validation enabled, but source member is not mapped to target."));
            }
        }

        if ((To.Options.EndPointValidation & MapperEndPoint.Target) == MapperEndPoint.Target)
        {
            // check all source member rules map to target rules.
            var unmappedTargetMembers = To
                .Members
                .Where(m => From
                    .Members
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedTargetMembers)
            {
                errors.Add(new MapperBuildError(To.Type, MapperEndPoint.Target, item.MemberName, "Target end point validation enabled, but target member is not mapped from source."));
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

        var creator = To.MemberResolver.CreateInstance(To.Type, null);
        var instance = creator();

        foreach (var member in Children.Keys)
        {
            var sourceGetter = From.MemberResolver.GetGetter(From.Type, member, From.Options);
            var targetGetter = To.MemberResolver.GetGetter(To.Type, member, To.Options);
            var targetSetter = To.MemberResolver.GetSetter(To.Type, member, To.Options);

            var memberFrom = sourceGetter(source);
            var memberTo = targetGetter(instance);
            var memberOperator = this.Children[member];
            var memberMapped = memberOperator.Map(memberFrom, memberTo);
            targetSetter(instance, memberMapped);
        }
        return instance;
    }
}