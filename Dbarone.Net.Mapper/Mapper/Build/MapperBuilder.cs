using System.Diagnostics;
using System.Runtime.CompilerServices;
using Dbarone.Net.Extensions;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Builds the mapping graph between 2 types.
/// </summary>
public class MapperBuilder
{
    /// <summary>
    /// The input configuration for the map.
    /// </summary>
    private MapperConfiguration Configuration { get; init; }

    /// <summary>
    /// Stores the build-time metadata.
    /// </summary>
    private BuildMetadataCache Metadata { get; set; }

    /// <summary>
    /// Creates a new <see cref="MapperBuilder"/> instance.
    /// </summary>
    /// <param name="configuration">The configuration used to create mapper objects.</param>
    public MapperBuilder(MapperConfiguration configuration)
    {
        this.Configuration = configuration;
        AddCoreResolvers();
        Metadata = new BuildMetadataCache();
    }

    public BuildType GetBuildTypeFor(Type type) {
        if (!Metadata.Types.ContainsKey(type)) {
            throw new Exception("Type not found!");
        }
        return Metadata.Types[type];
    }

    public IDictionary<SourceDestinationPath, SourceDestinationPathRules> GetMapRulesFor(Type sourceType, Type destinationType)
    {
        SourceDestination sourceDestination = new SourceDestination(sourceType, destinationType);

        if (!Metadata.MapRules.ContainsKey(sourceDestination))
        {
            Build(sourceType, destinationType);
        }


        // Return mapping rules for the source/destination pair
        return this.Metadata.MapRules[sourceDestination];
    }

    public CreateInstance GetCreatorFor(Type type)
    {
        if (!Metadata.Types.ContainsKey(type))
        {

        }
        return Metadata.Types[type].MemberResolver.CreateInstance(type, new object[] { });
    }

    private void AddCoreResolvers()
    {
        // Add core resolvers
        if (!this.Configuration.Config.Resolvers.Select(r => r.GetType()).Contains(typeof(StructMemberResolver)))
        {
            this.Configuration.Config.Resolvers.Add(new StructMemberResolver());
        }

        if (!this.Configuration.Config.Resolvers.Select(r => r.GetType()).Contains(typeof(DictionaryMemberResolver)))
        {
            this.Configuration.Config.Resolvers.Add(new DictionaryMemberResolver());
        }

        if (!this.Configuration.Config.Resolvers.Select(r => r.GetType()).Contains(typeof(ClassMemberResolver)))
        {
            this.Configuration.Config.Resolvers.Add(new ClassMemberResolver());
        }
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

    private List<MapperBuildError> Build(Type sourceType, Type destinationType, string path = "", List<MapperBuildError>? errors = null)
    {
        if (errors == null)

        {
            errors = new List<MapperBuildError>();
        }

        // Mapper rules already exist?
        if (this.Metadata.MapRules.ContainsKey(new SourceDestination(sourceType, destinationType)))
        {
            return errors;
        }

        // validations
        if (destinationType.IsInterface)
        {
            errors.Add(new MapperBuildError(destinationType, MapperEndPoint.Destination, path, null, $"Destination type cannot be interface."));
            return errors;
        }

        var sourceConfig = this.Configuration.Config.Types.FirstOrDefault(c => c.Value.Type == sourceType).Value;
        var destinationConfig = this.Configuration.Config.Types.FirstOrDefault(c => c.Value.Type == destinationType).Value;

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
            return errors;
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

        return errors;
    }

    private void BuildType(Type type, string path, List<MapperBuildError> errors)
    {
        var configType = this.Configuration.Config.Types[type];

        IMemberResolver? resolver = null;

        // Get resolver
        foreach (var configResolver in this.Configuration.Config.Resolvers)
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

        if (Configuration.Config.Types.ContainsKey(type))
        {
            throw new Exception($"GetIgnoreStatus. Invalid type: {type}.");
        }

        // Get member filter function if exists
        if (Configuration.Config.MemberFilters.ContainsKey(type))
        {
            memberFilterRuleA = Configuration.Config.MemberFilters[type];
        }

        memberFilterRuleB = Configuration.Config.Types[type].Options.MemberFilterRule;
        memberFilterRule = memberFilterRuleA != null ? memberFilterRuleA : memberFilterRuleB;

        var isIncluded = (memberFilterRule != null) ? memberFilterRule(member) : false;
        foreach (var configMemberInclusion in Configuration.Config.MemberInclusions.Where(c => c.Type == type && c.Member.Equals(member, StringComparison.Ordinal)))
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
        foreach (var rename in Configuration.Config.MemberRenames.Where(c => c.Type == type && c.MemberName.Equals(memberName, StringComparison.Ordinal)))
        {
            internalMemberName = rename.InternalMemberName;
        }
        return internalMemberName;
    }

    private void BuildMapRules(BuildType sourceBuild, BuildType destinationBuild, string path, List<MapperBuildError> errors)
    {
        SourceDestination sourceDestination = new SourceDestination(sourceBuild.Type, destinationBuild.Type);

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
                // member types the same - do simple assignment of value to destination object.
                MapperDelegate mapping = (s, d) =>
                {
                    destinationMemberBuild.Setter(d, sourceMemberBuild.Getter(s));
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
                };
                mappings.Add(mapping);
            }
            else if (this.Configuration.Config.Types.Keys.Contains(destinationMemberType) && this.Configuration.Config.Types.Keys.Contains(sourceMemberType))
            {
                // Member types differ, but mapping configuration exists for types
                // create mapping rules recursively.
                Build(sourceMemberType, destinationMemberType, "", errors);
            }
            else
            {
                errors.Add(new MapperBuildError(sourceBuild.Type, MapperEndPoint.Source, path, member, "Cannot create mapping rule from member. Are you missing a type registration?"));
            }
        }
    }

    private void AddCalculations(Type type, string path, List<MapperBuildError> errors)
    {
        // Add calculations to existing type
        foreach (var calculation in this.Configuration.Config.Calculations.Where(c => c.SourceType == type))
        {
            var buildType = Metadata.Types[type];

            buildType.Members.Add(new BuildMember
            {
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