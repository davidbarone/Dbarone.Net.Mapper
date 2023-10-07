namespace Dbarone.Net.Mapper;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;
using System.Reflection;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

/// <summary>
/// Creates the configuration for a <see cref="ObjectMapper" /> mapper object.
/// Before being able to map any objects and types, you must create
/// a mapper configuration, and from this generate an <see cref="ObjectMapper" /> object.
/// </summary>
public class MapperConfiguration
{
    #region Config

    private IList<IMemberResolver> ConfigResolvers { get; set; } = new List<IMemberResolver>();
    private IDictionary<Type, ConfigType> ConfigTypes = new Dictionary<Type, ConfigType>();
    private IList<ConfigCalculation> ConfigCalculations = new List<ConfigCalculation>();
    private IList<ConfigMemberInclusion> ConfigMemberInclusions = new List<ConfigMemberInclusion>();
    private IDictionary<Tuple<Type, Type>, ITypeConverter> ConfigConverters { get; set; } = new Dictionary<Tuple<Type, Type>, ITypeConverter>();
    private IDictionary<Type, MemberFilterDelegate> ConfigMemberFilters = new Dictionary<Type, MemberFilterDelegate>();
    private IList<ConfigRename> ConfigMemberRenames = new List<ConfigRename>();

    #endregion

    internal MapperConfiguration() { }

    #region ctor

    /// <summary>
    /// Creates a new <see cref="MapperConfiguration" /> instance.
    /// </summary>
    /// <returns>returns a new <see cref="MapperConfiguration" /> instance.</returns>
    public static MapperConfiguration Create()
    {
        return new MapperConfiguration();
    }

    #endregion

    #region Register Resolvers

    /// <summary>
    /// Registers an IMemberResolver using a generic type.
    /// </summary>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterResolvers<TResolver>() where TResolver : IMemberResolver
    {
        var resolverType = typeof(TResolver);
        return RegisterResolver(resolverType);
    }

    /// <summary>
    /// Registers an IMemberResolver by type.
    /// </summary>
    /// <param name="resolverType">The type of a resolver.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    /// <exception cref="Exception">Throws an exception if the type does not implement IMemberResolver.</exception>
    public MapperConfiguration RegisterResolver(Type resolverType)
    {
        if (!typeof(IMemberResolver).IsAssignableFrom(resolverType))
        {
            throw new ArgumentException("Type must implement IMemberResolver,", "resolverType");
        }
        IMemberResolver resolver = (IMemberResolver)Activator.CreateInstance(resolverType)!;
        return RegisterResolver(resolver);
    }

    /// <summary>
    /// Registers an IMemberResolver
    /// </summary>
    /// <param name="resolver">An IMemberResolver instance.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    /// <exception cref="Exception">Throws an exception if the type of resolver has already been registered.</exception>
    public MapperConfiguration RegisterResolver(IMemberResolver resolver)
    {
        if (this.ConfigResolvers.Select(r => r.GetType()).Contains(resolver.GetType()))
        {
            throw new Exception($"Resolver {resolver.GetType()} already registered.");
        }
        this.ConfigResolvers.Add(resolver);
        return this;
    }

    #endregion

    #region Register Types

    /// <summary>
    /// Registers a collection of types.
    /// </summary>
    /// <param name="types">An array of Types.</param>
    /// <param name="options">An optional <see cref="MapperOptions" /> object containing configuration details for all the types to be registered.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterTypes(Type[] types, MapperOptions? options = null)
    {
        foreach (var type in types)
        {
            RegisterType(type, options);
        }
        return this;
    }

    /// <summary>
    /// Registers a type using generic types.
    /// </summary>
    /// <typeparam name="T">The type of the entity to register.</typeparam>
    /// <param name="options">An optional <see cref="MapperOptions" /> object containing configuration details for the type being registered.</param>
    /// <example>
    /// The following example shows how to register a type:
    /// <code>
    /// // Configure mapper
    /// var mapper = MapperConfiguration
    ///     .Create()
    ///     .RegisterType&lt;CustomerEntity&gt;()
    ///     .RegisterType&lt;CustomerModel&gt;()
    ///     .Build();
    ///     
    /// // Map object
    /// var obj2 = mapper.MapOne&lt;CustomerEntity, CustomerModel&gt;(obj1); 
    /// </code>
    /// </example>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterType<T>(MapperOptions? options = null)
    {
        return RegisterType(typeof(T), options);
    }

