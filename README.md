# Dbarone.Net.Mapper
A .NET object mapper library.

Dbarone.Net.Mapper is an object-mapper library, similar to other object mapper libraries like https://docs.automapper.org/en/stable/Getting-started.html. Object mapping works by transforming an input object of one type into an output object of another type.

Mapping is configured using a `MapperConfiguration` class. To configure the mapper, you must register all the types that will be mapped to each other.
By default, the mapper works by matching member names between types, but it can be configured in cases where the member names do not match up.

Once mapping configuration is completed, you can then map objects using the `ObjectMapper.MapOne` method, or the `ObjectMapper.ManyMany` if you are mapping an array or IEnumerable of objects.

The project contains a test suite covering a variety of use cases.

This project is being used in another of my projects: https://github.com/davidbarone/Dbarone.Net.Document as a way to move data in and out of predefined document structures.

For a full reference of this library, please refer to the [API documentation](https://html-preview.github.io/?url=https://github.com/davidbarone/Dbarone.Net.Mapper/blob/main/Dbarone.Net.Mapper.html).

## Configuration
The `MapperConfiguration` class is used for configuring the mapping. A single MapperConfiguration class is used to store mapping information for all your Types.

In order to map objects between types, those types must be registered within the mapper configuration.

``` c#

        CustomerModel obj1 = new CustomerModel() {
            // Initialise obj1 with some values...
        };

        var mapper = MapperConfiguration.Create()
            .RegisterType<CustomerModel>()
            .RegisterType<CustomerDto>()
            .Build();

        var obj2 = mapper.MapOne<CustomerModel, CustomerDto>(obj1)

        // obj2 now contains all values of obj1.

```

The above example shows a simple mapping scenario. The mapper function (MapOne) maps a single object into another object / type. It does this by connecting members (properties and fields) with matching names. In the most basic example, if the CustomerModel CustomerDto types above have the same member names, then no additional configuration is required.

Multiple types can be registered in one go:

``` C#

        // Get all the 'dto' types in the current assembly.
        var dtoTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.EndsWith("dto")).ToArray();

        var mapper = MapperConfiguration.Create()
            .RegisterTypes(dtoTypes).Build();

        // do some mapping now...

```
A MappingOptions can also be provided, so that groups of types can be configured with similar settings.

### Mapper Options
When registering types, mapper options can be included. This defines how the type should be treated during mapping operations.

### Including fields and private members
By default, mapper will only map public properties. This behaviour can be overriden within the MapperOptions value:

``` C#

     CustomerModel obj1 = new CustomerModel() {
            // Initialise obj1 with some values...
        };

        var mapper = MapperConfiguration.Create()
            .RegisterType<CustomerModel>(new MapperOptions { IncludeFields = true }) // includes fields
            .RegisterType<CustomerDto>(new MapperOptions { IncludePrivateMembers = true }) // includes private members
            .Build();

        CustomerDto obj2 = mapper.MapOne<CustomerModel, CustomerDto>(obj1)

        // obj2 now contains all values of obj1.

```
### Member renaming strategy
A member renaming strategy can be applied to types that are registered with the mapper configuration. Member renaming strategies are employed to rename members based on a rule. Mapping strategies implement the `IMemberRenameStrategy` interface. By default, no member renaming strategy is used. In this case, actual type member names are compared and matched between source and target types to produce a set mapping rules. However, the following classes can be used to provide a general renaming strategy:

| Class                            | Description                                              |
| -------------------------------- | -------------------------------------------------------- |
| CaseChangeMemberRenameStrategy   | Replaces members of a particular case with another case. |
| PrefixSuffixMemberRenameStrategy | Removes prefix or suffix characters on member names.     |

An example of using a member renaming strategy is shown below:

``` C#
public class EntityWithPascalCaseMembers
{
    public int EntityId { get; set; }
    public string EntityName { get; set; } = default!;
}

public class EntityWithSnakeCaseMembers
{
    public int entity_id { get; set; }
    public string entity_name { get; set; } = default!;
}

public class Main {

    public void DoMapping() {

        var mapper = MapperConfiguration.Create()
            .RegisterType<EntityWithSnakeCaseMembers>(new MapperOptions() {
                MemberRenameStrategy = new CaseChangeMemberRenameStrategy(CaseType.SnakeCase, CaseType.LowerCase)
            })
            .RegisterType<EntityWithPascalCaseMembers>(new MapperOptions() {
                MemberRenameStrategy = new CaseChangeMemberRenameStrategy(CaseType.PascalCase, CaseType.LowerCase)
            })
            .Build();

        EntityWithPascalCaseMembers pascalObj = mapper.MapOne<EntityWithSnakeCaseMembers, EntityWithPascalCaseMembers>(snakeObj);
    }
}
```

#### EndPointValidation

The mapper can be configured by either registering individual types, or maps (source + target type pairs). The mapping rules can be validated in 2 places:
- When the configuration rules are built
- When a mapper is used to map objects

When the configuration rules are built, map rules can be validated. Note that individual types cannot be validated at this point, as they need to participate in a source + target map pair to be validated.

Additionally, before any map operations are carried out, a validation takes place.

Validation can be performed on the source, target, or both end points. This is configured using the `EndPointValidation` mapper option. The default setting is `MapperEndPoint.Target`.

### Calculations

Sometimes, a map between two types is not straight forward, and a transformation needs to take place. A calculation can be added to the configuration to perform transformations. The following example demonstrates:

``` C#
    // Assume we have 2 classes:
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public DateTime DoB { get; set; }
    }

    public class PersonWithFullName
    {
        public int PersonId { get; set; }
        public string FullName { get; set; } = default!;
        public DateTime DoB { get; set; }
    }

    // And we want to map FirstName + Surname => FullName
    Person person = new Person()
    {
        PersonId = 1,
        FirstName = "John",
        Surname = "Doe",
        DoB = new DateTime(1960, 8, 26)
    };

    var mapper = MapperConfiguration.Create()
        .RegisterType<Person>()
        .RegisterType<PersonWithFullName>()
        .RegisterCalculation<Person, string>("FullName", (p) => p.FirstName + " " + p.Surname)
        .Build();

    var person2 = mapper.MapOne<Person, PersonWithFullName>(person);
```

### Ignoring Members
Sometimes, you will want to ignore members from the mapping process. The `Ignore` method can be used:

``` C#

    // Ignore example here...

```

### MemberFilterRule
A MemberFilterRule allows you to set which members should be ignored based on a function. MemberFilterRules can
be set in two places:
- As part of the MemberOptions, when registering one or more types
- Using the SetMemberFilterRule method on the MapperConfiguration class, to set a member filter rule for a single type.

## Binding Algorithm
The mapper algorithm works by joining 'members' from source type to target type. The way members are defined depends on the type being mapped. The mapper library supports 2 broad kinds of types:
- Types with fixed schemas
- Types with flexible schemas

### Types with Fixed Schemas
This includes all classes and structs that are strong-typed, and have known properties and fields at compile time. Members correlate to the type's properties and fields.

### Types with Variable Schemas
This includes dictionaries and dynamic objects (which are implemented internally as dictionaries). These objects must be inspected at map-time to determine the members. The members correlate to the index key values.

### Mapping Process
The MapOne() method works as follows:
- A SourceType and TargetType are provided to the MapOne method.
- A source object is also provided to the MapOne method. This must be an object that can be assigned to SourceType, but can be a subtype. Note that if a sub type instance is provided, the mapper will still only map members based on SourceType.
- A check is made that the SourceType has been registered. The source type can be any registered type, including interface types.
- A check is made that the TargetType has been registered. The target type must be a concrete (non abstract) class, cannot be an interface, and the type must have a parameterless constructor.
- If SourceType and TargetType are registered, then a full memberwise map will be carried out (see below).
- If the SourceType or TargetType have not been registered, a second check is made in the TypeConverters registry. If a source and target TypeConverter match is found, then a simple type conversion mapping is performed (see below)

### TypeConversion Mapping Process
In a typeconversion mapping process, a converter method is called, passing in the source object. The converter method is then resposible for returning an object of type TargetType. No memberwise mapping takes place, and no recursive mapping of child objects takes place. This type of mapping should only be reserved in cases where you want to apply very simple mapping or type conversions on simple (e.g. native) types.

### Memberwise Mapping Process
The memberwise mapping process is the full mapping process, and involves recusively mapping each connected member between source and target types.

### Validation Process
Validation takes place in multiple places:
- Mapper configuration 'build' phase
- Before mapping of the first object in the map phase

#### Validation in the Mapper Configuration 'Build' Phase
Individual types are validated as follows:
- Check that the internal member names are unique within each type.

#### Before mapping of the first object
- The type validation is repeated
- If the Options.EndPointValidation is set to `MapperEndPoint.Target` (which is the default setting), then the system checks that every target member that is included in the mapping process has a matching member in the source object type.
- If the Options.EndPointValidation is set to `MapperEndPoint.Source`, then the system checks that every source member that is included in the mapping process has a matching member in the target object type.
- The source and target declared data types must also match for the mapping to occur (or there needs to be a type converter or a )
- Once validation takes place, the members for mapping are selected as follows:
  - If the TargetType is a fixed schema, the member list comes from the target type
  - Otherwise, the member list is obtained from the source type.
  - Any members that do not exist on both sides are unmapped. Note that if EndPointValidation is set, orphaned members will cause exceptions to be thrown.

#### Mapping
- When mapping a member, the data types on the source and target types must match.
- If the member types on source and target don't match, but a TypeConverter exists for the source / target type, a simple type conversion will be carried out (note this will not include any recursive mapping of child members though).
- Otherwise, a type exception will be thrown.
- When assigning the source value to the target object, if the type has been registered, then the child value will be mapped recursively.

### Member Selection Process
- By default, the members defined as the public properties.
- When registering a type, options can be provided to allow
  - Fields to be included as members
  - Private properties and fields to be included as members
- For flexible schema types, the members align to the dictionary keys
- Member names default to the property/field names or dictionary keys, but these names can be overridden using an `IMemberRenameStrategy`.
- The 'renamed' member is known as the 'InternalMemberName'. This is the actual member name used to connect / map to other types.
- Members can be excluded from the mapping process by calling the `Ignore` method.
- 



- If the SourceType or TargetType have not been registered, a second check is made in the TypeConverters registry. If a source + target TypeConverter match is found, then
-  both SourceType and TargetType have been registered in the configuration.
- If the SourceType is not registered, then the system checks for a registration in the Type Converters. If a registration is found, then objects will be 'converted' to the target type.
The following types can be mapped:
- Value Types
  - Built-in value types (e.g. int, float)
  - Structs
- Reference Types
  - Classes

To do:
- internal member names case sensitive?
- type converters can be global / assigned to type / member
- type.filtermembers()
- mapmany - all array types / list / ienumerable etc
- open generics?
- 