using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Dbarone.Net.Mapper;

public class EventsTests
{

    [Fact]
    public void TestBuildEvent()
    {
        // Given
        var mapper = new ObjectMapper(
            new MapperConfiguration()
            .SetAutoRegisterTypes(true));               // No need to register individual types here.

        int a = 1;
        var buildCount = 0;
        CreateOperatorDelegate onCreateOperator = (MapperOperatorInfo info) => { buildCount++; };
        mapper.OnCreateOperator = onCreateOperator;
        var b = mapper.Map<int, float>(a);  // Will automatically register necessary types here
        Assert.Equal((float)1, b);
        Assert.Equal(1, buildCount);
    }
}