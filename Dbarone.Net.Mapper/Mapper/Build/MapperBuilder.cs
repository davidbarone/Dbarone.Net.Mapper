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
            new AssignableMapperProvider(),
            new IConvertibleMapperProvider(),
            new ConverterMapperProvider(),
            new ImplicitOperatorMapperProvider(),
            new EnumerableMapperProvider(),
            new MemberwiseMapperProvider()
        };
    }

    #region Public Methods

    /// <summary>
    /// Gets a mapper delegate which is able to map the SourceDestination pairing.
    /// </summary>
    /// <param name="sourceDestination"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public MapperDelegate GetMapper(SourceDestination sourceDestination, string path = "", List<MapperBuildError>? errors = null)
    {
        var isRoot = (path == "");
        MapperDelegate mapper = null;

        if (isRoot)
        {
            errors = new List<MapperBuildError>();
        }

        // generate mapper if not root or mapper doesn't exist
        if (!(isRoot && MapperExists(sourceDestination)))
        {
            // source
            if (!this.Metadata.Types.ContainsKey(sourceDestination.Source))
            {
                this.BuildType(sourceDestination.Source);
            }
            var sourceBuild = this.Metadata.Types[sourceDestination.Source];

            // destination
            if (!this.Metadata.Types.ContainsKey(sourceDestination.Destination))
            {
                this.BuildType(sourceDestination.Destination);
            }
            var destinationBuild = this.Metadata.Types[sourceDestination.Destination];

            // find mapper to handle source-destination
            foreach (var provider in this.MapperProviders)
            {
                if (provider.CanCreateMapFor(sourceBuild, destinationBuild, this))
                {
                    mapper = provider.GetMapFor(sourceBuild, destinationBuild, this, path, errors);
                    break;
                }
            }
        }

        // cache results
        this.Metadata.Errors[sourceDestination] = errors;
        if (mapper != null)
        {
            this.Metadata.Mappers[sourceDestination] = mapper;
        }

        // throw exception?
        if (this.Metadata.Errors.ContainsKey(sourceDestination) && this.Metadata.Errors[sourceDestination].Any())
        {
            throw new MapperBuildException(this.Metadata.Errors[sourceDestination]);
        }

        // return mapping
        return this.Metadata.Mappers[sourceDestination];
    }

    /// <summary>
    /// Pre-emptively builds all type information provided by the configuration.
    /// </summary>
    public void Build()
    {
        foreach (var item in this.Configuration.Config.Types.Keys)
        {
            BuildType(item);
        }
    }

    /// <summary>
    /// Gets the build metadata for a single type.
    /// </summary>
    /// <param name="type">The type to get build information for.</param>
    /// <returns>Returns the build information.</returns>
    /// <exception cref="Exception">Throws an exception if the build type is not found.</exception>
    public BuildType GetBuildTypeFor(Type type)
    {
        if (!Metadata.Types.ContainsKey(type))
        {
            throw new Exception("Type not found!");
        }
        return Metadata.Types[type];
    }

    #endregion

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

    private void BuildType(Type type)
    {
        List<MapperBuildError> errors = new List<MapperBuildError>();

        if (!this.Configuration.Config.Types.ContainsKey(type) && this.Configuration.Config.AutoRegisterTypes) {
            this.Configuration.RegisterType(type);
        } 

        var configType = this.Configuration.Config.Types[type];

        if (configType == null)
        {
            throw new MapperBuildException(type, MapperEndPoint.None, "", null, "Type not registered in configuration.");
        }

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
            throw new MapperBuildException(type, MapperEndPoint.None, "", null, "No resolver found for type.");
        }


        BuildType buildType = new BuildType
        {
            Type = configType.Type,
            Options = configType.Options,
            MemberResolver = resolver,
        };

        // Add to metadata
        this.Metadata.Types[type] = buildType;

        if (!buildType.isOpenGeneric)
        {
            // Add members
            AddMembers(type);

            // Add calculations
            AddCalculations(type);

            // Validate Type
            ValidateType(buildType);
        }
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

        if (!this.Configuration.Config.Resolvers.Select(r => r.GetType()).Contains(typeof(InterfaceMemberResolver)))
        {
            this.Configuration.Config.Resolvers.Add(new InterfaceMemberResolver());
        }
    }

    private void ValidateType(BuildType buildType)
    {
        List<MapperBuildError> errors = new List<MapperBuildError>();

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
                errors.Add(new MapperBuildError(buildType.Type, MapperEndPoint.None, "", member.MemberName, "Member internal name not unique."));
            }
        }

        if (errors.Any())
        {
            throw new MapperBuildException(errors);
        }
    }

    private void AddMembers(Type type)
    {
        var buildType = this.GetBuildTypeFor(type);
        var resolver = buildType.MemberResolver;
        var configType = this.Configuration.Config.Types[type];

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
                    throw new MapperBuildException(type, MapperEndPoint.None, "", member, "Data type not known for member.");
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
        buildType.Members = buildMembers;
    }

    private bool GetMemberInclusionStatus(Type type, string member)
    {
        MemberFilterDelegate? memberFilterRuleA = null;
        MemberFilterDelegate? memberFilterRuleB = null;
        MemberFilterDelegate? memberFilterRule = null;

        if (!Configuration.Config.Types.ContainsKey(type))
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

    private void AddCalculations(Type type)
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