using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Takes a <see cref="MapperConfiguration" /> instance, and builds a mapper to handle the mapping between 2 types.
/// </summary>
public class MapperBuilder
{
    private Config Configuration { get; set; }
    public MapperBuilder(Config configuration)
    {
        this.Configuration = configuration;
    }

    /// <summary>
    /// Returns true if the source and destination mapper combination has been previously
    /// built and saved to the cache.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <returns></returns>
    public bool HasMapper<TSource, TDestination>()
    {

    }

    /// <summary>
    /// Gets a mapper for a source and destination type combination. If the source and destination
    /// mapper has not been retrieved before, a one-time build process occurs to generate all the
    /// mapping rules, and a validation also takes place.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public ObjectMapper GetMapper<TSource, TDestination>()
    {
        throw new NotSupportedException();
    }
    public GetMapper(Type fromType, Type toType)
    {

    }

    /// <summary>
    /// Validates the source and destinations in respect of the end-point validation rules provided. 
    /// </summary>
    /// <returns></returns>
    private List<MapperBuildNotification> EndPointValidation(MapperTypeConfiguration sourceConfiguration, MapperTypeConfiguration destinationConfiguration)
    {

        List<MapperBuildNotification> notifications = new List<MapperBuildNotification>();

        if ((sourceConfiguration.Options.EndPointValidation & MapperEndPoint.Source) == MapperEndPoint.Source)
        {
            // check all source member rules map to destination rules.
            var unmappedSourceMembers = sourceConfiguration
                .MemberConfiguration
                .Where(m => destinationConfiguration
                    .MemberConfiguration
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedSourceMembers)
            {
                notifications.Add(new MapperBuildNotification
                {
                    SourceType = sourceConfiguration.Type,
                    DestinationType = destinationConfiguration.Type,
                    MemberName = item.MemberName,
                    Message = "Source end point validation enabled, but source member is not mapped to destination."
                });
            }
        }

        if ((destinationConfiguration.Options.EndPointValidation & MapperEndPoint.Destination) == MapperEndPoint.Destination)
        {
            // check all source member rules map to destination rules.
            var unmappedDestinationMembers = destinationConfiguration
                .MemberConfiguration
                .Where(m => sourceConfiguration
                    .MemberConfiguration
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedDestinationMembers)
            {
                notifications.Add(new MapperBuildNotification
                {
                    SourceType = sourceConfiguration.Type,
                    DestinationType = destinationConfiguration.Type,
                    MemberName = item.MemberName,
                    Message = "Destination end point validation enabled, but destination member is not mapped from source."
                });
            }
        }

        return notifications;
    }


    private IEnumerable<MapperDelegate> Build(Type sourceType, Type destinationType)
    {
        List<MapperBuildNotification> notifications = new List<MapperBuildNotification>();
        var sourceConfig = this.Configuration.Types.FirstOrDefault(c => c.Value.Type == sourceType).Value;
        var destinationConfig = this.Configuration.Types.FirstOrDefault(c => c.Value.Type == destinationType).Value;

        if (destinationType.IsInterface)
        {
            throw new MapperException($"Destination type cannot be interface type.");
        }

        // Are both source and destination types registered?
        if (sourceConfig != null && destinationConfig !=null)
        {
            // both source and destination types registered - normal mapping process

            notifications.AddRange(EndPointValidation(sourceConfig, destinationConfig));
            // do end point validation here.




        }

        // validation
        if (!configuration.ContainsKey(sourceType))
        {
            throw new MapperException($"Source type: [{sourceType.Name}] not registered.");
        }

        if (!configuration.ContainsKey(destinationType))
        {
            throw new MapperException($"Destination type: [{destinationType.Name}] not registered.");
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