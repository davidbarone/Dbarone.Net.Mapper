# Assembly: Dbarone.Net.Mapper
## Contents
- [MapperIgnoreAttribute](#dbaronenetmappermapperignoreattribute)
- [MapperConfiguration](#dbaronenetmappermapperconfiguration)
  - [GetTypeConfigurationCount](#dbaronenetmappermapperconfigurationgettypeconfigurationcount)
  - [GetTypeConfiguration](#dbaronenetmappermapperconfigurationgettypeconfiguration(systemtype))
  - [Create](#dbaronenetmappermapperconfigurationcreate)
  - [RegisterTypes](#dbaronenetmappermapperconfigurationregistertypes(systemtype[],dbaronenetmappermapperoptions))
  - [RegisterType<T>](#dbaronenetmappermapperconfigurationregistertype``1(dbaronenetmappermapperoptions))
  - [RegisterType](#dbaronenetmappermapperconfigurationregistertype(systemtype,dbaronenetmappermapperoptions))
  - [RegisterMap<T, U>](#dbaronenetmappermapperconfigurationregistermap``2(dbaronenetmappermapperoptions,dbaronenetmappermapperoptions))
  - [Ignore<T>](#dbaronenetmappermapperconfigurationignore``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}}))
  - [Ignore](#dbaronenetmappermapperconfigurationignore(systemtype,systemstring[]))
  - [RegisterConverter<T, U>](#dbaronenetmappermapperconfigurationregisterconverter``2(systemfunc{``0,``1}))
  - [Rename<T>](#dbaronenetmappermapperconfigurationrename``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemstring))
  - [Rename](#dbaronenetmappermapperconfigurationrename(systemtype,systemstring,systemstring))
  - [MapMember<T, U>](#dbaronenetmappermapperconfigurationmapmember``2(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemlinqexpressionsexpression{systemfunc{``1,systemobject}}))
  - [Validate](#dbaronenetmappermapperconfigurationvalidate)
  - [Build](#dbaronenetmappermapperconfigurationbuild)
- [MapperMemberConfiguration](#dbaronenetmappermappermemberconfiguration)
  - [MemberName](#dbaronenetmappermappermemberconfigurationmembername)
  - [DataType](#dbaronenetmappermappermemberconfigurationdatatype)
  - [InternalMemberName](#dbaronenetmappermappermemberconfigurationinternalmembername)
  - [Ignore](#dbaronenetmappermappermemberconfigurationignore)
  - [Getter](#dbaronenetmappermappermemberconfigurationgetter)
  - [Setter](#dbaronenetmappermappermemberconfigurationsetter)
- [MapperOptions](#dbaronenetmappermapperoptions)
  - [IncludeFields](#dbaronenetmappermapperoptionsincludefields)
  - [IncludePrivateMembers](#dbaronenetmappermapperoptionsincludeprivatemembers)
  - [MemberNameCaseType](#dbaronenetmappermapperoptionsmembernamecasetype)
  - [MemberRenameStrategy](#dbaronenetmappermapperoptionsmemberrenamestrategy)
  - [AssertMapEndPoint](#dbaronenetmappermapperoptionsassertmapendpoint)
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
- [ClassMemberResolver](#dbaronenetmapperclassmemberresolver)
- [CreateInstance](#dbaronenetmappercreateinstance)
- [DictionaryMemberResolver](#dbaronenetmapperdictionarymemberresolver)
- [Getter](#dbaronenetmappergetter)
- [IMemberResolver](#dbaronenetmapperimemberresolver)
- [Setter](#dbaronenetmappersetter)
- [ObjectMapper](#dbaronenetmapperobjectmapper)
  - [MapOne](#dbaronenetmapperobjectmappermapone(systemtype,systemtype,systemobject))
  - [MapOne<T, U>](#dbaronenetmapperobjectmappermapone``2(``0))
  - [MapMany<T, U>](#dbaronenetmapperobjectmappermapmany``2(systemcollectionsgenericienumerable{``0}))
  - [Validate<T, U>](#dbaronenetmapperobjectmappervalidate``2)
- [IMemberRenameStrategy](#dbaronenetmapperimemberrenamestrategy)
  - [RenameMember](#dbaronenetmapperimemberrenamestrategyrenamemember(systemstring))
- [ITypeConverter](#dbaronenetmapperitypeconverter)
  - [Convert](#dbaronenetmapperitypeconverterconvert(systemobject))
- [PrefixSuffix](#dbaronenetmapperprefixsuffix)
  - [Prefix](#dbaronenetmapperprefixsuffixprefix)
  - [Suffix](#dbaronenetmapperprefixsuffixsuffix)
- [PrefixSuffixMemberRenameStrategy](#dbaronenetmapperprefixsuffixmemberrenamestrategy)
  - [#ctor](#dbaronenetmapperprefixsuffixmemberrenamestrategy#ctor(dbaronenetmapperprefixsuffix,systemstring))
  - [RenameMember](#dbaronenetmapperprefixsuffixmemberrenamestrategyrenamemember(systemstring))
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
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.GetTypeConfigurationCount`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationgettypeconfiguration(systemtype)'></a>method: GetTypeConfiguration
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.GetTypeConfiguration(System.Type)`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationcreate'></a>method: Create
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Create`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationregistertypes(systemtype[],dbaronenetmappermapperoptions)'></a>method: RegisterTypes
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterTypes(System.Type[],Dbarone.Net.Mapper.MapperOptions)`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationregistertype``1(dbaronenetmappermapperoptions)'></a>method: RegisterType<T>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterType``1(Dbarone.Net.Mapper.MapperOptions)`</small>
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

#### Exceptions Thrown:
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


>### <a id='dbaronenetmappermapperconfigurationregistertype(systemtype,dbaronenetmappermapperoptions)'></a>method: RegisterType
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterType(System.Type,Dbarone.Net.Mapper.MapperOptions)`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationregistermap``2(dbaronenetmappermapperoptions,dbaronenetmappermapperoptions)'></a>method: RegisterMap<T, U>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterMap``2(Dbarone.Net.Mapper.MapperOptions,Dbarone.Net.Mapper.MapperOptions)`</small>
#### Signature
``` c#
MapperConfiguration.RegisterMap<T, U>(Dbarone.Net.Mapper.MapperOptions sourceOptions, Dbarone.Net.Mapper.MapperOptions destinationOptions)
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationignore``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}})'></a>method: Ignore<T>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Ignore``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}})`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationignore(systemtype,systemstring[])'></a>method: Ignore
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Ignore(System.Type,System.String[])`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationregisterconverter``2(systemfunc{``0,``1})'></a>method: RegisterConverter<T, U>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterConverter``2(System.Func{``0,``1})`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationrename``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemstring)'></a>method: Rename<T>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Rename``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.String)`</small>
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

#### Exceptions Thrown:

Exception thrown: [T:System.ArgumentNullException](#T:System.ArgumentNullException): 

#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationrename(systemtype,systemstring,systemstring)'></a>method: Rename
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Rename(System.Type,System.String,System.String)`</small>
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

#### Exceptions Thrown:

Exception thrown: [T:System.ArgumentNullException](#T:System.ArgumentNullException): 

#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationmapmember``2(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemlinqexpressionsexpression{systemfunc{``1,systemobject}})'></a>method: MapMember<T, U>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.MapMember``2(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Linq.Expressions.Expression{System.Func{``1,System.Object}})`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationvalidate'></a>method: Validate
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Validate`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappermapperconfigurationbuild'></a>method: Build
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Build`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None

---
## Dbarone.Net.Mapper.MapperMemberConfiguration
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines the rules for a single member mapping. 

>### property: MemberName
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.MemberName`</small>

#### Summary
 Member name for the mapping rule. 

>### property: DataType
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.DataType`</small>

#### Summary
 Member data type. 

>### property: InternalMemberName
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.InternalMemberName`</small>

#### Summary
 The internal member name. Mapping from source to destination is done via matching internal names. 

>### property: Ignore
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.Ignore`</small>

#### Summary
 Set to true to ignore this member in the mapping configuration. 

>### property: Getter
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.Getter`</small>

#### Summary
 Delegate method to get the value from the instance. 

>### property: Setter
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.Setter`</small>

#### Summary
 Delegate method to set the value to the instance. 


---
## Dbarone.Net.Mapper.MapperOptions
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines a set of mapping options. 

>### property: IncludeFields
<small>id: `P:Dbarone.Net.Mapper.MapperOptions.IncludeFields`</small>

#### Summary
 Set to true to include mapping of fields as well as properties. Default is false. 

>### property: IncludePrivateMembers
<small>id: `P:Dbarone.Net.Mapper.MapperOptions.IncludePrivateMembers`</small>

#### Summary
 Set to true to include private fields and properties. Default is false.1 

>### property: MemberNameCaseType
<small>id: `P:Dbarone.Net.Mapper.MapperOptions.MemberNameCaseType`</small>

#### Summary
 The default casing convention for members of the type. Default is CaseType.None. 

>### property: MemberRenameStrategy
<small>id: `P:Dbarone.Net.Mapper.MapperOptions.MemberRenameStrategy`</small>

#### Summary
 Optional member renaming strategy. 

>### property: AssertMapEndPoint
<small>id: `P:Dbarone.Net.Mapper.MapperOptions.AssertMapEndPoint`</small>

#### Summary
 Defines implicit assertion of mapping rules prior to any map function call. Defaults to 'None'. 


---
## Dbarone.Net.Mapper.MapperTypeConfiguration
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines the configuration of a single type. 

>### property: Type
<small>id: `P:Dbarone.Net.Mapper.MapperTypeConfiguration.Type`</small>

#### Summary
 The type the configuration relates to. 

>### property: Options
<small>id: `P:Dbarone.Net.Mapper.MapperTypeConfiguration.Options`</small>

#### Summary
 Defines the options for the map registration 

>### property: MemberConfiguration
<small>id: `P:Dbarone.Net.Mapper.MapperTypeConfiguration.MemberConfiguration`</small>

#### Summary
 Defines the member configuration. 

>### property: MemberResolver
<small>id: `P:Dbarone.Net.Mapper.MapperTypeConfiguration.MemberResolver`</small>

#### Summary
 Provides the member resolving strategy for this type. 

>### <a id='dbaronenetmappermappertypeconfigurationgetmemberrule(systemlinqexpressionsexpression)'></a>method: GetMemberRule
<small>id: `M:Dbarone.Net.Mapper.MapperTypeConfiguration.GetMemberRule(System.Linq.Expressions.Expression)`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### property: CreateInstance
<small>id: `P:Dbarone.Net.Mapper.MapperTypeConfiguration.CreateInstance`</small>

#### Summary
 Create a new instance of the type. 


---
## Dbarone.Net.Mapper.MapperEndPoint
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines the mapper end point type. 

>### field: None
<small>id: `F:Dbarone.Net.Mapper.MapperEndPoint.None`</small>

#### Summary
 No end point type specified. 

>### field: Source
<small>id: `F:Dbarone.Net.Mapper.MapperEndPoint.Source`</small>

#### Summary
 Source mapper endpoint. 

>### field: Destination
<small>id: `F:Dbarone.Net.Mapper.MapperEndPoint.Destination`</small>

#### Summary
 Destination mapper endpoint. 


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
## Dbarone.Net.Mapper.ObjectMapper
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 The ObjectMapper class provides mapping functions to transform objects from one type to another. 

>### <a id='dbaronenetmapperobjectmappermapone(systemtype,systemtype,systemobject)'></a>method: MapOne
<small>id: `M:Dbarone.Net.Mapper.ObjectMapper.MapOne(System.Type,System.Type,System.Object)`</small>
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

#### Exceptions Thrown:

Exception thrown: [T:System.Exception](#T:System.Exception): 

#### Examples:
None
>### <a id='dbaronenetmapperobjectmappermapone``2(``0)'></a>method: MapOne<T, U>
<small>id: `M:Dbarone.Net.Mapper.ObjectMapper.MapOne``2(``0)`</small>
#### Signature
``` c#
ObjectMapper.MapOne<T, U>(TSource obj)
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmapperobjectmappermapmany``2(systemcollectionsgenericienumerable{``0})'></a>method: MapMany<T, U>
<small>id: `M:Dbarone.Net.Mapper.ObjectMapper.MapMany``2(System.Collections.Generic.IEnumerable{``0})`</small>
#### Signature
``` c#
ObjectMapper.MapMany<T, U>(System.Collections.Generic.IEnumerable<TSource> obj)
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmapperobjectmappervalidate``2'></a>method: Validate<T, U>
<small>id: `M:Dbarone.Net.Mapper.ObjectMapper.Validate``2`</small>
#### Signature
``` c#
ObjectMapper.Validate<T, U>()
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

#### Exceptions Thrown:
None
#### Examples:
None

---
## Dbarone.Net.Mapper.IMemberRenameStrategy
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Interface for classes that can provide member renaming strategies. 

>### <a id='dbaronenetmapperimemberrenamestrategyrenamemember(systemstring)'></a>method: RenameMember
<small>id: `M:Dbarone.Net.Mapper.IMemberRenameStrategy.RenameMember(System.String)`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None

---
## Dbarone.Net.Mapper.ITypeConverter
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Interface for classes that can convert values / types. 

>### <a id='dbaronenetmapperitypeconverterconvert(systemobject)'></a>method: Convert
<small>id: `M:Dbarone.Net.Mapper.ITypeConverter.Convert(System.Object)`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None

---
## Dbarone.Net.Mapper.PrefixSuffix
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Denotes either prefix or suffix. 

>### field: Prefix
<small>id: `F:Dbarone.Net.Mapper.PrefixSuffix.Prefix`</small>

#### Summary
 Prefix (start of string). 

>### field: Suffix
<small>id: `F:Dbarone.Net.Mapper.PrefixSuffix.Suffix`</small>

#### Summary
 Suffix (end of string). 


---
## Dbarone.Net.Mapper.PrefixSuffixMemberRenameStrategy
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Removes prefix/suffix characters from member names. 

>### <a id='dbaronenetmapperprefixsuffixmemberrenamestrategy#ctor(dbaronenetmapperprefixsuffix,systemstring)'></a>method: #ctor
<small>id: `M:Dbarone.Net.Mapper.PrefixSuffixMemberRenameStrategy.#ctor(Dbarone.Net.Mapper.PrefixSuffix,System.String)`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmapperprefixsuffixmemberrenamestrategyrenamemember(systemstring)'></a>method: RenameMember
<small>id: `M:Dbarone.Net.Mapper.PrefixSuffixMemberRenameStrategy.RenameMember(System.String)`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None

---
## Dbarone.Net.Mapper.TypeConverter`2
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Converts an object using a generic lambda function or Func. 
|T: |The source type.|
|U: |The destination type.|

>### <a id='dbaronenetmappertypeconverter`2#ctor(systemfunc{`0,`1})'></a>method: #ctor
<small>id: `M:Dbarone.Net.Mapper.TypeConverter`2.#ctor(System.Func{`0,`1})`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='dbaronenetmappertypeconverter`2convert(systemobject)'></a>method: Convert
<small>id: `M:Dbarone.Net.Mapper.TypeConverter`2.Convert(System.Object)`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None

---
## NamingConvention
### Namespace:
``
### Summary:
 Defines the default member naming convention for the type. 

>### field: None
<small>id: `F:NamingConvention.None`</small>

#### Summary
 No defined naming convention used. 

>### field: CamelCaseNamingConvention
<small>id: `F:NamingConvention.CamelCaseNamingConvention`</small>

#### Summary
 Members are named in CamelCase, for example 'memberName'. 

>### field: PascalCaseNamingConvention
<small>id: `F:NamingConvention.PascalCaseNamingConvention`</small>

#### Summary
 Members are named in PascalCase, for example: 'MemberName'. 

>### field: SnakeCasingConvention
<small>id: `F:NamingConvention.SnakeCasingConvention`</small>

#### Summary
 Members are named in SnakeCase, for example 'member_name'. 


---
## MapperException
### Namespace:
``
### Summary:
 Mapper exception class. 

>### <a id='mapperexception#ctor'></a>method: #ctor
<small>id: `M:MapperException.#ctor`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
>### <a id='mapperexception#ctor(systemstring)'></a>method: #ctor
<small>id: `M:MapperException.#ctor(System.String)`</small>
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

#### Exceptions Thrown:
None
#### Examples:
None
