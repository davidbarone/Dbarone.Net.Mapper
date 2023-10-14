using System.Diagnostics;
using System.Runtime.CompilerServices;
using Dbarone.Net.Extensions;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Takes a <see cref="MapperConfiguration" /> instance, and builds a mapper to handle the mapping between 2 types.
/// </summary>
public class MapperBuilder
{
    /// <summary>
    /// The input configuration.
    /// </summary>
    private Config Configuration { get; init; }

    /// <summary>
    /// Stores the build-time metadata.
    /// </summary>
    private BuildMetadataCache Metadata { get; set; }

    public MapperBuilder(Config configuration)
    {
        this.Configuration = configuration;
        AddCoreResolvers();
    }

    private void AddCoreResolvers()
    {
        // Add core resolvers
        if (!this.Configuration.Resolvers.Select(r => r.GetType()).Contains(typeof(StructMemberResolver)))
        {
            this.Configuration.Resolvers.Add(new StructMemberResolver());
        }

        if (!this.Configuration.Resolvers.Select(r => r.GetType()).Contains(typeof(DictionaryMemberResolver)))
        {
            this.Configuration.Resolvers.Add(new DictionaryMemberResolver());
        }

        if (!this.Configuration.Resolvers.Select(r => r.GetType()).Contains(typeof(ClassMemberResolver)))
        {
            this.Configuration.Resolvers.Add(new ClassMemberResolver());
        }
    }

    /// <summary>
    /// Creates an object mapper for a source / destination
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <returns>Returns an object mapper which can map objects from source to destination type.</returns>
    /// <exception cref="MapperBuildException">Throws an exception if any build-time errors occur.</exception>
    public ObjectMapper<TSource, TDestination> BuildMapper<TSource, TDestination>()
    {
        List<MapperBuildError> errors = new List<MapperBuildError>();
        var path = "";  // root path
        Build(typeof(TSource), typeof(TDestination), path, errors);

        if (errors.Any())
        {
            throw new MapperBuildException("Errors occurred during build. See Errors collection for details.", errors);
        }
        else
        {
            return new ObjectMapper<TSource, TDestination>()
        }
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

    private void ValidateType(BuildType buildType, string path, List<MapperBuildError> errors)
    {
        // Check no duplicate internal names
        var duplicates = buildType.Members
            .GroupBy(g => g.InternalMemberName)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key).ToList();

        foreach (var duplicate in duplicates)
        {
            var members = buildType.Members.Where(m => m.InternalMemberName == duplicate);
            foreach (var member in members)
            {
                errors.Add(new MapperBuildError(buildType.Type, MapperEndPoint.None, path, member.MemberName, "Member internal name not unique."));
            }
        }
    }


    /// <summary>
    /// Validates the source and destinations in respect of the end-point validation rules provided. 
    /// </summary>
    /// <returns></returns>
    private void EndPointValidation(BuildType sourceBuild, BuildType destinationBuild, string path, List<MapperBuildError> errors)
    {
        if ((sourceBuild.Options.EndPointValidation & MapperEndPoint.Source) == MapperEndPoint.Source)
        {
            // check all source member rules map to destination rules.
            var unmappedSourceMembers = sourceBuild
                .Members
                .Where(m => destinationBuild
                    .Members
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedSourceMembers)
            {
                errors.Add(new MapperBuildError(sourceBuild.Type, MapperEndPoint.Source, path, item.MemberName, "Source end point validation enabled, but source member is not mapped to destination."));
            }
        }

        if ((destinationBuild.Options.EndPointValidation & MapperEndPoint.Destination) == MapperEndPoint.Destination)
        {
            // check all source member rules map to destination rules.
            var unmappedDestinationMembers = destinationBuild
                .Members
                .Where(m => sourceBuild
                    .Members
                    .Select(d => d.InternalMemberName).Contains(m.InternalMemberName) == false);

            foreach (var item in unmappedDestinationMembers)
            {
                errors.Add(new MapperBuildError(destinationBuild.Type, MapperEndPoint.Destination, path, item.MemberName, "Destination end point validation enabled, but destination member is not mapped from source."));
            }
        }
    }

