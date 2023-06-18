# Dbarone.Net.Mapper
A .NET object mapper library.

Dbarone.Net.Mapper is an object-mapper library, similar to other object mapper libraries like https://docs.automapper.org/en/stable/Getting-started.html. Object mapping works by transforming an input object of one type into an output object of another type.

Mapping is configured using a `MapperConfiguration` class. To configure the mapper, you must register all the types that will be mapped to each other.
By default, the mapper works by matching member names between types, but it can be configured in cases where the member names do not match up.

Once mapping configuration is completed, you can then map objects using the `ObjectMapper.MapOne` method, or the `ObjectMapper.ManyMany` if you are mapping an array or IEnumerable of objects.

The project contains a test suite covering a variety of use cases.

This project is being used in another of my projects: https://github.com/davidbarone/Dbarone.Net.Document as a way to move data in and out of predefined document structures.

For a full reference of this library, please refer to the [documentation](https://github.com/davidbarone/Dbarone.Net.Mapper/blob/main/Documentation.md).

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

### Mapper Options
When registering types, a mapper options set of values can be included. This defines how the type should be treated during mapping operations.

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

        var obj2 = mapper.MapOne<CustomerModel, CustomerDto>(obj1)

        // obj2 now contains all values of obj1.

```

