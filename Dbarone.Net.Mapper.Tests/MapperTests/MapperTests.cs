namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Mapper;
using Dbarone.Net.Extensions.Object;
using System.Linq.Expressions;

public class MapperTests
{
    [Fact]
    public void MapOne_SameMembers_ShouldMap()
    {
        var mapper = MapperConfiguration.Create().RegisterType<ClassA>().RegisterType<ClassB>().Build();

        var a = new ClassA();
        var b = mapper.MapOne<ClassA, ClassB>(a);
        var a2 = mapper.MapOne<ClassB, ClassA>(b);
        Assert.True(a2.ValueEquals(a));
    }

    [Fact]
    public void MapMany_DifferentMembers_ShouldMap()
    {
        var customers = new List<CustomerEntity>(){
            new CustomerEntity(){CustomerId = 1, Name = "ABC Bicycles", AddressId = 1, Country = "AU", Rating = 'A'},
            new CustomerEntity(){CustomerId = 2, Name = "Star Scooters", AddressId = 2, Country = "AU", Rating = 'B'},
            new CustomerEntity(){CustomerId = 3, Name = "Bikes Galore", AddressId = 3, Country = "UK", Rating = 'A'},
            new CustomerEntity(){CustomerId = 4, Name = "Bikes R Us", AddressId = 4, Country = "UK", Rating = 'A'},
            new CustomerEntity(){CustomerId = 5, Name = "Bicycle Exchange", AddressId = 5, Country = "US", Rating = 'C'}
        };

        var mapper = MapperConfiguration.Create().RegisterType<CustomerEntity>().RegisterType<CustomerModel>().Build();
        var model = mapper.MapMany<CustomerEntity, CustomerModel>(customers.ToList().OrderBy(c => c.CustomerId));
        Assert.Equal("ABC Bicycles,Star Scooters,Bikes Galore,Bikes R Us,Bicycle Exchange", (string.Join(',', model.Select(m => m.Name))));
    }

    [Fact]
    public void MapOne_WithChildObjects_ShouldMap()
    {
        CustomerWithChildAddress obj = new CustomerWithChildAddress()
        {
            CustomerId = 1,
            CustomerName = "A Customer",
            Address = new CustomerChildAddress()
            {
                AddressLine1 = "1 Acacia Avenue",
                AddressLine2 = "Sometown",
                City = "SomeCity"
            }
        };

        var mapper = MapperConfiguration.Create()
            .RegisterType<CustomerWithChildAddress>()
            .RegisterType<CustomerChildAddress>().Build();

        var newObj = mapper.MapOne<CustomerWithChildAddress, CustomerWithChildAddress>(obj);
        Assert.Equal("1 Acacia Avenue", newObj!.Address.AddressLine1);
    }

    [Fact]
    public void MapOne_WithChildObjectsNoChildRegistration_ShouldThrowError()
    {
        CustomerWithChildAddress obj = new CustomerWithChildAddress()
        {
            CustomerId = 1,
            CustomerName = "A Customer",
            Address = new CustomerChildAddress()
            {
                AddressLine1 = "1 Acacia Avenue",
                AddressLine2 = "Sometown",
                City = "SomeCity"
            }
        };

        // Create Register the customer class, but not the address class
        var mapper = MapperConfiguration.Create()
            .RegisterType<CustomerWithChildAddress>()
            .Build();

        // Should throw exception, as we don't have any registration for CustomerChildAddress type.
        Assert.Throws<MapperException>(() => mapper.MapOne<CustomerWithChildAddress, CustomerWithChildAddress>(obj));
    }

    [Fact]
    public void MapOne_DifferentTypesWithoutTypeConverter_ShouldThrowError()
    {
        CustomerWithDifferentTypesA custA = new CustomerWithDifferentTypesA()
        {
            CustomerId = 123,
            CustomerDOB = "01-Jun-2000",
            CustomerRank = "789"
        };
        var mapper = MapperConfiguration.Create().RegisterType<CustomerWithDifferentTypesA>().RegisterType<CustomerWithDifferentTypesB>().Build();
        Assert.Throws<MapperException>(() => mapper.MapOne<CustomerWithDifferentTypesA, CustomerWithDifferentTypesB>(custA));
    }

    [Fact]
    public void TestMappingDifferentTypesWithTypeConverter()
    {
        CustomerWithDifferentTypesA custA = new CustomerWithDifferentTypesA()
        {
            CustomerId = 123,
            CustomerDOB = "2018-08-18",
            CustomerRank = "789"
        };
        var mapper = MapperConfiguration.Create()
        .RegisterType<CustomerWithDifferentTypesA>()
        .RegisterType<CustomerWithDifferentTypesB>()
        .RegisterConverter<string, DateTime>((str) => DateTime.Parse(str))
        .RegisterConverter<string, int>((str) => int.Parse(str))
        .Build();
        var custB = mapper.MapOne<CustomerWithDifferentTypesA, CustomerWithDifferentTypesB>(custA);
        Assert.NotNull(custB);
        Assert.Equal(789, custB!.CustomerRank);
        Assert.Equal(new DateTime(2018, 8, 18), custB!.CustomerDOB);
    }

    [Fact]
    public void Validation_MembersMatch_ShouldPassValidation()
    {
        var mapper = MapperConfiguration.Create()
            .RegisterType<ClassA>(new MapperOptions { AssertMapEndPoint = MapperEndPoint.Source | MapperEndPoint.Destination })
            .RegisterType<ClassB>(new MapperOptions { AssertMapEndPoint = MapperEndPoint.Source | MapperEndPoint.Destination })
            .Build();
        mapper.Validate<ClassA, ClassB>();
    }

    [Fact]
    public void Validation_SourceMembersDontMatch_ShouldThrowException()
    {
        var mapper = MapperConfiguration.Create()
            .RegisterType<CustomerA>(new MapperOptions { AssertMapEndPoint = MapperEndPoint.Source })
            .RegisterType<CustomerB>(new MapperOptions { })
            .Build();

        // This should pass (no assertion rules should apply).
        mapper.Validate<CustomerB, CustomerA>();

        // This should fail (CustomerA has source assertion rule set).
        Assert.Throws<MapperException>(() => mapper.Validate<CustomerA, CustomerB>());
    }

    [Fact]
    public void Validation_DestinationMembersDontMatch_ShouldThrowException()
    {
        var mapper = MapperConfiguration.Create()
            .RegisterType<CustomerA>(new MapperOptions { })
            .RegisterType<CustomerB>(new MapperOptions { AssertMapEndPoint = MapperEndPoint.Destination })
            .Build();

        // This should pass (no assertion rules should apply).
        mapper.Validate<CustomerB, CustomerA>();

        // This should fail (CustomerB has destination assertion rule set).
        Assert.Throws<MapperException>(() => mapper.Validate<CustomerA, CustomerB>());
    }

}