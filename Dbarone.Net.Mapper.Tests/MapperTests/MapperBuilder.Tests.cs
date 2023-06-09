namespace Dbarone.Net.Mapper;

public class MapperBuilderTests
{
    public class CustomerA {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }

    public class CustomerB {
        public int CustomerId { get; set; }
        public string NewName { get; set; }
    }

    [Fact]
    public void Test() {
        var mapper = MapperBuilder<CustomerA, CustomerB>
            .Create()
            .SetSourceMemberNamingConvention(NamingConvention.CamelCaseNamingConvention)
            .Ignore(cust => cust.CustomerId)
            .Rename(cust => cust.CustomerName, "NewName")
            .Configuration;

    }
}
