# Assembly: Dbarone.Net.Mapper
## Contents
- [MapperIgnoreAttribute](#dbaronenetmappermapperignoreattribute)
- [MapperConfiguration](#dbaronenetmappermapperconfiguration)
  - [GetTypeConfigurationCount](#dbaronenetmappermapperconfigurationgettypeconfigurationcount)
  - [GetTypeConfiguration](#dbaronenetmappermapperconfigurationgettypeconfiguration(systemtype))
  - [Create](#dbaronenetmappermapperconfigurationcreate)
  - [RegisterConverter<T, U>](#dbaronenetmappermapperconfigurationregisterconverter``2(systemfunc{``0,``1}))
  - [RegisterTypes](#dbaronenetmappermapperconfigurationregistertypes(systemtype[],dbaronenetmappermapperoptions))
  - [RegisterType<T>](#dbaronenetmappermapperconfigurationregistertype``1(dbaronenetmappermapperoptions))
  - [RegisterType](#dbaronenetmappermapperconfigurationregistertype(systemtype,dbaronenetmappermapperoptions))
  - [Ignore<T>](#dbaronenetmappermapperconfigurationignore``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}}))
  - [Rename<T>](#dbaronenetmappermapperconfigurationrename``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemstring))
  - [MapMember<T, U>](#dbaronenetmappermapperconfigurationmapmember``2(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemlinqexpressionsexpression{systemfunc{``1,systemobject}}))
  - [ApplyMemberAction<T>](#dbaronenetmappermapperconfigurationapplymemberaction``1(systemlinqexpressionsexpression{systemfunc{``0,systemobject}},systemaction{dbaronenetmappermappermemberconfiguration}))
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
  - [MemberNameTranslation](#dbaronenetmappermapperoptionsmembernametranslation)
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
- [ITypeConverter](#dbaronenetmapperitypeconverter)
  - [Convert](#dbaronenetmapperitypeconverterconvert(systemobject))
- [TypeConverter<A, B>](#dbaronenetmappertypeconverter`2)
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
 Indicates that property should not be mapped. 


---
## Dbarone.Net.Mapper.MapperConfiguration
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Creates configuration for a [ObjectMapper](#dbaronenetmapperobjectmapper) mapper object. 

> ### method: GetTypeConfigurationCount
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.GetTypeConfigurationCount`</small>

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
> ### method: GetTypeConfiguration
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.GetTypeConfiguration(System.Type)`</small>

#### Summary
 Gets the type configuration for a specific type. 

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
> ### method: Create
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Create`</small>

#### Summary
 Creates a new MapperConfiguration instance. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions Thrown:
None
#### Examples:
None
> ### method: RegisterConverter<T, U>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterConverter``2(System.Func{``0,``1})`</small>

#### Summary
 Adds a type converter. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: ||
|U: ||

#### Parameters:
|Name | Description |
|-----|------|
|converter: ||

#### Exceptions Thrown:
None
#### Examples:
None
> ### method: RegisterTypes
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterTypes(System.Type[],Dbarone.Net.Mapper.MapperOptions)`</small>

#### Summary
 Registers a collection of types. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|types: ||
|options: ||

#### Exceptions Thrown:
None
#### Examples:
None
> ### method: RegisterType<T>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterType``1(Dbarone.Net.Mapper.MapperOptions)`</small>

#### Summary
 Registers a type using generic types. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of the entity to register.|

#### Parameters:
|Name | Description |
|-----|------|
|options: |optional configuration for the mapping.|

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


> ### method: RegisterType
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterType(System.Type,Dbarone.Net.Mapper.MapperOptions)`</small>

#### Summary
 Registers a single type. 

#### Type Parameters:
None

#### Parameters:
|Name | Description |
|-----|------|
|type: ||
|options: ||

#### Exceptions Thrown:
None
#### Examples:
None
> ### method: Ignore<T>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Ignore``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}})`</small>

#### Summary
 Defines a member that will not be mapped. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions Thrown:
None
#### Examples:
None
> ### method: Rename<T>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Rename``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.String)`</small>

#### Summary
 Defines a custom name for a member when mapping to other types. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions Thrown:
None
#### Examples:
None
> ### method: MapMember<T, U>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.MapMember``2(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Linq.Expressions.Expression{System.Func{``1,System.Object}})`</small>

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
> ### method: ApplyMemberAction<T>
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.ApplyMemberAction``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Action{Dbarone.Net.Mapper.MapperMemberConfiguration})`</small>

#### Summary
 Selects a member, then applies an action to the member mapping rule. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions Thrown:
None
#### Examples:
None
> ### method: Build
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.Build`</small>

#### Summary
 Builds a configured object mapper. 

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

> ### property: MemberName
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.MemberName`</small>

#### Summary
 Member name for the mapping rule. 

> ### property: DataType
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.DataType`</small>

#### Summary
 Member data type. 

> ### property: InternalMemberName
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.InternalMemberName`</small>

#### Summary
 The internal member name. Mapping from source to destination is done via matching internal names. 

> ### property: Ignore
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.Ignore`</small>

#### Summary
 Set to true to ignore this member in the mapping configuration. 

> ### property: Getter
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.Getter`</small>

#### Summary
 Delegate method to get the value from the instance. 

> ### property: Setter
<small>id: `P:Dbarone.Net.Mapper.MapperMemberConfiguration.Setter`</small>

#### Summary
 Delegate method to set the value to the instance. 


---
## Dbarone.Net.Mapper.MapperOptions
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines a set of mapping options. 

> ### property: IncludeFields
<small>id: `P:Dbarone.Net.Mapper.MapperOptions.IncludeFields`</small>

#### Summary
 Set to true to include mapping of fields as well as properties. Default is false. 

> ### property: IncludePrivateMembers
<small>id: `P:Dbarone.Net.Mapper.MapperOptions.IncludePrivateMembers`</small>

#### Summary
 Set to true to include private fields and properties. Default is false.1 

> ### property: MemberNameCaseType
<small>id: `P:Dbarone.Net.Mapper.MapperOptions.MemberNameCaseType`</small>

#### Summary
 The default casing convention for members of the type. Default is CaseType.None. 

> ### property: MemberNameTranslation
<small>id: `P:Dbarone.Net.Mapper.MapperOptions.MemberNameTranslation`</small>

#### Summary
 Optional member name translation function 

> ### property: AssertMapEndPoint
<small>id: `P:Dbarone.Net.Mapper.MapperOptions.AssertMapEndPoint`</small>

#### Summary
 Defines implicit assertion of mapping rules prior to any map function call. Defaults to 'None'. 


---
## Dbarone.Net.Mapper.MapperTypeConfiguration
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines the configuration of a single type. 

> ### property: Type
<small>id: `P:Dbarone.Net.Mapper.MapperTypeConfiguration.Type`</small>

#### Summary
 The type the configuration relates to. 

> ### property: Options
<small>id: `P:Dbarone.Net.Mapper.MapperTypeConfiguration.Options`</small>

#### Summary
 Defines the options for the map registration 

> ### property: MemberConfiguration
<small>id: `P:Dbarone.Net.Mapper.MapperTypeConfiguration.MemberConfiguration`</small>

#### Summary
 Defines the member configuration. 

> ### property: MemberResolver
<small>id: `P:Dbarone.Net.Mapper.MapperTypeConfiguration.MemberResolver`</small>

#### Summary
 Provides the member resolving strategy for this type. 

> ### method: GetMemberRule
<small>id: `M:Dbarone.Net.Mapper.MapperTypeConfiguration.GetMemberRule(System.Linq.Expressions.Expression)`</small>

#### Summary
 Resolves a member/unary expression to a member configuration. 

#### Type Parameters:
None

#### Parameters:
None

#### Exceptions Thrown:
None
#### Examples:
None
> ### property: CreateInstance
<small>id: `P:Dbarone.Net.Mapper.MapperTypeConfiguration.CreateInstance`</small>

#### Summary
 Create a new instance of the type. 


---
## Dbarone.Net.Mapper.MapperEndPoint
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines the mapper end point type. 

> ### field: None
<small>id: `F:Dbarone.Net.Mapper.MapperEndPoint.None`</small>

#### Summary
 No end point type specified. 

> ### field: Source
<small>id: `F:Dbarone.Net.Mapper.MapperEndPoint.Source`</small>

#### Summary
 Source mapper endpoint. 

> ### field: Destination
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

> ### method: MapOne
<small>id: `M:Dbarone.Net.Mapper.ObjectMapper.MapOne(System.Type,System.Type,System.Object)`</small>

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
> ### method: MapOne<T, U>
<small>id: `M:Dbarone.Net.Mapper.ObjectMapper.MapOne``2(``0)`</small>

#### Summary
 Maps / transforms an object from one type to another. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: ||
|U: ||

#### Parameters:
|Name | Description |
|-----|------|
|obj: ||

#### Exceptions Thrown:
None
#### Examples:
None
> ### method: MapMany<T, U>
<small>id: `M:Dbarone.Net.Mapper.ObjectMapper.MapMany``2(System.Collections.Generic.IEnumerable{``0})`</small>

#### Summary
 Maps a collection, list or array of items. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: |The type of the source object.|
|U: |The type of the destination object.|

#### Parameters:
|Name | Description |
|-----|------|
|obj: |The source object. Must be an enumerable, collection, list or array of type U.|

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

> ### method: Convert
<small>id: `M:Dbarone.Net.Mapper.ITypeConverter.Convert(System.Object)`</small>

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
## Dbarone.Net.Mapper.TypeConverter`2
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Converts an object using a generic lambda function or Func. 
|T: |The source type.|
|U: |The destination type.|

> ### method: #ctor
<small>id: `M:Dbarone.Net.Mapper.TypeConverter`2.#ctor(System.Func{`0,`1})`</small>

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
> ### method: Convert
<small>id: `M:Dbarone.Net.Mapper.TypeConverter`2.Convert(System.Object)`</small>

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

> ### field: None
<small>id: `F:NamingConvention.None`</small>

#### Summary
 No defined naming convention used. 

> ### field: CamelCaseNamingConvention
<small>id: `F:NamingConvention.CamelCaseNamingConvention`</small>

#### Summary
 Members are named in CamelCase, for example 'memberName'. 

> ### field: PascalCaseNamingConvention
<small>id: `F:NamingConvention.PascalCaseNamingConvention`</small>

#### Summary
 Members are named in PascalCase, for example: 'MemberName'. 

> ### field: SnakeCasingConvention
<small>id: `F:NamingConvention.SnakeCasingConvention`</small>

#### Summary
 Members are named in SnakeCase, for example 'member_name'. 


---
## MapperException
### Namespace:
``
### Summary:
 Mapper exception class. 

> ### method: #ctor
<small>id: `M:MapperException.#ctor`</small>

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
> ### method: #ctor
<small>id: `M:MapperException.#ctor(System.String)`</small>

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
