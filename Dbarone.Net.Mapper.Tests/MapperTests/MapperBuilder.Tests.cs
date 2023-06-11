namespace Dbarone.Net.Mapper;

public class MapperBuilderTests
{
    public class CustomerA
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }

    public class CustomerB
    {
        public int CustomerId { get; set; }
        public string NewName { get; set; }
    }

    [Fact]
    public void Test()
    {
        var config = MapperConfiguration.Create()
        .RegisterType<CustomerA>(new MapperOptions
        {
            MemberNameCaseType = Extensions.CaseType.CamelCase
        })
        .RegisterType<CustomerB>(new MapperOptions
        {
            MemberNameCaseType = Extensions.CaseType.CamelCase
        })
        .Ignore<CustomerB>(c => c.CustomerId)
        .Build();
    }
}
