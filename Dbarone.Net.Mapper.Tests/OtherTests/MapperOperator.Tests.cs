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

    [Fact]
    public void TestPrettyPrint()
    {
        Company[] companies = new Company[] {
        new Company {
            CompanyId=123,
            CompanyName="Acme Ltd",
            Employees = new Person[]{
                new Person { PersonId = 1, PersonName = "Wily C.", DoB=new DateTime(1990,01,01)},
                new Person { PersonId = 2, PersonName = "R.Runner", DoB=new DateTime(1991,07,01)},
            }
        },

        new Company {
            CompanyId=456,
            CompanyName="Globex Corporation",
            Employees = new Person[]{
                new Person { PersonId = 1, PersonName = "Homer", DoB=new DateTime(1990,01,01)},
                new Person { PersonId = 2, PersonName = "Marge", DoB=new DateTime(1991,07,01)},
            }
        } };

        var objectMapper = new ObjectMapper(new MapperConfiguration().SetAutoRegisterTypes(true));
        var op = objectMapper.GetMapperOperator(typeof(Company[]), typeof(List<CompanyDto>));
        var str = op.PrettyPrint();

        Assert.Equal(@"+- EnumerableMapperOperator (Company[]->List`1)
   +- []: MemberwiseMapperOperator (Company->CompanyDto)
      +- CompanyId: AssignableMapperOperator (Int32->Int32)
      +- CompanyName: AssignableMapperOperator (String->String)
      +- Employees: EnumerableMapperOperator (Person[]->List`1)
         +- []: MemberwiseMapperOperator (Person->PersonDto)
            +- PersonId: AssignableMapperOperator (Int32->Int32)
            +- PersonName: AssignableMapperOperator (String->String)
            +- DoB: AssignableMapperOperator (DateTime->DateTime)
", str);
    }
}