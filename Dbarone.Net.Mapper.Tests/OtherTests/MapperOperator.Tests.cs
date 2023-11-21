using System.Diagnostics;

namespace Dbarone.Net.Mapper;

public class Company
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public Person[] Employees { get; set; }
}

public class Person
{
    public int PersonId { get; set; }
    public string PersonName { get; set; }
    public DateTime DoB { get; set; }
}

public class CompanyDto
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public List<PersonDto> Employees { get; set; }
}

public class PersonDto
{
    public int PersonId { get; set; }
    public string PersonName { get; set; }
    public DateTime DoB { get; set; }
}


public class MapperOperatorTests
{
    [Fact]
    public void GetOperator1()
    {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int>()
            .RegisterType<float>()
        );

        var op = mapper.GetOperator<int, float>();
        Assert.NotNull(op);
    }

    [Fact]
    public void TestExecutionPlanOutput()
    {
        Company companyA = new Company()
        {
            CompanyId = 1,
            CompanyName = "Test Company A",
            Employees = new Person[3] {
                new Person() {PersonId = 1, PersonName = "Paul"},
                new Person() {PersonId = 2, PersonName = "Tony"},
                new Person() {PersonId = 3, PersonName = "Ian"}
            }
        };

        var mapper = new ObjectMapper(new MapperConfiguration().SetAutoRegisterTypes(true));
        var op = mapper.GetOperator<Company, CompanyDto>();
        var executionPlan = op.ToExecutionPlanNode();
        Assert.NotNull(executionPlan);

        // Also do map
        var companyB = mapper.Map<Company, CompanyDto>(companyA); 
    }
}