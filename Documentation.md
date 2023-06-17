# Assembly: Dbarone.Net.Mapper
## Contents
- [Dbarone.Net.Mapper.MapperIgnoreAttribute](#dbaronenetmappermapperignoreattribute)
- [Dbarone.Net.Mapper.MapperConfiguration](#dbaronenetmappermapperconfiguration)
- [Dbarone.Net.Mapper.MapperMemberConfiguration](#dbaronenetmappermappermemberconfiguration)
- [Dbarone.Net.Mapper.MapperOptions](#dbaronenetmappermapperoptions)
- [Dbarone.Net.Mapper.MapperTypeConfiguration](#dbaronenetmappermappertypeconfiguration)
- [Dbarone.Net.Mapper.MapperEndPoint](#dbaronenetmappermapperendpoint)
- [Dbarone.Net.Mapper.ClassMemberResolver](#dbaronenetmapperclassmemberresolver)
- [Dbarone.Net.Mapper.CreateInstance](#dbaronenetmappercreateinstance)
- [Dbarone.Net.Mapper.DictionaryMemberResolver](#dbaronenetmapperdictionarymemberresolver)
- [Dbarone.Net.Mapper.Getter](#dbaronenetmappergetter)
- [Dbarone.Net.Mapper.IMemberResolver](#dbaronenetmapperimemberresolver)
- [Dbarone.Net.Mapper.Setter](#dbaronenetmappersetter)
- [Dbarone.Net.Mapper.ObjectMapper](#dbaronenetmapperobjectmapper)
- [Dbarone.Net.Mapper.ITypeConverter](#dbaronenetmapperitypeconverter)
- [Dbarone.Net.Mapper.TypeConverter`2](#dbaronenetmappertypeconverter`2)
- [NamingConvention](#namingconvention)
- [MapperException](#mapperexception)



---
## MapperIgnoreAttribute
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Indicates that property should not be mapped. 


---
## MapperConfiguration
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
TBD
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
> ### method: RegisterConverter``2
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
> ### method: RegisterType``1
<small>id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterType``1(Dbarone.Net.Mapper.MapperOptions)`</small>

#### Summary
 Registers a single type. 

#### Type Parameters:
|Param | Description |
|-----|-----|
|T: ||

#### Parameters:
|Name | Description |
|-----|------|
|options: ||

#### Exceptions Thrown:
None
#### Examples:
None
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
> ### method: Ignore``1
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
> ### method: Rename``1
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
> ### method: MapMember``2
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
> ### method: ApplyMemberAction``1
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
## MapperMemberConfiguration
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
## MapperOptions
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
## MapperTypeConfiguration
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
## MapperEndPoint
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
## ClassMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 General resolver for classes. 


---
## CreateInstance
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Delegate to create a new instance of an object. 
|args: |Optional args to pass into the constructor.|
Returns: Returns an instance of an object.


---
## DictionaryMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Member resolver for dictionaries. 


---
## Getter
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines a basic getter delegate. 
|obj: |The object providing the value|
Returns: A value


---
## IMemberResolver
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Interface for describing methods to 


---
## Setter
### Namespace:
`Dbarone.Net.Mapper`
### Summary:
 Defines a basic setter delegate. 
|target: |The object to set the value in.|
|value: |The value to set.|


---
## ObjectMapper
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
> ### method: MapOne``2
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
> ### method: MapMany``2
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
## ITypeConverter
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
## TypeConverter`2
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
