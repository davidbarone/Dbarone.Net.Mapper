namespace Dbarone.Net.Mapper;
using Dbarone.Net.Extensions;
using System.Linq.Expressions;

/// <summary>
/// The ObjectMapper class provides mapping functions to transform objects from one type to another. 
/// </summary>
public class ObjectMapper
{
    private IDictionary<Type, MapperTypeConfiguration> configuration;
    private IDictionary<Tuple<Type, Type>, ITypeConverter> customTypeConverters;

    #region Configuration

    /// <summary>
    /// Returns the number of types configured.
    /// </summary>
    /// <returns>Returns the number of types configured.</returns>
    public Type[] GetTypeConfigurationKeys()
    {
        return configuration.Keys.ToArray();
    }

    /// <summary>
    /// Gets the <see cref="MapperTypeConfiguration" /> configuration for a specific type.
    /// </summary>
    /// <param name="type">The type to get the configuration for.</param>
    /// <returns>Returns a <see cref="MapperTypeConfiguration" /> object representing the specified configuration.</returns>
    public MapperTypeConfiguration GetTypeConfiguration(Type type)
    {
        return configuration[type];
    }

    #endregion

    /// <summary>
    /// Returns the mapper configuration.
    /// </summary>
    /// 
    public IDictionary<Type, MapperTypeConfiguration> Configuration { get; }

    internal ObjectMapper(IDictionary<Type, MapperTypeConfiguration> configuration, IDictionary<Tuple<Type, Type>, ITypeConverter> customTypeConverters)
    {
        this.configuration = configuration;
        this.customTypeConverters = customTypeConverters;
    }

    internal object MapOneInternal(Type fromType, Type toType, object? obj, string memberPath, IDictionary<string, IEnumerable<MapperDelegate>> cachedMappings) {
        
    }

    /// <summary>
    /// Maps / transforms an object from one type to another.
    /// </summary>
    /// <param name="fromType">The type to transform the object from.</param>
    /// <param name="toType">The type to transform the object to.</param>
    /// <param name="obj">The object being transformed from. Must be assignable to `fromType`.</param>
    /// <returns>Returns a mapped object of type `toType`.</returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="MapperException"></exception>
    public object MapOne(Type fromType, Type toType, object? obj)
    {


        MapperTypeConfiguration fromTypeConfiguration = configuration[fromType];
        MapperTypeConfiguration toTypeConfiguration = configuration[toType];

        var newInstance = toTypeConfiguration.CreateInstance();

        // for dictionary + dynamic types, we need to get the source members now
        if (fromTypeConfiguration.MemberResolver.DeferMemberResolution)
        {
            var members = fromTypeConfiguration.MemberResolver.GetInstanceMembers(obj);
            var memberConfig = members.Select(m => new MapperMemberConfiguration
            {
                MemberName = m,
                DataType = fromTypeConfiguration.MemberResolver.GetMemberType(m),
                Getter = fromTypeConfiguration.MemberResolver.GetGetter(m),
                Setter = fromTypeConfiguration.MemberResolver.GetSetter(m)
            }).ToList();
            fromTypeConfiguration.MemberConfiguration = memberConfig;
            foreach (var item in fromTypeConfiguration.MemberConfiguration)
            {
                item.SetInternalMemberName(fromTypeConfiguration.Options.MemberRenameStrategy);
            }
            fromTypeConfiguration.Validate();
        }

        // Get members to map. By default, the members come from the 'to' side.
        // However, if the to type is deferred resolution, then the members come from the 'from' side.
        List<string> internalMemberNames = null;
        if (!toTypeConfiguration.MemberResolver.DeferMemberResolution)
        {
            internalMemberNames = toTypeConfiguration.MemberConfiguration.Where(r => r.Ignore == false).Select(m => m.InternalMemberName).ToList();
        }
        else
        {
            internalMemberNames = fromTypeConfiguration.MemberConfiguration.Where(r => r.Ignore == false).Select(m => m.InternalMemberName).ToList();
        }

        // If to type is dictionary / dynamic (DeferMemberResolution = true)
        foreach (var internalMemberName in internalMemberNames)
        {
            // Get from rule
            var toRule = toTypeConfiguration.MemberConfiguration.FirstOrDefault(mc => mc.InternalMemberName.Equals(internalMemberName, StringComparison.CurrentCultureIgnoreCase));
            var fromRule = fromTypeConfiguration.MemberConfiguration.FirstOrDefault(mc => mc.InternalMemberName.Equals(internalMemberName, StringComparison.CurrentCultureIgnoreCase));

            if (fromRule == null && (toTypeConfiguration.Options.EndPointValidation & MapperEndPoint.Destination) == MapperEndPoint.Destination)
            {
                throw new Exception($"Cannot find member: {toRule.InternalMemberName} in source mapping configuration.");
            }
            var fromObj = fromRule.Getter.Invoke(obj!);

            // Get type of from member:
            if (toRule.DataType != fromRule.DataType)
            {
                // Check for custom type converter:
                var validConverterKey = this.customTypeConverters.Keys.FirstOrDefault(k => k.Item1 == fromRule.DataType && k.Item2 == toRule.DataType);
                if (validConverterKey != null)
                {
                    ITypeConverter typeConverter = this.customTypeConverters[validConverterKey];
                    var convertedObject = typeConverter.Convert(fromObj);
                    toRule.Setter.Invoke(newInstance, convertedObject!);
                }
                else
                {
                    throw new MapperException($"Cannot map from type: {fromRule.DataType} to type: {toRule.DataType}. Are you missing a Type Converter?");
                }
            }


        }
        return newInstance;
    }

    /// <summary>
    /// Maps / transforms an object from one type to another.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="obj">Returns an object of the destination type.</param>
    /// <returns></returns>
    public TDestination? MapOne<TSource, TDestination>(TSource? obj)
    {
        return (TDestination?)MapOne(typeof(TSource), typeof(TDestination), obj);
    }

    /// <summary>
    /// Maps a collection, list or array of items.
    /// </summary>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <typeparam name="TDestination">The type of the destination object.</typeparam>
    /// <param name="obj">The source object. Must be an enumerable, collection, list or array of type U.</param>
    /// <returns></returns>
    public IEnumerable<TDestination?> MapMany<TSource, TDestination>(IEnumerable<TSource?> obj)
    {
        foreach (var item in obj)
        {
            yield return MapOne<TSource, TDestination>(item);
        }
    }

    private IEnumerable<MapperDelegate> Build(Type sourceType, Type destinationType)
    {
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