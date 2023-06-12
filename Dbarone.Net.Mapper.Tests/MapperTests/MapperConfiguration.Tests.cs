namespace Dbarone.Net.Mapper;

public class MapperConfigurationTests {

    [Fact]
    public void TestConfigurationSimpleCustomer() {
        var config = MapperConfiguration.Create().RegisterType<SimpleCustomer>();
        Assert.Equal(1, config.GetTypeConfigurationCount());

        // should be 2 member rules
        MapperTypeConfiguration typeConfig = config.GetTypeConfiguration(typeof(SimpleCustomer));
        Assert.Equal(2, typeConfig.MemberConfiguration.Count());
    }

    [Theory]
    [InlineData(false, false, 2)]
    [InlineData(false, true, 3)]
    [InlineData(true, false, 3)]
    [InlineData(true, true, 5)]
    public void TestConfigurationSimpleCustomerWithPrivatePropertiesAndFields(bool includePrivateMembers, bool includeFields, int expectedMemberCount) {
        var config = MapperConfiguration.Create().RegisterType<SimpleCustomerWithPrivatePropertiesAndFields>(new MapperOptions{
            IncludeFields = includeFields,
            IncludePrivateMembers = includePrivateMembers
        });
        Assert.Equal(1, config.GetTypeConfigurationCount());

        MapperTypeConfiguration typeConfig = config.GetTypeConfiguration(typeof(SimpleCustomerWithPrivatePropertiesAndFields));
        Assert.Equal(expectedMemberCount, typeConfig.MemberConfiguration.Count());
    }


}