    private void Build(Type sourceType, Type destinationType, string path, List<MapperBuildError> errors)
    {
        // validations
        if (destinationType.IsInterface)
        {
            errors.Add(new MapperBuildError(destinationType, MapperEndPoint.Destination, path, null, $"Destination type cannot be interface."));
            return;
        }

        var sourceConfig = this.Configuration.Types.FirstOrDefault(c => c.Value.Type == sourceType).Value;
        var destinationConfig = this.Configuration.Types.FirstOrDefault(c => c.Value.Type == destinationType).Value;

        // Do we have the source + destination types registered?
        if (sourceConfig == null)
        {
            errors.Add(new MapperBuildError(sourceType, MapperEndPoint.Source, path, null, $"Source type not registered."));
        }
        if (destinationConfig == null)
        {
            errors.Add(new MapperBuildError(destinationType, MapperEndPoint.Destination, path, null, $"Destination type not registered."));
        }

        if (sourceConfig == null || destinationConfig == null)
        {
            // cannot proceed. Exit early.
            return;
        }

        // Check whether types have been built?
        if (!Metadata.Types.ContainsKey(sourceType))
        {
            BuildType(sourceType, path, errors);
            AddCalculations(sourceType, path, errors);
        }

        if (!Metadata.Types.ContainsKey(destinationType))
        {
            BuildType(destinationType, path, errors);
            AddCalculations(destinationType, path, errors);
        }

        // At this point, the types are built
        var sourceBuild = Metadata.Types[sourceType];
        var destinationBuild = Metadata.Types[destinationType];

        // Validate each type separately
        ValidateType(sourceBuild, path, errors);
        ValidateType(destinationBuild, path, errors);

        // Do end point validation
        EndPointValidation(sourceBuild, destinationBuild, path, errors);

        // Build Mappings
        BuildMapRules(sourceBuild, destinationBuild, path, errors);
    }

    private void BuildType(Type type, string path, List<MapperBuildError> errors)
    {
        var configType = this.Configuration.Types[type];

        IMemberResolver? resolver = null;

        // Get resolver
        foreach (var configResolver in this.Configuration.Resolvers)
        {
            if (configResolver.CanResolveMembersForType(configType.Type))
            {
                resolver = configResolver;
                break;
            }
        }

        if (resolver == null)
        {
            errors.Add(new MapperBuildError(type, MapperEndPoint.None, path, null, "No resolver found for type."));
            return;
        }

        // Get members
        string[] members = new string[] { };
        List<BuildMember> buildMembers = new List<BuildMember>();
        if (!resolver.DeferMemberResolution)
        {
            members = resolver.GetTypeMembers(configType.Type, configType.Options);

            foreach (var member in members)
            {
                var dataType = resolver.GetMemberType(configType.Type, member, configType.Options);
                var getter = resolver.GetGetter(configType.Type, member, configType.Options);
                var setter = resolver.GetSetter(configType.Type, member, configType.Options);
                var ignore = GetMemberInclusionStatus(configType.Type, member);
                var internalName = GetInternalName(configType.Type, member, configType.Options.MemberRenameStrategy);

                // validations
                if (dataType == null)
                {
                    errors.Add(new MapperBuildError(type, MapperEndPoint.None, path, member, "Data type not known for member."));
                }
                else if (getter == null)
                {
                    errors.Add(new MapperBuildError(type, MapperEndPoint.None, path, member, "No getter for member."));
                }
                else if (setter == null)
                {
                    errors.Add(new MapperBuildError(type, MapperEndPoint.None, path, member, "No setter for member."));
                }
                else
                {
                    // add member to build
                    buildMembers.Add(new BuildMember
                    {
                        MemberName = member,
                        DataType = dataType,
                        Getter = getter,
                        Setter = setter,
                        Ignore = ignore,
                        InternalMemberName = internalName
                    });
                }
            }
        }

        BuildType buildType = new BuildType
        {
            Type = configType.Type,
            Options = configType.Options,
            MemberResolver = resolver,
            Members = buildMembers
        };

        // Add to metadata
        this.Metadata.Types[type] = buildType;
    }

    private bool GetMemberInclusionStatus(Type type, string member)
    {
        MemberFilterDelegate? memberFilterRuleA = null;
        MemberFilterDelegate? memberFilterRuleB = null;
        MemberFilterDelegate? memberFilterRule = null;

        if (Configuration.Types.ContainsKey(type))
        {
            throw new Exception($"GetIgnoreStatus. Invalid type: {type}.");
        }

        // Get member filter function if exists
        if (Configuration.MemberFilters.ContainsKey(type))
        {
            memberFilterRuleA = Configuration.MemberFilters[type];
        }

        memberFilterRuleB = Configuration.Types[type].Options.MemberFilterRule;
        memberFilterRule = memberFilterRuleA != null ? memberFilterRuleA : memberFilterRuleB;

        var isIncluded = (memberFilterRule != null) ? memberFilterRule(member) : false;
        foreach (var configMemberInclusion in Configuration.MemberInclusions.Where(c => c.Type == type && c.Member.Equals(member, StringComparison.Ordinal)))
        {
            isIncluded = configMemberInclusion.IncludeExclude == IncludeExclude.Include ? true : false;
        }
        return isIncluded;
    }

