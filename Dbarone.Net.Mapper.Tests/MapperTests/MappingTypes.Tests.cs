namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Mapper;

public class MappingTypesTests
{
    [Fact]
    public void Mapper_WhenRegisteringTypes_ShouldMap()
    {
        var mapper = MapperConfiguration.Create()
            .RegisterType<DateTime>()
            .Build();

        var d1 = DateTime.Now;
        var d2 = mapper.MapOne<DateTime, DateTime>(d1);
    }
}