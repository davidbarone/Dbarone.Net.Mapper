namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Mapper;
using Dbarone.Net.Extensions.Object;

public enum EnumValue
{
    A,
    B,
    C
}

public class ClassA
{
    public int IntValue { get; set; } = int.MaxValue;
    public float FloatValue { get; set; } = float.MaxValue;
    public string StringValue { get; set; } = "foobar";
    public EnumValue EnumValue { get; set; } = EnumValue.A;
    public Nullable<short> ShortValue { get; set; } = short.MaxValue;
}

public class ClassB
{
    public int IntValue { get; set; }
    public float FloatValue { get; set; }
    public string StringValue { get; set; } = default!;
    public EnumValue EnumValue { get; set; }
    public Nullable<short> ShortValue { get; set; } = short.MaxValue;
}

public class CustomerEntity
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = default!;
    public int AddressId { get; set; }
    public string Country { get; set; } = default!;

    public char Rating { get; set; }
}

public class CustomerModel
{
    public string Name { get; set; } = default!;
}

public class MapperTests
{
    [Fact]
    public void TestMapOne()
    {
        // Map from ClassA -> ClassB -> ClassA, and check before / after values are identical.
        var mapperAB = ObjectMapper<ClassA, ClassB>.Create();
        var mapperBA = ObjectMapper<ClassB, ClassA>.Create();
        var a = new ClassA();
        var b = mapperAB.MapOne(a);
        var a2 = mapperBA.MapOne(b);
        Assert.True(a2.ValueEquals(a));
    }

    public void TestMapMany()
    {
        var customers = new List<CustomerEntity>(){
            new CustomerEntity(){CustomerId = 1, Name = "ABC Bicycles", AddressId = 1, Country = "AU", Rating = 'A'},
            new CustomerEntity(){CustomerId = 2, Name = "Star Scooters", AddressId = 2, Country = "AU", Rating = 'B'},
            new CustomerEntity(){CustomerId = 3, Name = "Bikes Galore", AddressId = 3, Country = "UK", Rating = 'A'},
            new CustomerEntity(){CustomerId = 4, Name = "Bikes R Us", AddressId = 4, Country = "UK", Rating = 'A'},
            new CustomerEntity(){CustomerId = 5, Name = "Bicycle Exchange", AddressId = 5, Country = "US", Rating = 'C'}
        };

        var mapper = ObjectMapper<CustomerEntity, CustomerModel>.Create();
        var model = mapper.MapMany(customers.ToList().OrderBy(c => c.CustomerId));
        Assert.Equal("ABC Bicycles,Star Scooters,Bikes Galore,Bikes R Us,Bicycle Exchange", (string.Concat(model.Select(m => m.Name))));
    }
}