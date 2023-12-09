using Dbarone.Net.Mapper;

namespace Dbarone.Net.Mapper;

public class MemberwiseMapperOperator : MapperOperator
{
    public MemberwiseMapperOperator(MapperBuilder builder, BuildType from, BuildType to, MapperOperator parent = null) : base(builder, from, to, parent)
    {

    }

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

    public override int Priority => 70;

    public override bool CanMap()
    {
        return From.MemberResolver.HasMembers && To.MemberResolver.HasMembers;
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

    protected override object? MapInternal(object? source, object? target)
    {
        EndPointValidation();

        var creator = To.MemberResolver.CreateInstance(To.Type, null);
        var instance = creator();

        foreach (var member in Children.Keys)
        {
            var memberFrom = From.MemberResolver.GetGetter(From.Type, member, From.Options)(source);
            var memberTo = To.MemberResolver.GetGetter(To.Type, member, From.Options)(instance);
            var memberOperator = this.Children[member];
            var memberMapped = memberOperator.Map(memberFrom, memberTo);
            To.MemberResolver.GetSetter(To.Type, member, From.Options)(instance, memberMapped);
        }
        return instance;
    }
}