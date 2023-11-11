using Dbarone.Net.Mapper;

public class CustomerEntity {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public AddressEntity Address { get; set; }
}

public class CustomerDto {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public AddressDto Address { get; set; }
}

public class AddressEntity {
    public int AddressId { get; set; }
    public string AddressLine1 { get; set; }
}

public class AddressDto {
    public int AddressId { get; set; }
    public string AddressLine1 { get; set; }
}

public class MemberwiseMapperProviderTests {

    [Fact]
    public void MemberMapping() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .SetAutoRegisterTypes(true)
        );

        var addressA = new AddressEntity() { AddressId = 123, AddressLine1 = "Test Address" }; 
        var addressB = mapper.Map<AddressEntity, AddressDto>(addressA);
        Assert.Equal("Test Address", addressB.AddressLine1);
    }

    [Fact]
    public void NestedMemberMapping() {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .SetAutoRegisterTypes(true)
        );

        var addressA = new AddressEntity() { AddressId = 123, AddressLine1 = "Test Address" };
        var customerA = new CustomerEntity() { CustomerId = 1, CustomerName = "Test Customer", Address = addressA };
        var customerB = mapper.Map<CustomerEntity, CustomerDto>(customerA);
        Assert.Equal("Test Address", customerB.Address.AddressLine1);
    }

}