using Dbarone.Net.Mapper;

public class MemberwiseMapperProvider : IMapperProvider
{
    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        return from.MemberResolver.HasMembers && to.MemberResolver.HasMembers;
    }

    public MapperDelegate GetMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        // member-wise mapping
        // Get internal member names matching on source + destination
        IEnumerable<string> matchedMembers = to
            .Members
            .Where(mc => mc.Ignore == false)
            .Select(mc => mc.InternalMemberName).Intersect(
                from
                .Members
                .Where(mc => mc.Ignore == false)
                .Select(mc => mc.InternalMemberName)
            );

        // Get mapper delegates for each member mapping
        Dictionary<string, MapperDelegate> columnMappings = new Dictionary<string, MapperDelegate>();
        Dictionary<string, Getter> sourceMemberGetters = new Dictionary<string, Getter>();
        Dictionary<string, Getter> destinationMemberGetters = new Dictionary<string, Getter>();
        Dictionary<string, Setter> destinationMemberSetters = new Dictionary<string, Setter>();

        foreach (var member in matchedMembers)
        {
            var mSourceType = from.Members.First(m => m.MemberName == member).DataType;
            var mDestinationType = to.Members.First(m => m.MemberName == member).DataType;
            var memberMapper = builder.GetMapperFor(new SourceDestination(mSourceType, mDestinationType));
            columnMappings[member] = memberMapper;
            sourceMemberGetters[member] = from.MemberResolver.GetGetter(mSourceType, member, from.Options);
            destinationMemberGetters[member] = to.MemberResolver.GetGetter(mDestinationType, member, from.Options);
            destinationMemberSetters[member] = to.MemberResolver.GetSetter(mDestinationType, member, from.Options);
        }

        MapperDelegate mapping = (s, d) =>
            {
                var creator = to.MemberResolver.CreateInstance;
                var instance = creator(to.Type, null);

                foreach (var member in matchedMembers)
                {
                    var memberFrom = sourceMemberGetters[member](s);
                    var memberTo = destinationMemberGetters[member](d);
                    var memberMapper = columnMappings[member];
                    var memberMapped = memberMapper(memberFrom, memberTo);
                    destinationMemberSetters[member](instance, memberMapped);
                }
                return instance;
            };
        return mapping;
    }
}