    /// <summary>
    /// Registers a single type.
    /// </summary>
    /// <param name="type">The type to be registered.</param>
    /// <param name="options">An optional <see cref="MapperOptions" /> object containing configuration details for the type being registered.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterType(Type type, MapperOptions? options = null)
    {
        if (options == null)
        {
            options = new MapperOptions();
        }

        this.ConfigTypes[type] = new ConfigType()
        {
            Type = type,
            Options = options
        };

        return this;
    }

    #endregion

    #region Calculations

    /// <summary>
    /// Registers a calculation for a type. Calculations are used to transform a value into another value. A calculation can be given a name allowing it to be used in mappings.
    /// </summary>
    /// <typeparam name="TSource">The source entity type.</typeparam>
    /// <typeparam name="TReturn">The type of the return value for the calculated member.</typeparam>
    /// <param name="memberName">The calculated member name.</param>
    /// <param name="calculation">The calculation.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterCalculation<TSource, TReturn>(string memberName, Func<TSource?, TReturn?> calculation)
    {
        ConfigCalculation calc = new ConfigCalculation()
        {
            SourceType = typeof(TSource),
            MemberName = memberName,
            Calculation = new TypeConverter<TSource, TReturn>(calculation),
            MemberType = typeof(TReturn)
        };

        this.ConfigCalculations.Add(calc);

        return this;
    }

    #endregion

    #region Register Map

    /// <summary>
    /// Registers a specific type-to-type configuration. When registering via <see cref="RegisterType" /> only 1 endpoint
    /// is specified, and the <see cref="ObjectMapper" /> automatically joins members based on member name. In cases
    /// where a specific mapping between 2 types is required, this method can be used to provide custom behaviour.
    /// </summary>
    /// <typeparam name="TSource">The source type to map.</typeparam>
    /// <typeparam name="TDestination">The destination type to map.</typeparam>
    /// <param name="sourceOptions">An optional <see cref="MapperOptions" /> object containing configuration details for the source type being registered.</param>
    /// <param name="destinationOptions">An optional <see cref="MapperOptions" /> object containing configuration details for the destination type being registered.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterMap<TSource, TDestination>(MapperOptions sourceOptions, MapperOptions destinationOptions)
    {
        RegisterMap(typeof(TSource), sourceOptions, typeof(TDestination), destinationOptions);
        return this;
    }

    /// <summary>
    /// Registers a specific type-to-type configuration. When registering via <see cref="RegisterType" /> only 1 endpoint
    /// is specified, and the <see cref="ObjectMapper" /> automatically joins members based on member name. In cases
    /// where a specific mapping between 2 types is required, this method can be used to provide custom behaviour.
    /// </summary>
    /// <param name="sourceType">The source type participating in the map.</param>
    /// <param name="sourceOptions">The source options.</param>
    /// <param name="destinationType">The destination type participating in the map.</param>
    /// <param name="destinationOptions">The destination options.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterMap(Type sourceType, MapperOptions sourceOptions, Type destinationType, MapperOptions destinationOptions)
    {
        var key = new Tuple<Type, Type>(
            sourceType,
            destinationType
        );
        var value = new Tuple<MapperTypeConfiguration, MapperTypeConfiguration>(
            new MapperTypeConfiguration
            {
                Type = sourceType,
                Options = sourceOptions
            },
            new MapperTypeConfiguration
            {
                Type = destinationType,
                Options = destinationOptions
            }
        );
        return this;
    }

    #endregion 

    #region Ignore Members

    /// <summary>
    /// Defines a member that will not be mapped.
    /// </summary>
    /// <typeparam name="T">The source object type.</typeparam>
    /// <param name="member">A unary member expression to select a member on the source object type.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration Ignore<T>(Expression<Func<T, object>> member)
    {
        ConfigIgnoreIncludes.Add(new ConfigIgnoreInclude()
        {
            Type = typeof(T),
            Member = member.GetMemberPath(),
            IgnoreInclude = IgnoreIncludeEnum.Ignore
        });
        return this;
    }

    /// <summary>
    /// Sets one or more members on a type to be ignored for mapping purposes.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="members">The list of members to ignore</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration Ignore(Type type, params string[] members)
    {
        members.ToList().ForEach(m =>
        {
            ConfigIgnoreIncludes.Add(new ConfigIgnoreInclude()
            {
                Type = type,
                Member = m,
                IgnoreInclude = IgnoreIncludeEnum.Ignore
            });
        });
        return this;
    }

    #endregion

    #region Include Members

    /// <summary>
    /// Defines a member that will be mapped.
    /// </summary>
    /// <typeparam name="T">The source object type.</typeparam>
    /// <param name="member">A unary member expression to select a member on the source object type.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration Include<T>(Expression<Func<T, object>> member)
    {
        ConfigIgnoreIncludes.Add(new ConfigIgnoreInclude()
        {
            Type = typeof(T),
            Member = member.GetMemberPath(),
            IgnoreInclude = IgnoreIncludeEnum.Include
        });
        return this;
    }

