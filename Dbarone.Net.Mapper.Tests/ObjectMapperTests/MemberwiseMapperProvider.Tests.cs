using Dbarone.Net.Mapper;

public class CustomerEntity {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
}

public class CustomerDto {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
}

public class MemberwiseMapperProviderTests {

    [Fact]
    public void MapCustomer() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .SetAutoRegisterTypes(true)
        );

        var customerA = new CustomerEntity() { CustomerId = 123, CustomerName = "Test Customer" }; 
        var customerB = mapper.Map<CustomerEntity, CustomerDto>(customerA);
        Assert.Equal("Test Customer", customerB.CustomerName);
    }
}