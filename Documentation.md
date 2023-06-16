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



### method: MapperConfiguration.GetTypeConfigurationCount
id: `M:Dbarone.Net.Mapper.MapperConfiguration.GetTypeConfigurationCount`

 Returns the number of types configured. 







### method: MapperConfiguration.GetTypeConfiguration
id: `M:Dbarone.Net.Mapper.MapperConfiguration.GetTypeConfiguration(System.Type)`

 Gets the type configuration for a specific type. 



|Name | Description |
|-----|------|
|type: |The type to get the configuration for.|



### method: MapperConfiguration.Create
id: `M:Dbarone.Net.Mapper.MapperConfiguration.Create`

 Creates a new MapperConfiguration instance. 







### method: MapperConfiguration.RegisterConverter``2
id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterConverter``2(System.Func{``0,``1})`

 Adds a type converter. 

|Param | Description |
|-----|-----|
|T: ||
|U: ||

|Name | Description |
|-----|------|
|converter: ||



### method: MapperConfiguration.RegisterTypes
id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterTypes(System.Type[],Dbarone.Net.Mapper.MapperOptions)`

 Registers a collection of types. 



|Name | Description |
|-----|------|
|types: ||
|options: ||



### method: MapperConfiguration.RegisterType``1
id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterType``1(Dbarone.Net.Mapper.MapperOptions)`

 Registers a single type. 

|Param | Description |
|-----|-----|
|T: ||

|Name | Description |
|-----|------|
|options: ||



### method: MapperConfiguration.RegisterType
id: `M:Dbarone.Net.Mapper.MapperConfiguration.RegisterType(System.Type,Dbarone.Net.Mapper.MapperOptions)`

 Registers a single type. 



|Name | Description |
|-----|------|
|type: ||
|options: ||



### method: MapperConfiguration.Ignore``1
id: `M:Dbarone.Net.Mapper.MapperConfiguration.Ignore``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}})`

 Defines a member that will not be mapped. 







### method: MapperConfiguration.Rename``1
id: `M:Dbarone.Net.Mapper.MapperConfiguration.Rename``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.String)`

 Defines a custom name for a member when mapping to other types. 







### method: MapperConfiguration.MapMember``2
id: `M:Dbarone.Net.Mapper.MapperConfiguration.MapMember``2(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Linq.Expressions.Expression{System.Func{``1,System.Object}})`

 Maps a member from source to destination using lambda expressions. 

|Param | Description |
|-----|-----|
|T: ||
|U: ||

|Name | Description |
|-----|------|
|fromMember: ||
|toMember: ||



### method: MapperConfiguration.ApplyMemberAction``1
id: `M:Dbarone.Net.Mapper.MapperConfiguration.ApplyMemberAction``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Action{Dbarone.Net.Mapper.MapperMemberConfiguration})`

 Selects a member, then applies an action to the member mapping rule. 







### method: MapperConfiguration.Build
id: `M:Dbarone.Net.Mapper.MapperConfiguration.Build`

 Builds a configured object mapper. 








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
### method: MapperTypeConfiguration.GetMemberRule
id: `M:Dbarone.Net.Mapper.MapperTypeConfiguration.GetMemberRule(System.Linq.Expressions.Expression)`

 Resolves a member/unary expression to a member configuration. 







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

### method: ObjectMapper.MapMany``2
id: `M:Dbarone.Net.Mapper.ObjectMapper.MapMany``2(System.Collections.Generic.IEnumerable{``0})`

 Maps a collection, list or array of items. 

|Param | Description |
|-----|-----|
|T: |The type of the source object.|
|U: |The type of the destination object.|

|Name | Description |
|-----|------|
|obj: |The source object. Must be an enumerable, collection, list or array of type U.|




---
## Dbarone.Net.Mapper.ITypeConverter
Namespace: `Dbarone.Net.Mapper`

 Interface for classes that can convert values / types. 

### method: ITypeConverter.Convert
id: `M:Dbarone.Net.Mapper.ITypeConverter.Convert(System.Object)`

 Converts an object. 



|Name | Description |
|-----|------|
|obj: |The object to be converted.|




---
## Dbarone.Net.Mapper.TypeConverter`2
Namespace: `Dbarone.Net.Mapper`

 Converts an object using a generic lambda function or Func. 
|T: |The source type.|
|U: |The destination type.|

### method: TypeConverter`2.#ctor
id: `M:Dbarone.Net.Mapper.TypeConverter`2.#ctor(System.Func{`0,`1})`

 Creates a TypeConverter instance using a m 



|Name | Description |
|-----|------|
|converter: ||



### method: TypeConverter`2.Convert
id: `M:Dbarone.Net.Mapper.TypeConverter`2.Convert(System.Object)`

 Implementation of interface Convert method. 



|Name | Description |
|-----|------|
|obj: |The object to be converted.|




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

### method: MapperException.#ctor
id: `M:MapperException.#ctor`

 Parameterless constructor. 







### method: MapperException.#ctor
id: `M:MapperException.#ctor(System.String)`

 Create a mapper exception with message. 



|Name | Description |
|-----|------|
|message: ||



