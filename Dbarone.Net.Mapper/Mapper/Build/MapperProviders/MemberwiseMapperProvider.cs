using Dbarone.Net.Mapper;

public class MemberwiseMapperProvider : IMapperProvider
{
    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        return from.MemberResolver.HasMembers && to.MemberResolver.HasMembers;
    }

    private void EndPointValidation(BuildType from, BuildType to, string path, List<MapperBuildError> errors)
    {
        if ((from.Options.EndPointValidation & MapperEndPoint.Source) == MapperEndPoint.Source)
        {
            // check all source member rules map to destination rules.
            var unmappedSourceMembers = from
                .Members
                .Where(m => to
                    .Members
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedSourceMembers)
            {
                errors.Add(new MapperBuildError(from.Type, MapperEndPoint.Source, path, item.MemberName, "Source end point validation enabled, but source member is not mapped to destination."));
            }
        }

        if ((to.Options.EndPointValidation & MapperEndPoint.Destination) == MapperEndPoint.Destination)
        {
            // check all source member rules map to destination rules.
            var unmappedDestinationMembers = to
                .Members
                .Where(m => from
                    .Members
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedDestinationMembers)
            {
                errors.Add(new MapperBuildError(to.Type, MapperEndPoint.Destination, path, item.MemberName, "Destination end point validation enabled, but destination member is not mapped from source."));
            }
        }
    }

    public MapperDelegate GetMapFor(BuildType from, BuildType to, MapperBuilder builder, string path, List<MapperBuildError> errors)
    {
        EndPointValidation(from, to, path, errors);

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
            var memberMapper = builder.GetMapper(new SourceDestination(mSourceType, mDestinationType));
            columnMappings[member] = memberMapper;
            sourceMemberGetters[member] = from.MemberResolver.GetGetter(from.Type, member, from.Options);
            destinationMemberGetters[member] = to.MemberResolver.GetGetter(to.Type, member, from.Options);
            destinationMemberSetters[member] = to.MemberResolver.GetSetter(to.Type, member, from.Options);
        }

        MapperDelegate mapping = (s, d) =>
            {
                var creator = to.MemberResolver.CreateInstance(to.Type,null);
                var instance = creator();

                foreach (var member in matchedMembers)
                {
                    var memberFrom = sourceMemberGetters[member](s);
                    var memberTo = destinationMemberGetters[member](instance);
                    var memberMapper = columnMappings[member];
                    var memberMapped = memberMapper(memberFrom, memberTo);
                    destinationMemberSetters[member](instance, memberMapped);
                }
                return instance;
            };
        return mapping;
    }
}