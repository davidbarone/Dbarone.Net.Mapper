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
        // Get internal member names matching on source + destination
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
            var mDestinationType = To.Members.First(m => m.MemberName == member).DataType;
            var memberMapper = Builder.GetMapper(new SourceDestination(mSourceType, mDestinationType), this);
            children[member] = memberMapper;
        }
        return children;
    }

    /// <summary>
    /// Overrides the priority of the <see cref="MemberwiseMapperDeferBuildOperator"/> instance.
    /// </summary>
    public override int Priority => 60;

    public override bool CanMap()
    {
        return From.MemberResolver.HasMembers && To.MemberResolver.HasMembers && From.MemberResolver.DeferBuild;
    }

    private void EndPointValidation()
    {
        List<MapperBuildError> errors = new List<MapperBuildError>();
        if ((From.Options.EndPointValidation & MapperEndPoint.Source) == MapperEndPoint.Source)
        {
            // check all source member rules map to destination rules.
            var unmappedSourceMembers = From
                .Members
                .Where(m => To
                    .Members
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedSourceMembers)
            {
                errors.Add(new MapperBuildError(From.Type, MapperEndPoint.Source, item.MemberName, "Source end point validation enabled, but source member is not mapped to destination."));
            }
        }

        if ((To.Options.EndPointValidation & MapperEndPoint.Destination) == MapperEndPoint.Destination)
        {
            // check all source member rules map to destination rules.
            var unmappedDestinationMembers = To
                .Members
                .Where(m => From
                    .Members
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedDestinationMembers)
            {
                errors.Add(new MapperBuildError(To.Type, MapperEndPoint.Destination, item.MemberName, "Destination end point validation enabled, but destination member is not mapped from source."));
            }
        }
        if (errors.Any())
        {
            throw new MapperBuildException("Error occurred during end point validation. See inner errors collection for more information.", errors);
        }
    }

    public override MapperDelegate GetMap()
    {
        EndPointValidation();

        MapperDelegate mapping = (s, d) =>
            {
                var creator = To.MemberResolver.CreateInstance(To.Type, null);
                var instance = creator();

                foreach (var member in Children.Keys)
                {
                    var memberFrom = From.MemberResolver.GetGetter(From.Type, member, From.Options)(s);
                    var memberTo = To.MemberResolver.GetGetter(To.Type, member, From.Options)(instance);
                    var memberMapper = this.Children[member].GetMap();
                    var memberMapped = memberMapper(memberFrom, memberTo);
                    To.MemberResolver.GetSetter(To.Type, member, From.Options)(instance, memberMapped);
                }
                return instance;
            };
        return mapping;
    }
}