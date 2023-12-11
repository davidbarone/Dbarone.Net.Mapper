
using Dbarone.Net.Mapper;

public class Customer {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
}

public class DictionaryTests {

    [Fact]
    public void TestClassToDictionary() {
        var customer = new Customer()
        {
            CustomerId = 123,
            CustomerName = "Acme Ltd"
        };

        ObjectMapper mapper = new ObjectMapper(new MapperConfiguration().SetAutoRegisterTypes(true));

        var dict = mapper.Map<Customer, Dictionary<string, object>>(customer);
        Assert.Equal(123, dict["CustomerId"]);
    }
}