namespace Dbarone.Net.Mapper.Tests;
using System.Reflection;
using Dbarone.Net.Extensions;

public class MapperConfigurationTests
{
    [Fact]
    public void ShouldBuild_When_RegisterType()
    {
        var mapper = MapperConfiguration.Create()
        .RegisterType<SimpleEntity>()
        .Build();
        Assert.NotNull(mapper);
        Assert.Equal(1, mapper.GetTypeConfigurationKeys().Count());
    }

    [Theory]
    [InlineData(false, false, 2)]
    [InlineData(false, true, 3)]
    [InlineData(true, false, 3)]
    [InlineData(true, true, 8)]
    public void Should_Include_Private_Properties_And_Fields(bool includePrivateMembers, bool includeFields, int expectedMemberCount)
    {
        var mapper = MapperConfiguration.Create().RegisterType<TypeWithPrivatePropertiesAndFields>(new MapperOptions
        {
            IncludeFields = includeFields,
            IncludePrivateMembers = includePrivateMembers
        }).Build();
        Assert.Equal(1, mapper.GetTypeConfigurationKeys().Count());

        MapperTypeConfiguration typeConfig = mapper.GetTypeConfiguration(typeof(TypeWithPrivatePropertiesAndFields));
        Assert.Equal(expectedMemberCount, typeConfig.MemberConfiguration.Count());
    }

    [Fact]
    public void Should_Allow_Multiple_Type_Registration()
    {
        Type[] types = new Type[]{
            typeof(ClassA),
            typeof(ClassB)
        };

        var mapper = MapperConfiguration.Create()
        .RegisterTypes(types)
        .Build();

        Assert.Equal(2, mapper.GetTypeConfigurationKeys().Count());
    }

    [Fact]
    public void Should_Allow_Multiple_Type_Registration_2()
    {
        var dtoTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.StartsWith("VectorStruct")).ToArray();

        var mapper = MapperConfiguration.Create()
            .RegisterTypes(dtoTypes).Build();

        Assert.Equal(2, mapper.GetTypeConfigurationKeys().Count());
    }

    [Fact]
    public void When_Register_Type_Should_Return_Correct_Member_Rules()
    {
        var mapper = MapperConfiguration.Create().RegisterType<SimpleEntity>().Build();
        Assert.Equal(1, mapper.GetTypeConfigurationKeys().Count());

        // should be 2 member rules
        MapperTypeConfiguration typeConfig = mapper.GetTypeConfiguration(typeof(SimpleEntity));
        Assert.Equal(2, typeConfig.MemberConfiguration.Count());
    }

    [Fact]
    public void Should_Allow_Rename_Of_Member()
    {
        var mapper = MapperConfiguration.Create()
            .RegisterType<SimpleEntity>()
            .Rename<SimpleEntity>(e => e.EntityId, "Entity_Id")
            .Build();  // rename EntityId -> Entity_Id.

        Assert.Equal(2, mapper.GetTypeConfiguration(typeof(SimpleEntity)).MemberConfiguration.Count());
        Assert.Contains("Entity_Id", mapper.GetTypeConfiguration(typeof(SimpleEntity)).GetActiveInternalMemberNames());
    }

    [Fact]
    public void Should_Throw_Exception_When_Duplicate_Internal_Names()
    {
        var config = MapperConfiguration.Create()
            .RegisterType<CustomerEntity>()
            .Rename<CustomerEntity>(c => c.CustomerId, "Name");  // 2 Name members now exist.

        Assert.Throws<MapperException>(() => config.Build());
    }

    [Fact]
    public void When_Apply_CaseChangeMemberRenameStrategy_ShouldMatchMembers()
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
    public void When_Add_Calculation_Should_Map_Calculation()
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