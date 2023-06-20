namespace Dbarone.Net.Mapper.Tests;
using System.Reflection;
using Dbarone.Net.Extensions;

public class MapperConfigurationTests
{
    [Fact]
    public void MapperBuilder_RegisterTypes_ShouldBuild()
    {
        var config = MapperConfiguration.Create()
        .RegisterType<CustomerA>()
        .RegisterType<CustomerB>()
        .Ignore<CustomerB>(c => c.CustomerId)
        .Build();
    }

    public void MapperConfiguration_AddMultipleTypes_ShouldBuild()
    {
        var dtoTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.EndsWith("dto")).ToArray();

        var mapper = MapperConfiguration.Create()
            .RegisterTypes(dtoTypes).Build();

        // do some mapping now...
    }

    [Fact]
    public void MapperBuilder_RegisterSingle_Returns2MemberRules()
    {
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
    [InlineData(true, true, 8)]
    public void MapperBuilder_IncludePrivateFieldsProperties_ShouldReturnCorrectMemberRules(bool includePrivateMembers, bool includeFields, int expectedMemberCount)
    {
        var config = MapperConfiguration.Create().RegisterType<SimpleCustomerWithPrivatePropertiesAndFields>(new MapperOptions
        {
            IncludeFields = includeFields,
            IncludePrivateMembers = includePrivateMembers
        });
        Assert.Equal(1, config.GetTypeConfigurationCount());

        MapperTypeConfiguration typeConfig = config.GetTypeConfiguration(typeof(SimpleCustomerWithPrivatePropertiesAndFields));
        Assert.Equal(expectedMemberCount, typeConfig.MemberConfiguration.Count());
    }

    [Fact]
    public void MapperBuilder_DuplicateInternalNames_ShouldThrowException()
    {
        var config = MapperConfiguration.Create()
            .RegisterType<CustomerEntity>()
            .Rename<CustomerEntity>(c => c.CustomerId, "Name");  // 2 Name members now exist.

        Assert.Throws<MapperException>(() => config.Build());
    }

    [Fact]
    public void MapperConfiguration_UseCaseChangeMemberRenameStrategy_ShouldMatchMembers()
    {
        var snakeObj = new EntityWithSnakeCaseMembers()
        {
            entity_id = 123,
            entity_name = "this is an entity name"
        };

        var mapper = MapperConfiguration.Create()
            .RegisterType<EntityWithSnakeCaseMembers>(new MapperOptions()
            {
                MemberRenameStrategy = new CaseChangeMemberRenameStrategy(CaseType.SnakeCase, CaseType.LowerCase)
            })
            .RegisterType<EntityWithPascalCaseMembers>(new MapperOptions()
            {
                MemberRenameStrategy = new CaseChangeMemberRenameStrategy(CaseType.PascalCase, CaseType.LowerCase)
            })
            .Build();

        var pascalObj = mapper.MapOne<EntityWithSnakeCaseMembers, EntityWithPascalCaseMembers>(snakeObj);
        Assert.Equal(123, pascalObj!.EntityId);
    }

    [Fact]
    public void MapperConfiguration_AddCalculation_ShouldMapCalculation()
    {
        Person person = new Person()
        {
            PersonId = 1,
            FirstName = "John",
            Surname = "Doe",
            DoB = new DateTime(1960, 8, 26)
        };
        var mapper = MapperConfiguration.Create()
           .RegisterType<Person>()
           .RegisterType<PersonWithFullName>()
           .RegisterCalculation<Person, string>("FullName", (p) => p.FirstName + " " + p.Surname)
           .Build();

        var person2 = mapper.MapOne<Person, PersonWithFullName>(person);
    }
}