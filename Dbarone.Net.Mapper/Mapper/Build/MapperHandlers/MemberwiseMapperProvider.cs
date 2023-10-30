using Dbarone.Net.Mapper;

public class MemberwiseMapperProvider : IMapperProvider
{
    public bool CanCreateMapFor(BuildType from, BuildType to)
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

        
        MapperDelegate mapping = (s, d) =>
            {
                var creator = to.MemberResolver.CreateInstance;
                var instance = creator(null);

            foreach (var member in matchedMembers)
            {
                var sourceMemberBuild = from.Members.First(mc => mc.InternalMemberName.Equals(member));
                var destinationMemberBuild = to.Members.First(mc => mc.InternalMemberName.Equals(member));
                
                
                
                
                var sourceMemberType = sourceMemberBuild.DataType;
                var destinationMemberType = destinationMemberBuild.DataType;

                if (sourceMemberType == destinationMemberType)
                {
                    // member types the same - do simple assignment of value to destination object.
                    MapperDelegate mapping = (s, d) =>
                    {
                        destinationMemberBuild.Setter(d, sourceMemberBuild.Getter(s));
                        return d;
                    };
                    mappings.Add(mapping);
                }
                else if (this.Configuration.Config.Converters.ContainsKey(sourceDestination))
                {
                    // Member types differ, but converter exists - convert then assign value to destination object.
                    MapperDelegate mapping = (s, d) =>
                    {
                        var converter = this.Configuration.Config.Converters[sourceDestination];
                        var converted = converter.Convert(sourceMemberBuild.Getter(s));
                        destinationMemberBuild.Setter(d, converted);
                        return d;
                    };
                    mappings.Add(mapping);
                }
                else if (this.Configuration.Config.Types.Keys.Contains(destinationMemberType) && this.Configuration.Config.Types.Keys.Contains(sourceMemberType))
                {
                    // Member types differ, but mapping configuration exists for types
                    // create mapping rules recursively.
                    Build(sourceDestination, new SourceDestination(sourceMemberType, destinationMemberType), "", errors);
                }
                else
                {
                    errors.Add(new MapperBuildError(sourceBuild.Type, MapperEndPoint.Source, path, member, "Cannot create mapping rule from member. Are you missing a type registration?"));
                }
            }





                
                d = s;
                return d;
            };

        return mapping;
    }
}