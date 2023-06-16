# Dbarone.Net.Mapper


>## T:Dbarone.Net.Mapper.MapperIgnoreAttribute

 Indicates that property should not be mapped. 

---


>## T:Dbarone.Net.Mapper.MapperConfiguration



---
### M:Dbarone.Net.Mapper.MapperConfiguration.GetTypeConfigurationCount
 Returns the number of types configured. 
|Name | Description |
|-----|------|

---
### M:Dbarone.Net.Mapper.MapperConfiguration.GetTypeConfiguration(System.Type)
 Gets the type configuration for a specific type. 
|Name | Description |
|-----|------|
|type: |The type to get the configuration for.|

---
### M:Dbarone.Net.Mapper.MapperConfiguration.Create
 Creates a new MapperConfiguration instance. 
|Name | Description |
|-----|------|

---
### M:Dbarone.Net.Mapper.MapperConfiguration.RegisterConverter``2(System.Func{``0,``1})
 Adds a type converter. 
|Name | Description |
|-----|------|
|converter: ||

---
### M:Dbarone.Net.Mapper.MapperConfiguration.RegisterTypes(System.Type[],Dbarone.Net.Mapper.MapperOptions)
 Registers a collection of types. 
|Name | Description |
|-----|------|
|types: ||
|options: ||

---
### M:Dbarone.Net.Mapper.MapperConfiguration.RegisterType``1(Dbarone.Net.Mapper.MapperOptions)
 Registers a single type. 
|Name | Description |
|-----|------|
|options: ||

---
### M:Dbarone.Net.Mapper.MapperConfiguration.RegisterType(System.Type,Dbarone.Net.Mapper.MapperOptions)
 Registers a single type. 
|Name | Description |
|-----|------|
|type: ||
|options: ||

---
### M:Dbarone.Net.Mapper.MapperConfiguration.Ignore``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}})
 Defines a member that will not be mapped. 
|Name | Description |
|-----|------|

---
### M:Dbarone.Net.Mapper.MapperConfiguration.Rename``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.String)
 Defines a custom name for a member when mapping to other types. 
|Name | Description |
|-----|------|

---
### M:Dbarone.Net.Mapper.MapperConfiguration.MapMember``2(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Linq.Expressions.Expression{System.Func{``1,System.Object}})
 Maps a member from source to destination using lambda expressions. 
|Name | Description |
|-----|------|
|fromMember: ||
|toMember: ||

---
### M:Dbarone.Net.Mapper.MapperConfiguration.ApplyMemberAction``1(System.Linq.Expressions.Expression{System.Func{``0,System.Object}},System.Action{Dbarone.Net.Mapper.MapperMemberConfiguration})
 Selects a member, then applies an action to the member mapping rule. 
|Name | Description |
|-----|------|

---
### M:Dbarone.Net.Mapper.MapperConfiguration.Build
 Builds a configured object mapper. 
|Name | Description |
|-----|------|

---


>## T:Dbarone.Net.Mapper.MapperMemberConfiguration

 Defines the rules for a single member mapping. 

---
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


>## T:Dbarone.Net.Mapper.MapperOptions

 Defines a set of mapping options. 

---
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


>## T:Dbarone.Net.Mapper.MapperTypeConfiguration

 Defines the configuration of a single type. 

---
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
### M:Dbarone.Net.Mapper.MapperTypeConfiguration.GetMemberRule(System.Linq.Expressions.Expression)
 Resolves a member/unary expression to a member configuration. 
|Name | Description |
|-----|------|

---
### P:Dbarone.Net.Mapper.MapperTypeConfiguration.CreateInstance
 Create a new instance of the type. 

---


>## T:Dbarone.Net.Mapper.MapperEndPoint

 Defines the mapper end point type. 

---
### F:Dbarone.Net.Mapper.MapperEndPoint.None
 No end point type specified. 

---
### F:Dbarone.Net.Mapper.MapperEndPoint.Source
 Source mapper endpoint. 

---
### F:Dbarone.Net.Mapper.MapperEndPoint.Destination
 Destination mapper endpoint. 

---


>## T:Dbarone.Net.Mapper.ClassMemberResolver

 General resolver for classes. 

---


>## T:Dbarone.Net.Mapper.CreateInstance

 Delegate to create a new instance of an object. 
|args: |Optional args to pass into the constructor.|
Returns: Returns an instance of an object.

---


>## T:Dbarone.Net.Mapper.DictionaryMemberResolver

 Member resolver for dictionaries. 

---


>## T:Dbarone.Net.Mapper.Getter

 Defines a basic getter delegate. 
|obj: |The object providing the value|
Returns: A value

---


>## T:Dbarone.Net.Mapper.IMemberResolver

 Interface for describing methods to 

---


>## T:Dbarone.Net.Mapper.Setter

 Defines a basic setter delegate. 
|target: |The object to set the value in.|
|value: |The value to set.|

---
### M:Dbarone.Net.Mapper.ObjectMapper.MapMany``2(System.Collections.Generic.IEnumerable{``0})
 Maps a collection, list or array of items. 
|Name | Description |
|-----|------|
|obj: |The source object. Must be an enumerable, collection, list or array of type U.|

---


>## T:Dbarone.Net.Mapper.ITypeConverter

 Interface for classes that can convert values / types. 

---
### M:Dbarone.Net.Mapper.ITypeConverter.Convert(System.Object)
 Converts an object. 
|Name | Description |
|-----|------|
|obj: |The object to be converted.|

---


>## T:Dbarone.Net.Mapper.TypeConverter`2

 Converts an object using a generic lambda function or Func. 
|T: |The source type.|
|U: |The destination type.|

---
### M:Dbarone.Net.Mapper.TypeConverter`2.#ctor(System.Func{`0,`1})
 Creates a TypeConverter instance using a m 
|Name | Description |
|-----|------|
|converter: ||

---
### M:Dbarone.Net.Mapper.TypeConverter`2.Convert(System.Object)
 Implementation of interface Convert method. 
|Name | Description |
|-----|------|
|obj: |The object to be converted.|

---


>## T:NamingConvention

 Defines the default member naming convention for the type. 

---
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


>## T:MapperException

 Mapper exception class. 

---
### M:MapperException.#ctor
 Parameterless constructor. 
|Name | Description |
|-----|------|

---
### M:MapperException.#ctor(System.String)
 Create a mapper exception with message. 
|Name | Description |
|-----|------|
|message: ||

---
