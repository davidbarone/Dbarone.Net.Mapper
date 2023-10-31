using System.Diagnostics;
using System.Reflection;
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
    internal MapperConfiguration Configuration { get; init; }

    /// <summary>
    /// Stores the build-time metadata.
    /// </summary>
    internal BuildMetadataCache Metadata { get; set; }

    /// <summary>
    /// Stores build errors.
    /// </summary>
    private Dictionary<SourceDestination, List<MapperBuildError>> Errors { get; set; } = new Dictionary<SourceDestination, List<MapperBuildError>>();

    private IMapperProvider[] MapperProviders;

    /// <summary>
    /// Creates a new <see cref="MapperBuilder"/> instance.
    /// </summary>
    /// <param name="configuration">The configuration used to create mapper objects.</param>
    public MapperBuilder(MapperConfiguration configuration)
    {
        this.Configuration = configuration;
        AddCoreResolvers();
        Metadata = new BuildMetadataCache();
        this.MapperProviders = new IMapperProvider[] {
            new MatchingTypeMapperProvider(),
            new ImplicitOperatorMapperProvider()
        };
    }

    /// <summary>
    /// Gets the build metadata for a single type.
    /// </summary>
    /// <param name="type">The type to get build information for.</param>
    /// <returns>Returns the build information.</returns>
    /// <exception cref="Exception">Throws an exception if the build type is not found.</exception>
    internal BuildType GetBuildTypeFor(Type type)
    {
        if (!Metadata.Types.ContainsKey(type))
        {
            throw new Exception("Type not found!");
        }
        return Metadata.Types[type];
    }

    /// <summary>
    /// Returns true if the source and destination type pairing exists.
    /// </summary>
    /// <param name="sourceDestination"></param>
    /// <returns></returns>
    public bool MapperExists(SourceDestination sourceDestination)
    {
        return Metadata.Mappers.ContainsKey(sourceDestination);
    }

    /// <summary>
    /// Gets the mapping rules for a SourceDestination pairing.
    /// </summary>
    /// <param name="sourceDestination">The source and destination types.</param>
    /// <returns></returns>
    internal MapperDelegate GetMapperFor(SourceDestination sourceDestination)
    {
        Build(sourceDestination);

        // Return mapping rules for the source/destination pair
        return this.Metadata.Mappers[sourceDestination];
    }

    /// <summary>
    /// Returns the creator delegate for a type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>Returns a delegate that when invoked will create an instance of the object.</returns>
    internal CreateInstance GetCreatorFor(Type type)
    {
        if (!Metadata.Types.ContainsKey(type))
        {

        }
        return Metadata.Types[type].MemberResolver.CreateInstance(type, new object[] { });
    }

    #region Private Core Build Methods

    public void Build(SourceDestination sourceDestination)
    {
        if (!MapperExists(sourceDestination))
        {
            var errors = new List<MapperBuildError>();
            string path = "";
            errors = Build(sourceDestination, sourceDestination, path, errors);
            this.Errors[sourceDestination] = errors;
        }
        if (this.Errors.ContainsKey(sourceDestination) && this.Errors[sourceDestination].Any())
        {
            throw new MapperBuildException("Error occurred during build phase. See Errors collection for more information", this.Errors[sourceDestination]);
        }
    }

    private List<MapperBuildError> Build(SourceDestination sourceDestination, SourceDestination memberSourceDestination, string path, List<MapperBuildError> errors)
    {
        // validations
        if (memberSourceDestination.Destination.IsInterface)
        {
            errors.Add(new MapperBuildError(memberSourceDestination.Destination, MapperEndPoint.Destination, path, null, $"Destination type cannot be interface."));
            return errors;
        }

        var sourceConfig = this.Configuration.Config.Types.FirstOrDefault(c => c.Value.Type == memberSourceDestination.Source).Value;
        var destinationConfig = this.Configuration.Config.Types.FirstOrDefault(c => c.Value.Type == memberSourceDestination.Destination).Value;

        // Do we have the source + destination types registered?
        if (sourceConfig == null)
        {
            errors.Add(new MapperBuildError(memberSourceDestination.Source, MapperEndPoint.Source, path, null, $"Source type not registered."));
        }
        if (destinationConfig == null)
        {
            errors.Add(new MapperBuildError(memberSourceDestination.Destination, MapperEndPoint.Destination, path, null, $"Destination type not registered."));
        }

        if (sourceConfig == null || destinationConfig == null)
        {
            // cannot proceed. Exit early.
            return errors;
        }

        // Check whether types have been built?
        if (!Metadata.Types.ContainsKey(memberSourceDestination.Source))
        {
            BuildType(memberSourceDestination.Source, path, errors);
            AddCalculations(memberSourceDestination.Source, path, errors);
        }

        if (!Metadata.Types.ContainsKey(memberSourceDestination.Destination))
        {
            BuildType(memberSourceDestination.Destination, path, errors);
            AddCalculations(memberSourceDestination.Destination, path, errors);
        }

        // At this point, the types are built
        var sourceBuild = Metadata.Types[memberSourceDestination.Source];
        var destinationBuild = Metadata.Types[memberSourceDestination.Destination];

        // Validate each type separately
        ValidateType(sourceBuild, path, errors);
        ValidateType(destinationBuild, path, errors);

        // Do end point validation
        EndPointValidation(sourceBuild, destinationBuild, path, errors);

        // Build Mappings
        this.Metadata.Mappers[sourceDestination] = GetMapper(sourceDestination);
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

    /// <summary>
    /// Adds members to a dynamic type at runtime.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="path"></param>
    /// <param name="obj"></param>
    internal void AddDynamicMembers(Type type, string path, object? obj, List<MapperBuildError> errors)
    {
        var buildType = this.GetBuildTypeFor(type);

        var members = buildType.MemberResolver.GetInstanceMembers(obj);

        List<BuildMember> buildMembers = new List<BuildMember>();
        foreach (var member in members)
        {
            var dataType = buildType.MemberResolver.GetMemberType(type, member, buildType.Options);
            var getter = buildType.MemberResolver.GetGetter(type, member, buildType.Options);
            var setter = buildType.MemberResolver.GetSetter(type, member, buildType.Options);
            var ignore = GetMemberInclusionStatus(type, member);
            var internalName = GetInternalName(type, member, buildType.Options.MemberRenameStrategy);

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
        buildType.Members = buildMembers;
    }

    private MethodInfo? GetImplicitCast(Type fromType, Type toType)
    {
        var method = fromType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .FirstOrDefault(
                            m => m.ReturnType == toType &&
                            m.Name == "op_Implicit"
                        );
        return method;
    }

    public MapperDelegate GetMapper(SourceDestination sourceDestination) {
        foreach (var provider in this.MapperProviders) {
            var sourceBuild = this.Metadata.Types[sourceDestination.Source];
            var destinationBuild = this.Metadata.Types[sourceDestination.Destination];
            if (provider.CanCreateMapFor(sourceBuild, destinationBuild, this)) {
                return provider.GetMapFor(sourceBuild, destinationBuild, this);
            }
        }
        throw new Exception("whoops");
    }

    internal void BuildMapRules(SourceDestination sourceDestination, BuildType sourceBuild, BuildType destinationBuild, string path, List<MapperBuildError> errors)
    {
        IList<MapperDelegate> mappings = new List<MapperDelegate>();

        // Check if types map directly via assignment
        var implicitOperator = GetImplicitCast(sourceDestination.Source, sourceDestination.Destination);

        if (sourceDestination.Source == sourceDestination.Destination)
        {
            MapperDelegate mapping = (s, d) =>
            {
                d = s;
                return d;
            };
            mappings.Add(mapping);
        }
        else if (implicitOperator != null)
        {
            MapperDelegate mapping = (s, d) =>
            {
                d = implicitOperator.Invoke(s, new object[] { })!;
                return d;
            };
            mappings.Add(mapping);
        }
        else
        {











        }
    }

    #endregion

    #region Helper Methods
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

    #endregion

}