    /// <summary>
    /// Sets one or more members on a type to be included for mapping purposes.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="members">The list of members to include.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration Include(Type type, params string[] members)
    {
        members.ToList().ForEach(m =>
        {
            ConfigIgnoreIncludes.Add(new ConfigIgnoreInclude()
            {
                Type = type,
                Member = m,
                IgnoreInclude = IgnoreIncludeEnum.Include
            });
        });
        return this;
    }

    #endregion

    # region Member Filter Rule

    /// <summary>
    /// Sets a member filter rule for a single type.
    /// </summary>
    /// <typeparam name="T">The type to set the member filter rule for.</typeparam>
    /// <param name="memberFilterRule">The filter rule.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration SetMemberFilterRule<T>(MemberFilterDelegate memberFilterRule)
    {
        var type = typeof(T);
        return SetMemberFilterRule(type, memberFilterRule);
    }

    /// <summary>
    /// Sets a member filter rule for a single type.
    /// </summary>
    /// <param name="type">The type to set the member filter rule on.</param>
    /// <param name="memberFilterRule">The filter rule.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration SetMemberFilterRule(Type type, MemberFilterDelegate memberFilterRule)
    {
        ConfigMemberFilterRules[type] = memberFilterRule;
        return this;
    }

    #endregion

    #region Converters

    /// <summary>
    /// Adds a type converter. Type converters are used to convert simple / native types where the members in the
    /// source and destinations have different types.
    /// </summary>
    /// <typeparam name="T">The type of the source member.</typeparam>
    /// <typeparam name="U">The type of the destination member.</typeparam>
    /// <param name="converter">A converter func.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration RegisterConverter<T, U>(Func<T, U> converter)
    {
        if (this.ConfigConverters.Any(c => c.Key.Item1 == typeof(T) && c.Key.Item2 == typeof(U)))
        {
            throw new Exception("A converter between these 2 types has already been defined.");
        }

        TypeConverter<T, U> typeConverter = new TypeConverter<T, U>(converter);
        Tuple<Type, Type> k = new Tuple<Type, Type>(typeof(T), typeof(U));
        this.ConfigConverters[k] = typeConverter;
        return this;
    }

    #endregion

    #region Rename members

    /// <summary>
    /// Defines a custom name for a member when mapping to other types.
    /// </summary>
    /// <typeparam name="T">The source object type.</typeparam>
    /// <param name="member">A unary member expression to select a member on the source object type.</param>
    /// <param name="newName">The new name for the member.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public MapperConfiguration Rename<T>(Expression<Func<T, object>> member, string newName)
    {
        return Rename(typeof(T), member.GetMemberPath(), newName);
    }

    /// <summary>
    /// Defines a custom name for a member when mapping to other types.
    /// </summary>
    /// <param name="type">The type containing the member.</param>
    /// <param name="member">The member to rename.</param>
    /// <param name="newName">The new name for the member.</param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public MapperConfiguration Rename(Type type, string member, string newName)
    {
        ConfigRenames.Add(new ConfigRename()
        {
            Type = type,
            MemberName = member,
            InternalMemberName = newName
        });
        return this;
    }

    #endregion

    #region Private Members

    /// <summary>
    /// Maps a member from source to destination using lambda expressions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="fromMember"></param>
    /// <param name="toMember"></param>
    /// <returns>Returns the current <see cref="MapperConfiguration" /> instance.</returns>
    public MapperConfiguration MapMember<T, U>(Expression<Func<T, object>> fromMember, Expression<Func<U, object>> toMember)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Validates the mapper configuration
    /// </summary>
    public void Validate()
    {
        // ensure all mapping rules are connected

    }

