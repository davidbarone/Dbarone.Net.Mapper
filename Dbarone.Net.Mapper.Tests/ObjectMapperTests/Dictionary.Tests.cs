
using Dbarone.Net.Mapper;

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
}

public class DictionaryTests
{

    [Fact]
    public void TestClassToDictionary()
    {
        var customer = new Customer()
        {
            CustomerId = 123,
            CustomerName = "Acme Ltd"
        };

        ObjectMapper mapper = new ObjectMapper(new MapperConfiguration().SetAutoRegisterTypes(true));

        var dict = mapper.Map<Customer, Dictionary<string, object>>(customer);
        Assert.Equal(123, dict["CustomerId"]);
    }

    [Fact]
    public void TestDictionaryToClass()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>
        {
            {"CustomerId", 123 },
            {"CustomerName", "Acme Ltd" }
        };

        ObjectMapper mapper = new ObjectMapper(new MapperConfiguration().SetAutoRegisterTypes(true));
        var customer = mapper.Map<Dictionary<string, object>, Customer>(dict);
        Assert.Equal(123, customer.CustomerId);
    }
}