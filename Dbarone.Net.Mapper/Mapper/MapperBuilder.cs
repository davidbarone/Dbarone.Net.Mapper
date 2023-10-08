using System.Diagnostics;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Takes a <see cref="MapperConfiguration" /> instance, and builds a mapper to handle the mapping between 2 types.
/// </summary>
public class MapperBuilder {
    private MapperConfiguration Configuration { get; set; }
    public MapperBuilder(MapperConfiguration configuration) {
        this.Configuration = configuration;
    }

    public ObjectMapper GetMapper<TSource, TDestination>(){
        throw new NotSupportedException();
    }
    public GetMapper(Type fromType, Type toType) {

    }


    private IEnumerable<MapperDelegate> Build(Type sourceType, Type destinationType)
    {
        // Are both source and destination types registered?
        if (Configuration.)

        // validation
        if (!configuration.ContainsKey(sourceType))
        {
            throw new MapperException($"Source type: [{sourceType.Name}] not registered.");
        }

        if (!configuration.ContainsKey(destinationType))
        {
            throw new MapperException($"Destination type: [{destinationType.Name}] not registered.");
        }

        if (destinationType.IsInterface)
        {
            throw new MapperException($"Destination type cannot be interface type.");
        }

        var sourceConfig = this.configuration.First(c => c.Value.Type == sourceType).Value;
        var destinationConfig = this.configuration.First(c => c.Value.Type == destinationType).Value;

        if ((sourceConfig.Options.EndPointValidation & MapperEndPoint.Source) == MapperEndPoint.Source)
        {
            // check all source member rules map to destination rules.
            var unmappedSourceMembers = sourceConfig
                .MemberConfiguration
                .Where(m => destinationConfig
                    .MemberConfiguration
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);
            if (unmappedSourceMembers.Count() > 0)
            {
                var errorMembers = unmappedSourceMembers.Select(m => m.MemberName).Aggregate("", (current, next) => current + " " + $"[{next}]");
                throw new MapperException($"The following source members are not mapped: {errorMembers}.");
            }
        }

        if ((destinationConfig.Options.EndPointValidation & MapperEndPoint.Destination) == MapperEndPoint.Destination)
        {
            // check all source member rules map to destination rules.
            var unmappedDestinationMembers = destinationConfig
                .MemberConfiguration
                .Where(m => sourceConfig
                    .MemberConfiguration
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);
            if (unmappedDestinationMembers.Count() > 0)
            {
                var errorMembers = unmappedDestinationMembers.Select(m => m.MemberName).Aggregate("", (current, next) => current + " " + $"[{next}]");
                throw new MapperException($"The following destination members are not mapped: {errorMembers}.");
            }
        }

        // Get internal member names matching on source + destination
        IEnumerable<string> matchedMembers = destinationConfig
            .MemberConfiguration
            .Where(mc => mc.Ignore == false)
            .Select(mc => mc.InternalMemberName).Intersect(
                sourceConfig
                .MemberConfiguration
                .Where(mc => mc.Ignore == false)
                .Select(mc => mc.InternalMemberName)
            );

        IList<MapperDelegate> mappings = new List<MapperDelegate>();
        foreach (var member in matchedMembers)
        {
            var sourceMemberConfig = sourceConfig.MemberConfiguration.First(mc => mc.InternalMemberName.Equals(member));
            var destinationMemberConfig = destinationConfig.MemberConfiguration.First(mc => mc.InternalMemberName.Equals(member));
            var sourceMemberType = sourceMemberConfig.DataType;
            var destinationMemberType = destinationMemberConfig.DataType;

            if (sourceMemberType == destinationMemberType)
            {
                if (sourceMemberType.IsBuiltInType())
                {
                    // built-in types - simple assign for map
                    MapperDelegate mapping = (s, d) =>
                    {
                        destinationMemberConfig.Setter(d, sourceMemberConfig.Getter(s));
                    };
                    mappings.Add(mapping);
                }
                else if (
                    // enum -> enum
                    destinationMemberConfig.DataType.IsEnum &&
                    destinationMemberConfig.DataType.GetEnumUnderlyingType().IsBuiltInType() &&
                    sourceMemberConfig.DataType.IsEnum &&
                    sourceMemberConfig.DataType.GetEnumUnderlyingType().IsBuiltInType())
                {
                    MapperDelegate mapping = (s, d) =>
                    {
                        destinationMemberConfig.Setter(d, sourceMemberConfig.Getter(s));
                    };
                    mappings.Add(mapping);
                }
                else if (
                    // nullable -> nullable
                    destinationMemberConfig.DataType.IsNullable() &&
                    destinationMemberConfig.DataType.GetNullableUnderlyingType()!.IsBuiltInType() &&
                    sourceMemberConfig.DataType.IsNullable() &&
                    sourceMemberConfig.DataType.GetNullableUnderlyingType()!.IsBuiltInType())
                {
                    MapperDelegate mapping = (s, d) =>
                    {
                        destinationMemberConfig.Setter(d, sourceMemberConfig.Getter(s));
                    };
                    mappings.Add(mapping);
                }
            }
            else if (this.configuration.Keys.Contains(destinationMemberType) && this.configuration.Keys.Contains(sourceMemberType))
            {
                // reference type -> reference type (both types registered in mapper config)
                MapperDelegate mapping = (s, d) =>
                {
                    destinationMemberConfig.Setter(
                        d,
                        MapOne(sourceMemberType, destinationMemberType, sourceMemberConfig.Getter(s)));
                };
                mappings.Add(mapping);
            }
            else if (sourceMemberType == destinationMemberType && (sourceMemberType.IsValueType))
            {
                // from/to are same type, and ValueType. ValueTypes are automatically copied on assignment.
                // No need to map properties.
                MapperDelegate mapping = (s, d) =>
                {
                    destinationMemberConfig.Setter(d, sourceMemberConfig.Getter(s));
                };
                mappings.Add(mapping);
            }
            else
            {
                throw new MapperException($"Cannot map from type: {sourceType} to {destinationType}. Are you missing a type registration or mapping?");
            }
        }
        return mappings;
    }
}