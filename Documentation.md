<a id='top'></a>
# Assembly: Dbarone.Net.Mapper
## Contents
- [MapperIgnoreAttribute](#dbaronenetmappermapperignoreattribute)
- [MapperConfiguration](#dbaronenetmappermapperconfiguration)
  - [GetTypeConfigurationCount](#dbaronenetmappermapperconfigurationgettypeconfigurationcount)
  - [GetTypeConfiguration](#dbaronenetmappermapperconfigurationgettypeconfiguration(systemtype))
  - [Create](#dbaronenetmappermapperconfigurationcreate)
  - [RegisterTypes](#dbaronenetmappermapperconfigurationregistertypes(systemtype[],dbaronenetmappermapperoptions))
  - [RegisterType](#dbaronenetmappermapperconfigurationregistertype``1(dbaronenetmappermapperoptions))
  - [RegisterType](#dbaronenetmappermapperconfigurationregistertype(systemtype,dbaronenetmappermapperoptions))
  - [RegisterCalculation](#dbaronenetmappermapperconfigurationregistercalculation``2(systemstring,systemfunc{``0,``1}))
  - [RegisterMap](#dbaronenetmappermapperconfigurationregistermap``2(dbaronenetmappermapperoptions,dbaronenetmappermapperoptions))
  - [Ignore](#dbaronenetmappermapperconfigurationignore``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}}))
  - [Ignore](#dbaronenetmappermapperconfigurationignore(systemtype,systemstring[]))
  - [RegisterConverter](#dbaronenetmappermapperconfigurationregisterconverter``2(systemfunc{``0,``1}))
  - [Rename](#dbaronenetmappermapperconfigurationrename``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemstring))
  - [Rename](#dbaronenetmappermapperconfigurationrename(systemtype,systemstring,systemstring))
  - [MapMember](#dbaronenetmappermapperconfigurationmapmember``2(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemlinqexpressionsexpression{systemfunc{``1,systemobject}}))
  - [Validate](#dbaronenetmappermapperconfigurationvalidate)
  - [Build](#dbaronenetmappermapperconfigurationbuild)
- [MapperMemberConfiguration](#dbaronenetmappermappermemberconfiguration)
  - [MemberName](#dbaronenetmappermappermemberconfigurationmembername)
  - [DataType](#dbaronenetmappermappermemberconfigurationdatatype)
  - [InternalMemberName](#dbaronenetmappermappermemberconfigurationinternalmembername)
  - [Ignore](#dbaronenetmappermappermemberconfigurationignore)
  - [Getter](#dbaronenetmappermappermemberconfigurationgetter)
  - [Setter](#dbaronenetmappermappermemberconfigurationsetter)
  - [IsCalculation](#dbaronenetmappermappermemberconfigurationiscalculation)
  - [Calculation](#dbaronenetmappermappermemberconfigurationcalculation)
- [MapperOptions](#dbaronenetmappermapperoptions)
  - [IncludeFields](#dbaronenetmappermapperoptionsincludefields)
  - [IncludePrivateMembers](#dbaronenetmappermapperoptionsincludeprivatemembers)
  - [MemberRenameStrategy](#dbaronenetmappermapperoptionsmemberrenamestrategy)
  - [EndPointValidation](#dbaronenetmappermapperoptionsendpointvalidation)
- [MapperTypeConfiguration](#dbaronenetmappermappertypeconfiguration)
  - [Type](#dbaronenetmappermappertypeconfigurationtype)
  - [Options](#dbaronenetmappermappertypeconfigurationoptions)
  - [MemberConfiguration](#dbaronenetmappermappertypeconfigurationmemberconfiguration)
  - [MemberResolver](#dbaronenetmappermappertypeconfigurationmemberresolver)
  - [GetMemberRule](#dbaronenetmappermappertypeconfigurationgetmemberrule(systemlinqexpressionsexpression))
  - [CreateInstance](#dbaronenetmappermappertypeconfigurationcreateinstance)
- [MapperEndPoint](#dbaronenetmappermapperendpoint)
  - [None](#dbaronenetmappermapperendpointnone)
  - [Source](#dbaronenetmappermapperendpointsource)
  - [Destination](#dbaronenetmappermapperendpointdestination)
- [CaseChangeMemberRenameStrategy](#dbaronenetmappercasechangememberrenamestrategy)
  - [#ctor](#dbaronenetmappercasechangememberrenamestrategy#ctor(dbaronenetextensionscasetype,dbaronenetextensionscasetype))
  - [RenameMember](#dbaronenetmappercasechangememberrenamestrategyrenamemember(systemstring))
- [IMemberRenameStrategy](#dbaronenetmapperimemberrenamestrategy)
  - [RenameMember](#dbaronenetmapperimemberrenamestrategyrenamemember(systemstring))
- [PrefixSuffix](#dbaronenetmapperprefixsuffix)
  - [Prefix](#dbaronenetmapperprefixsuffixprefix)
  - [Suffix](#dbaronenetmapperprefixsuffixsuffix)
- [PrefixSuffixMemberRenameStrategy](#dbaronenetmapperprefixsuffixmemberrenamestrategy)
  - [#ctor](#dbaronenetmapperprefixsuffixmemberrenamestrategy#ctor(dbaronenetmapperprefixsuffix,systemstring))
  - [RenameMember](#dbaronenetmapperprefixsuffixmemberrenamestrategyrenamemember(systemstring))
- [ClassMemberResolver](#dbaronenetmapperclassmemberresolver)
- [CreateInstance](#dbaronenetmappercreateinstance)
- [DictionaryMemberResolver](#dbaronenetmapperdictionarymemberresolver)
- [Getter](#dbaronenetmappergetter)
- [IMemberResolver](#dbaronenetmapperimemberresolver)
- [Setter](#dbaronenetmappersetter)
- [StructMemberResolver](#dbaronenetmapperstructmemberresolver)
- [ObjectMapper](#dbaronenetmapperobjectmapper)
  - [MapOne](#dbaronenetmapperobjectmappermapone(systemtype,systemtype,systemobject))
  - [MapOne](#dbaronenetmapperobjectmappermapone``2(``0))
  - [MapMany](#dbaronenetmapperobjectmappermapmany``2(systemcollectionsgenericienumerable{``0}))
  - [Validate](#dbaronenetmapperobjectmappervalidate``2)
- [ITypeConverter](#dbaronenetmapperitypeconverter)
  - [Convert](#dbaronenetmapperitypeconverterconvert(systemobject))
- [TypeConverter`2](#dbaronenetmappertypeconverter`2)
  - [#ctor](#dbaronenetmappertypeconverter`2#ctor(systemfunc{`0,`1}))
  - [Convert](#dbaronenetmappertypeconverter`2convert(systemobject))
- [NamingConvention](#namingconvention)
  - [None](#namingconventionnone)
  - [CamelCaseNamingConvention](#namingconventioncamelcasenamingconvention)
  - [PascalCaseNamingConvention](#namingconventionpascalcasenamingconvention)
  - [SnakeCasingConvention](#namingconventionsnakecasingconvention)
- [MapperException](#mapperexception)
  - [#ctor](#mapperexception#ctor)
  - [#ctor](#mapperexception#ctor(systemstring))



---
## Dbarone.Net.Mapper.MapperIgnoreAttribute
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Attribute to indicate that a property should not be mapped. 


---
## Dbarone.Net.Mapper.MapperConfiguration
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Creates configuration for a [ObjectMapper](#dbaronenetmapperobjectmapper) mapper object. Before being able to map any objects and types, you must create a mapper configuration, and from this generate a [ObjectMapper](#dbaronenetmapperobjectmapper) object. 

>### <a id='dbaronenetmappermapperconfigurationgettypeconfigurationcount'></a>method: GetTypeConfigurationCount
#### Signature
``` c#
MapperConfiguration.GetTypeConfigurationCount()
```
#### Summary
 Returns the number of types configured. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationgettypeconfiguration(systemtype)'></a>method: GetTypeConfiguration
#### Signature
``` c#
MapperConfiguration.GetTypeConfiguration(System.Type type)
```
#### Summary
 Gets the [MapperTypeConfiguration](#dbaronenetmappermappertypeconfiguration) configuration for a specific type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to get the configuration for.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationcreate'></a>method: Create
#### Signature
``` c#
MapperConfiguration.Create()
```
#### Summary
 Creates a new [MapperConfiguration](#dbaronenetmappermapperconfiguration) instance. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregistertypes(systemtype[],dbaronenetmappermapperoptions)'></a>method: RegisterTypes
#### Signature
``` c#
MapperConfiguration.RegisterTypes(System.Type[] types, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Registers a collection of types. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|types: |An array of Types.|
|options: |An optional [MapperOptions](#dbaronenetmappermapperoptions) object containing configuration details for all the types to be registered.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregistertype``1(dbaronenetmappermapperoptions)'></a>method: RegisterType
#### Signature
``` c#
MapperConfiguration.RegisterType<T>(Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Registers a type using generic types. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of the entity to register.|

#### Parameters:
|Name | Description |
|-----|------|
|options: |An optional [MapperOptions](#dbaronenetmappermapperoptions) object containing configuration details for the type being registered.|

#### Exceptions:
None
#### Examples:

#### Examples:


The following example shows how to register a type:
``` c#
    // Configure mapper
    var mapper = MapperConfiguration
        .Create()
        .RegisterType<CustomerEntity>()
        .RegisterType<CustomerModel>()
        .Build();
        
    // Map object
    var obj2 = mapper.MapOne<CustomerEntity, CustomerModel>(obj1); 
    
```



<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregistertype(systemtype,dbaronenetmappermapperoptions)'></a>method: RegisterType
#### Signature
``` c#
MapperConfiguration.RegisterType(System.Type type, Dbarone.Net.Mapper.MapperOptions options)
```
#### Summary
 Registers a single type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type to be registered.|
|options: |An optional [MapperOptions](#dbaronenetmappermapperoptions) object containing configuration details for the type being registered.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregistercalculation``2(systemstring,systemfunc{``0,``1})'></a>method: RegisterCalculation
#### Signature
``` c#
MapperConfiguration.RegisterCalculation<TSource, TReturn>(System.String memberName, System.Func<TSource,TReturn> calculation)
```
#### Summary
 Registers a calculation for a type. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|TSource: |The source entity type.|
|TReturn: |The type of the return value for the calculated member.|

#### Parameters:
|Name | Description |
|-----|------|
|memberName: |The calculated member name.|
|calculation: |The calculation.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregistermap``2(dbaronenetmappermapperoptions,dbaronenetmappermapperoptions)'></a>method: RegisterMap
#### Signature
``` c#
MapperConfiguration.RegisterMap<TSource, TDestination>(Dbarone.Net.Mapper.MapperOptions sourceOptions, Dbarone.Net.Mapper.MapperOptions destinationOptions)
```
#### Summary
 Registers a specific type-to-type configuration. When registering via [RegisterType](#dbaronenetmappermapperconfigurationregistertype(systemtype,dbaronenetmappermapperoptions)) only 1 endpoint is specified, and the [ObjectMapper](#dbaronenetmapperobjectmapper) automatically joins members based on member name. In cases where a specific mapping between 2 types is required, this method can be used to provide custom behaviour. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|TSource: |The source type to map.|
|TDestination: |The destination type to map.|

#### Parameters:
|Name | Description |
|-----|------|
|sourceOptions: |An optional [MapperOptions](#dbaronenetmappermapperoptions) object containing configuration details for the source type being registered.|
|destinationOptions: |An optional [MapperOptions](#dbaronenetmappermapperoptions) object containing configuration details for the destination type being registered.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationignore``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}})'></a>method: Ignore
#### Signature
``` c#
MapperConfiguration.Ignore<T>(System.Linq.Expressions.Expression<System.Func<T,System.Object>> member)
```
#### Summary
 Defines a member that will not be mapped. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The source object type.|

#### Parameters:
|Name | Description |
|-----|------|
|member: |A unary member expression to select a member on the source object type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationignore(systemtype,systemstring[])'></a>method: Ignore
#### Signature
``` c#
MapperConfiguration.Ignore(System.Type type, System.String[] members)
```
#### Summary
 Sets one or more members on a type to be ignored for mapping purposes. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type.|
|members: |The list of members to ignore|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationregisterconverter``2(systemfunc{``0,``1})'></a>method: RegisterConverter
#### Signature
``` c#
MapperConfiguration.RegisterConverter<T, U>(System.Func<T,U> converter)
```
#### Summary
 Adds a type converter. Type converters are used to convert simple / native types where the members in the source and destinations have different types. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of the source member.|
|U: |The type of the destination member.|

#### Parameters:
|Name | Description |
|-----|------|
|converter: |A converter func.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationrename``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemstring)'></a>method: Rename
#### Signature
``` c#
MapperConfiguration.Rename<T>(System.Linq.Expressions.Expression<System.Func<T,System.Object>> member, System.String newName)
```
#### Summary
 Defines a custom name for a member when mapping to other types. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The source object type.|

#### Parameters:
|Name | Description |
|-----|------|
|member: |A unary member expression to select a member on the source object type.|
|newName: |The new name for the member.|

#### Exceptions:

Exception thrown: [T:System.ArgumentNullException](#T:System.ArgumentNullException): 

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationrename(systemtype,systemstring,systemstring)'></a>method: Rename
#### Signature
``` c#
MapperConfiguration.Rename(System.Type type, System.String member, System.String newName)
```
#### Summary
 Defines a custom name for a member when mapping to other types. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: |The type containing the member.|
|member: |The member to rename.|
|newName: |The new name for the member.|

#### Exceptions:

Exception thrown: [T:System.ArgumentNullException](#T:System.ArgumentNullException): 

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationmapmember``2(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemlinqexpressionsexpression{systemfunc{``1,systemobject}})'></a>method: MapMember
#### Signature
``` c#
MapperConfiguration.MapMember<T, U>(System.Linq.Expressions.Expression<System.Func<T,System.Object>> fromMember, System.Linq.Expressions.Expression<System.Func<U,System.Object>> toMember)
```
#### Summary
 Maps a member from source to destination using lambda expressions. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: ||
|U: ||

#### Parameters:
|Name | Description |
|-----|------|
|fromMember: ||
|toMember: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationvalidate'></a>method: Validate
#### Signature
``` c#
MapperConfiguration.Validate()
```
#### Summary
 Validates the mapper configuration 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermapperconfigurationbuild'></a>method: Build
#### Signature
``` c#
MapperConfiguration.Build()
```
#### Summary
 Takes all the configuration and builds an [ObjectMapper](#dbaronenetmapperobjectmapper) object that can then be used to map objects. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.MapperMemberConfiguration
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines the rules for a single member mapping. 

>### property: MemberName
#### Summary
 Member name for the mapping rule. 


<small>[Back to top](#top)</small>
>### property: DataType
#### Summary
 Member data type. 


<small>[Back to top](#top)</small>
>### property: InternalMemberName
#### Summary
 The internal member name. Mapping from source to destination is done via matching internal names. 


<small>[Back to top](#top)</small>
>### property: Ignore
#### Summary
 Set to true to ignore this member in the mapping configuration. 


<small>[Back to top](#top)</small>
>### property: Getter
#### Summary
 Delegate method to get the value from the instance. 


<small>[Back to top](#top)</small>
>### property: Setter
#### Summary
 Delegate method to set the value to the instance. 


<small>[Back to top](#top)</small>
>### property: IsCalculation
#### Summary
 Set to true if a calculation. 


<small>[Back to top](#top)</small>
>### property: Calculation
#### Summary
 Used if the member is a custom calculation. 


<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.MapperOptions
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines a set of mapping options. 

>### property: IncludeFields
#### Summary
 Set to true to include mapping of fields as well as properties. Default is false. 


<small>[Back to top](#top)</small>
>### property: IncludePrivateMembers
#### Summary
 Set to true to include private fields and properties. Default is false.1 


<small>[Back to top](#top)</small>
>### property: MemberRenameStrategy
#### Summary
 Optional member renaming strategy. 


<small>[Back to top](#top)</small>
>### property: EndPointValidation
#### Summary
 Defines implicit assertion of mapping rules prior to any map function call. Defaults to 'None'. 


<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.MapperTypeConfiguration
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines the configuration of a single type. 

>### property: Type
#### Summary
 The type the configuration relates to. 


<small>[Back to top](#top)</small>
>### property: Options
#### Summary
 Defines the options for the map registration 


<small>[Back to top](#top)</small>
>### property: MemberConfiguration
#### Summary
 Defines the member configuration. 


<small>[Back to top](#top)</small>
>### property: MemberResolver
#### Summary
 Provides the member resolving strategy for this type. 


<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappermappertypeconfigurationgetmemberrule(systemlinqexpressionsexpression)'></a>method: GetMemberRule
#### Signature
``` c#
MapperTypeConfiguration.GetMemberRule(System.Linq.Expressions.Expression expr)
```
#### Summary
 Resolves a member/unary expression to a member configuration. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|expr: |A unary expression to select a member.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### property: CreateInstance
#### Summary
 Create a new instance of the type. 


<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.MapperEndPoint
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines the mapper end point type. 

>### field: None
#### Summary
 No end point type specified. 


<small>[Back to top](#top)</small>
>### field: Source
#### Summary
 Source mapper endpoint. 


<small>[Back to top](#top)</small>
>### field: Destination
#### Summary
 Destination mapper endpoint. 


<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.CaseChangeMemberRenameStrategy
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Converts member names from specified case type to a common (lowercase). Implementation of [IMemberRenameStrategy](#dbaronenetmapperimemberrenamestrategy). 

>### <a id='dbaronenetmappercasechangememberrenamestrategy#ctor(dbaronenetextensionscasetype,dbaronenetextensionscasetype)'></a>method: #ctor
#### Signature
``` c#
CaseChangeMemberRenameStrategy.#ctor(Dbarone.Net.Extensions.CaseType matchCase, Dbarone.Net.Extensions.CaseType newCase)
```
#### Summary
 Creates a new instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|matchCase: |The case to match.|
|newCase: |The case to change the member to.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappercasechangememberrenamestrategyrenamemember(systemstring)'></a>method: RenameMember
#### Signature
``` c#
CaseChangeMemberRenameStrategy.RenameMember(System.String member)
```
#### Summary
 Renames a member. If the case of the member matches the `matchCase`, then it is converted to `newCase`. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|member: |The input member name.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.IMemberRenameStrategy
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Interface for classes that can provide member renaming strategies. 

>### <a id='dbaronenetmapperimemberrenamestrategyrenamemember(systemstring)'></a>method: RenameMember
#### Signature
``` c#
IMemberRenameStrategy.RenameMember(System.String member)
```
#### Summary
 Renames member names. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|member: |The member name.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.PrefixSuffix
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Denotes either prefix or suffix. 

>### field: Prefix
#### Summary
 Prefix (start of string). 


<small>[Back to top](#top)</small>
>### field: Suffix
#### Summary
 Suffix (end of string). 


<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.PrefixSuffixMemberRenameStrategy
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Removes prefix/suffix characters from member names. 

>### <a id='dbaronenetmapperprefixsuffixmemberrenamestrategy#ctor(dbaronenetmapperprefixsuffix,systemstring)'></a>method: #ctor
#### Signature
``` c#
PrefixSuffixMemberRenameStrategy.#ctor(Dbarone.Net.Mapper.PrefixSuffix stringType, System.String stringToRemove)
```
#### Summary
 Creates a new instance. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|stringType: |The [PrefixSuffix](#dbaronenetmapperprefixsuffix) type.|
|stringToRemove: |The string to remove.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperprefixsuffixmemberrenamestrategyrenamemember(systemstring)'></a>method: RenameMember
#### Signature
``` c#
PrefixSuffixMemberRenameStrategy.RenameMember(System.String member)
```
#### Summary
 Renames a member, removing either a Pre or Post 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|member: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.ClassMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 General resolver for classes. 


---
## Dbarone.Net.Mapper.CreateInstance
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Delegate to create a new instance of an object. 
|args: |Optional args to pass into the constructor.|
Returns: Returns an instance of an object.


---
## Dbarone.Net.Mapper.DictionaryMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Member resolver for dictionaries. 


---
## Dbarone.Net.Mapper.Getter
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines a basic getter delegate. 
|obj: |The object providing the value|
Returns: A value


---
## Dbarone.Net.Mapper.IMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Interface for describing methods to 


---
## Dbarone.Net.Mapper.Setter
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines a basic setter delegate. 
|target: |The object to set the value in.|
|value: |The value to set.|


---
## Dbarone.Net.Mapper.StructMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 General resolver for structs. 


---
## Dbarone.Net.Mapper.ObjectMapper
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 The ObjectMapper class provides mapping functions to transform objects from one type to another. 

>### <a id='dbaronenetmapperobjectmappermapone(systemtype,systemtype,systemobject)'></a>method: MapOne
#### Signature
``` c#
ObjectMapper.MapOne(System.Type fromType, System.Type toType, System.Object obj)
```
#### Summary
 Maps / transforms an object from one type to another. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|fromType: |The type to transform the object from.|
|toType: |The type to transform the object to.|
|obj: |The object being transformed from. Must be assignable to `fromType`.|

#### Exceptions:

Exception thrown: [T:System.Exception](#T:System.Exception): 

#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperobjectmappermapone``2(``0)'></a>method: MapOne
#### Signature
``` c#
ObjectMapper.MapOne<TSource, TDestination>(TSource obj)
```
#### Summary
 Maps / transforms an object from one type to another. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|TSource: ||
|TDestination: ||

#### Parameters:
|Name | Description |
|-----|------|
|obj: |Returns an object of the destination type.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperobjectmappermapmany``2(systemcollectionsgenericienumerable{``0})'></a>method: MapMany
#### Signature
``` c#
ObjectMapper.MapMany<TSource, TDestination>(System.Collections.Generic.IEnumerable<TSource> obj)
```
#### Summary
 Maps a collection, list or array of items. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|TSource: |The type of the source object.|
|TDestination: |The type of the destination object.|

#### Parameters:
|Name | Description |
|-----|------|
|obj: |The source object. Must be an enumerable, collection, list or array of type U.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmapperobjectmappervalidate``2'></a>method: Validate
#### Signature
``` c#
ObjectMapper.Validate<TSource, TDestination>()
```
#### Summary
 Validates the mapping between two types. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|TSource: ||
|TDestination: ||

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.ITypeConverter
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Interface for classes that can convert values / types. 

>### <a id='dbaronenetmapperitypeconverterconvert(systemobject)'></a>method: Convert
#### Signature
``` c#
ITypeConverter.Convert(System.Object obj)
```
#### Summary
 Converts an object. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|obj: |The object to be converted.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
## Dbarone.Net.Mapper.TypeConverter`2
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Converts an object using a generic lambda function or Func. 
|T: |The source type.|
|U: |The destination type.|

>### <a id='dbaronenetmappertypeconverter`2#ctor(systemfunc{`0,`1})'></a>method: #ctor
#### Signature
``` c#
TypeConverter<A, B>.#ctor(System.Func<`0,`1> converter)
```
#### Summary
 Creates a TypeConverter instance using a m 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|converter: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='dbaronenetmappertypeconverter`2convert(systemobject)'></a>method: Convert
#### Signature
``` c#
TypeConverter<A, B>.Convert(System.Object obj)
```
#### Summary
 Implementation of interface Convert method. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|obj: |The object to be converted.|

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>

---
## NamingConvention
### Namespace:
``
### Summary:
 Defines the default member naming convention for the type. 

>### field: None
#### Summary
 No defined naming convention used. 


<small>[Back to top](#top)</small>
>### field: CamelCaseNamingConvention
#### Summary
 Members are named in CamelCase, for example 'memberName'. 


<small>[Back to top](#top)</small>
>### field: PascalCaseNamingConvention
#### Summary
 Members are named in PascalCase, for example: 'MemberName'. 


<small>[Back to top](#top)</small>
>### field: SnakeCasingConvention
#### Summary
 Members are named in SnakeCase, for example 'member_name'. 


<small>[Back to top](#top)</small>

---
## MapperException
### Namespace:
``
### Summary:
 Mapper exception class. 

>### <a id='mapperexception#ctor'></a>method: #ctor
#### Signature
``` c#
MapperException.#ctor()
```
#### Summary
 Parameterless constructor. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
>### <a id='mapperexception#ctor(systemstring)'></a>method: #ctor
#### Signature
``` c#
MapperException.#ctor(System.String message)
```
#### Summary
 Create a mapper exception with message. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|message: ||

#### Exceptions:
None
#### Examples:
None

<small>[Back to top](#top)</small>