    private bool GetIgnoreStatus(Type type, string member)
    {
        MemberFilterDelegate? memberFilterRuleA = null;
        MemberFilterDelegate? memberFilterRuleB = null;
        MemberFilterDelegate? memberFilterRule = null;

        if (ConfigTypes.ContainsKey(type))
        {
            throw new Exception($"GetIgnoreStatus. Invalid type: {type}.");
        }

        // Get member filter function if exists
        if (ConfigMemberFilterRules.ContainsKey(type))
        {
            memberFilterRuleA = ConfigMemberFilterRules[type];
        }

        memberFilterRuleB = ConfigTypes[type].Options.MemberFilterRule;
        memberFilterRule = memberFilterRuleA != null ? memberFilterRuleA : memberFilterRuleB;

        var ignore = (memberFilterRule != null) ? memberFilterRule(member) : false;
        foreach (var configIgnoreInclude in ConfigIgnoreIncludes.Where(c => c.Type == type && c.Member.Equals(member, StringComparison.Ordinal)))
        {
            ignore = configIgnoreInclude.IgnoreInclude == IgnoreIncludeEnum.Ignore ? true : false;
        }
        return ignore;
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
        foreach (var rename in ConfigRenames.Where(c => c.Type == type && c.MemberName.Equals(memberName, StringComparison.Ordinal)))
        {
            internalMemberName = rename.InternalMemberName;
        }
        return internalMemberName;
    }

    /// <summary>
    /// Takes all the configuration and builds an <see cref="ObjectMapper" /> object that can then be used to map objects.
    /// </summary>
    /// <returns>Returns a configured <see cref="ObjectMapper" /> object.</returns>
    public ObjectMapper Build()
    {
        IDictionary<Type, MapperTypeConfiguration> buildTypes = new Dictionary<Type, MapperTypeConfiguration>();
        IDictionary<Tuple<Type, Type>, Tuple<MapperTypeConfiguration, MapperTypeConfiguration>> buildMaps = new Dictionary<Tuple<Type, Type>, Tuple<MapperTypeConfiguration, MapperTypeConfiguration>>();

        // Add core resolvers
        if (!this.ConfigResolvers.Select(r => r.GetType()).Contains(typeof(StructMemberResolver)))
        {
            this.ConfigResolvers.Add(new StructMemberResolver());
        }

        if (!this.ConfigResolvers.Select(r => r.GetType()).Contains(typeof(DictionaryMemberResolver)))
        {
            this.ConfigResolvers.Add(new DictionaryMemberResolver());
        }

        if (!this.ConfigResolvers.Select(r => r.GetType()).Contains(typeof(ClassMemberResolver)))
        {
            this.ConfigResolvers.Add(new ClassMemberResolver());
        }

        // go through each type
        foreach (var configTypeKey in this.ConfigTypes.Keys)
        {
            var configType = this.ConfigTypes[configTypeKey];

            IMemberResolver? resolver = null;

            // Get resolver
            foreach (var configResolver in this.ConfigResolvers)
            {
                if (configResolver.CanResolveMembersForType(configType.Type))
                {
                    resolver = configResolver;
                    break;
                }
            }

            // if no resolver found, throw error
            if (resolver == null)
            {
                throw new Exception($"No member resolver found for type: ${configType.Type}.");
            }

            // Get members
            string[] members = new string[] { };
            List<MapperMemberConfiguration> buildMembers = new List<MapperMemberConfiguration>();
            if (!resolver.DeferMemberResolution)
            {
                members = resolver.GetTypeMembers(configType.Type, configType.Options);
                buildMembers = members.Select(m => new MapperMemberConfiguration
                {
                    MemberName = m,
                    DataType = resolver.GetMemberType(configType.Type, m, configType.Options),
                    Getter = resolver.GetGetter(configType.Type, m, configType.Options),
                    Setter = resolver.GetSetter(configType.Type, m, configType.Options),
                    Ignore = GetIgnoreStatus(configType.Type, m),
                    InternalMemberName = GetInternalName(configType.Type, m, configType.Options.MemberRenameStrategy)
                }).ToList();
            }

            // Create build type
            buildTypes[configType.Type] = new MapperTypeConfiguration
            {
                Type = configType.Type,
                Options = configType.Options,
                MemberResolver = resolver,
                MemberConfiguration = buildMembers,
            };
        }

        // Add calculations to existing types

        foreach (var configCalculation in this.ConfigCalculations)
        {
            var sourceType = configCalculation.SourceType;
            if (!buildTypes.ContainsKey(sourceType))
            {
                throw new Exception($"Calculation exists for missing source type: ${sourceType.Name}.");
            }

            var configType = buildTypes[sourceType];
            configType.MemberConfiguration.Add(new MapperMemberConfiguration()
            {
                MemberName = configCalculation.MemberName,
                DataType = configCalculation.MemberType,
                InternalMemberName = configCalculation.MemberName,
                Ignore = false,
                Getter = configCalculation.Calculation.Convert,
                Setter = null,
                Calculation = configCalculation.Calculation
            });
        }

        // Validation
        foreach (var buildTypeKey in buildTypes.Keys)
        {
            buildTypes[buildTypeKey].Validate();
        }

        return new ObjectMapper(buildTypes, this.ConfigConverters);
    }

    #endregion
}