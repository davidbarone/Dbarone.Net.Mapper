# Dbarone.Net.Mapper
A .NET object mapper library.

Dbarone.Net.Mapper is an object-mapper library, similar to other object mapper libraries like https://docs.automapper.org/en/stable/Getting-started.html. Object mapping works by transforming an input object of one type into an output object of another type.

Mapping is configured using a `MapperConfiguration` class. To configure the mapper, you must register all the types that will be mapped to each other.
By default, the mapper works by matching member names between types, but it can be configured in cases where the member names do not match up.

Once mapping configuration is completed, you can then map objects using the `ObjectMapper.MapOne` method, or the `ObjectMapper.ManyMany` if you are mapping an array or IEnumerable of objects.

The project contains a test suite covering a variety of use cases.

This project is being used in another of my projects: https://github.com/davidbarone/Dbarone.Net.Document as a way to move data in and out of predefined document structures.

For a full reference of this library, please refer to the [documentation](https://github.com/davidbarone/Dbarone.Net.Mapper/blob/main/Documentation.md).