    private string GetInternalName(Type type, string memberName, IMemberRenameStrategy? memberRenameStrategy = null)
    {
        var internalMemberName = memberName;

        // rename strategy present?
        if (memberRenameStrategy != null)
        {
            internalMemberName = memberRenameStrategy!.RenameMember(memberName);
        }

        // loop through any specific rename rules. Last one wins. 
        foreach (var rename in Configuration.MemberRenames.Where(c => c.Type == type && c.MemberName.Equals(memberName, StringComparison.Ordinal)))
        {
            internalMemberName = rename.InternalMemberName;
        }
        return internalMemberName;
    }

    private void BuildMapRules(BuildType sourceBuild, BuildType destinationBuild, string path, List<MapperBuildError> errors)
    {

        // Get internal member names matching on source + destination
        IEnumerable<string> matchedMembers = destinationBuild
            .Members
            .Where(mc => mc.Ignore == false)
            .Select(mc => mc.InternalMemberName).Intersect(
                sourceBuild
                .Members
                .Where(mc => mc.Ignore == false)
                .Select(mc => mc.InternalMemberName)
            );

        IList<MapperDelegate> mappings = new List<MapperDelegate>();
        foreach (var member in matchedMembers)
        {
            var sourceMemberBuild = sourceBuild.Members.First(mc => mc.InternalMemberName.Equals(member));
            var destinationMemberBuild = destinationBuild.Members.First(mc => mc.InternalMemberName.Equals(member));
            var sourceMemberType = sourceMemberBuild.DataType;
            var destinationMemberType = destinationMemberBuild.DataType;

            if (sourceMemberType == destinationMemberType)
            {
                if (sourceMemberType.IsBuiltInType())
                {
                    // built-in types - simple assign for map
                    MapperDelegate mapping = (s, d) =>
                    {
                        destinationMemberBuild.Setter(d, sourceMemberBuild.Getter(s));
                    };
                    mappings.Add(mapping);
                }
                else if (
                    // enum -> enum
                    destinationMemberBuild.DataType.IsEnum &&
                    destinationMemberBuild.DataType.GetEnumUnderlyingType().IsBuiltInType() &&
                    sourceMemberBuild.DataType.IsEnum &&
                    sourceMemberBuild.DataType.GetEnumUnderlyingType().IsBuiltInType())
                {
                    MapperDelegate mapping = (s, d) =>
                    {
                        destinationMemberBuild.Setter(d, sourceMemberBuild.Getter(s));
                    };
                    mappings.Add(mapping);
                }
                else if (
                    // nullable -> nullable
                    destinationMemberBuild.DataType.IsNullable() &&
                    destinationMemberBuild.DataType.GetNullableUnderlyingType()!.IsBuiltInType() &&
                    sourceMemberBuild.DataType.IsNullable() &&
                    sourceMemberBuild.DataType.GetNullableUnderlyingType()!.IsBuiltInType())
                {
                    MapperDelegate mapping = (s, d) =>
                    {
                        destinationMemberBuild.Setter(d, sourceMemberBuild.Getter(s));
                    };
                    mappings.Add(mapping);
                }
                else {
                    // as long as source + destination types match, we can do straight 1:1 map
                    MapperDelegate mapping = (s, d) =>
                    {
                        destinationMemberBuild.Setter(d, sourceMemberBuild.Getter(s));
                    };
                    mappings.Add(mapping);
                }
            }

            else if (this.Configuration.Converters.ContainsKey(sourceMemberType) && this.Configuration.Converters.ContainsKey(destinationMemberType)){
                // Use converter to map
                
            }
            else if (this.Configuration.Types.Keys.Contains(destinationMemberType) && this.Configuration.Types.Keys.Contains(sourceMemberType))
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
    }

    private void AddCalculations(Type type, string path, List<MapperBuildError> errors) {

        // Add calculations to existing type
        foreach (var calculation in this.Configuration.Calculations.Where(c=>c.SourceType==type))
        {
            var buildType = Metadata.Types[type];

            buildType.Members.Add(new BuildMember {
                MemberName = calculation.MemberName,
                DataType = calculation.MemberType,
                InternalMemberName = calculation.MemberName,
                Ignore = false,
                Getter = calculation.Calculation.Convert,
                Setter = null,
                Calculation = calculation.Calculation
            });
        }
    }
}