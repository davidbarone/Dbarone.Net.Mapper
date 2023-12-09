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

        var op = mapper.GetMapperOperator<int, float>();
        Assert.NotNull(op);
    }
}