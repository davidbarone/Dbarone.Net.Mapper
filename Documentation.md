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
## Dbarone.Net.Mapper.MapperIgnoreAttribute
Namespace: `Dbarone.Net.Mapper`

 Indicates that property should not be mapped. 


---
## Dbarone.Net.Mapper.MapperConfiguration
Namespace: `Dbarone.Net.Mapper`



> ### method: MapperConfiguration.GetTypeConfigurationCount
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
> ### method: MapperConfiguration.GetTypeConfiguration
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
> ### method: MapperConfiguration.Create
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
> ### method: MapperConfiguration.RegisterConverter``2
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
> ### method: MapperConfiguration.RegisterTypes
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
> ### method: MapperConfiguration.RegisterType``1
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
> ### method: MapperConfiguration.RegisterType
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
> ### method: MapperConfiguration.Ignore``1
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
> ### method: MapperConfiguration.Rename``1
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
> ### method: MapperConfiguration.MapMember``2
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
> ### method: MapperConfiguration.ApplyMemberAction``1
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
> ### method: MapperConfiguration.Build
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
Namespace: `Dbarone.Net.Mapper`

 Defines the rules for a single member mapping. 

### P:Dbarone.Net.Mapper.MapperMemberConfiguration.MemberName
 Member name for the mapping rule. 

---
### P:Dbarone.Net.Mapper.MapperMemberConfiguration.DataType
 Member data type. 

---
### P:Dbarone.Net.Mapper.MapperMemberConfiguration.InternalMemberName
 The internal member name. Mapping from source to destination is done via matching internal names. 

---
### P:Dbarone.Net.Mapper.MapperMemberConfiguration.Ignore
 Set to true to ignore this member in the mapping configuration. 

---
### P:Dbarone.Net.Mapper.MapperMemberConfiguration.Getter
 Delegate method to get the value from the instance. 

---
### P:Dbarone.Net.Mapper.MapperMemberConfiguration.Setter
 Delegate method to set the value to the instance. 

---

---
## Dbarone.Net.Mapper.MapperOptions
Namespace: `Dbarone.Net.Mapper`

 Defines a set of mapping options. 

### P:Dbarone.Net.Mapper.MapperOptions.IncludeFields
 Set to true to include mapping of fields as well as properties. Default is false. 

---
### P:Dbarone.Net.Mapper.MapperOptions.IncludePrivateMembers
 Set to true to include private fields and properties. Default is false.1 

---
### P:Dbarone.Net.Mapper.MapperOptions.MemberNameCaseType
 The default casing convention for members of the type. Default is CaseType.None. 

---
### P:Dbarone.Net.Mapper.MapperOptions.MemberNameTranslation
 Optional member name translation function 

---
### P:Dbarone.Net.Mapper.MapperOptions.AssertMapEndPoint
 Defines implicit assertion of mapping rules prior to any map function call. Defaults to 'None'. 

---

---
## Dbarone.Net.Mapper.MapperTypeConfiguration
Namespace: `Dbarone.Net.Mapper`

 Defines the configuration of a single type. 

### P:Dbarone.Net.Mapper.MapperTypeConfiguration.Type
 The type the configuration relates to. 

---
### P:Dbarone.Net.Mapper.MapperTypeConfiguration.Options
 Defines the options for the map registration 

---
### P:Dbarone.Net.Mapper.MapperTypeConfiguration.MemberConfiguration
 Defines the member configuration. 

---
### P:Dbarone.Net.Mapper.MapperTypeConfiguration.MemberResolver
 Provides the member resolving strategy for this type. 

---
> ### method: MapperTypeConfiguration.GetMemberRule
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
### P:Dbarone.Net.Mapper.MapperTypeConfiguration.CreateInstance
 Create a new instance of the type. 

---

---
## Dbarone.Net.Mapper.MapperEndPoint
Namespace: `Dbarone.Net.Mapper`

 Defines the mapper end point type. 

### F:Dbarone.Net.Mapper.MapperEndPoint.None
 No end point type specified. 

---
### F:Dbarone.Net.Mapper.MapperEndPoint.Source
 Source mapper endpoint. 

---
### F:Dbarone.Net.Mapper.MapperEndPoint.Destination
 Destination mapper endpoint. 

---

---
## Dbarone.Net.Mapper.ClassMemberResolver
Namespace: `Dbarone.Net.Mapper`

 General resolver for classes. 


---
## Dbarone.Net.Mapper.CreateInstance
Namespace: `Dbarone.Net.Mapper`

 Delegate to create a new instance of an object. 
|args: |Optional args to pass into the constructor.|
Returns: Returns an instance of an object.


---
## Dbarone.Net.Mapper.DictionaryMemberResolver
Namespace: `Dbarone.Net.Mapper`

 Member resolver for dictionaries. 


---
## Dbarone.Net.Mapper.Getter
Namespace: `Dbarone.Net.Mapper`

 Defines a basic getter delegate. 
|obj: |The object providing the value|
Returns: A value


---
## Dbarone.Net.Mapper.IMemberResolver
Namespace: `Dbarone.Net.Mapper`

 Interface for describing methods to 


---
## Dbarone.Net.Mapper.Setter
Namespace: `Dbarone.Net.Mapper`

 Defines a basic setter delegate. 
|target: |The object to set the value in.|
|value: |The value to set.|


---
## Dbarone.Net.Mapper.ObjectMapper
Namespace: `Dbarone.Net.Mapper`

 The ObjectMapper class provides mapping functions to transform objects from one type to another. 

> ### method: ObjectMapper.MapOne
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
> ### method: ObjectMapper.MapOne``2
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
> ### method: ObjectMapper.MapMany``2
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
Namespace: `Dbarone.Net.Mapper`

 Interface for classes that can convert values / types. 

> ### method: ITypeConverter.Convert
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
Namespace: `Dbarone.Net.Mapper`

 Converts an object using a generic lambda function or Func. 
|T: |The source type.|
|U: |The destination type.|

> ### method: TypeConverter`2.#ctor
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
> ### method: TypeConverter`2.Convert
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
Namespace: ``

 Defines the default member naming convention for the type. 

### F:NamingConvention.None
 No defined naming convention used. 

---
### F:NamingConvention.CamelCaseNamingConvention
 Members are named in CamelCase, for example 'memberName'. 

---
### F:NamingConvention.PascalCaseNamingConvention
 Members are named in PascalCase, for example: 'MemberName'. 

---
### F:NamingConvention.SnakeCasingConvention
 Members are named in SnakeCase, for example 'member_name'. 

---

---
## MapperException
Namespace: ``

 Mapper exception class. 

> ### method: MapperException.#ctor
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
> ### method: MapperException.#ctor
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
