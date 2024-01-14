using Dbarone.Net.Document;
using Dbarone.Net.Mapper;

namespace Dbarone.Net.Mapper.Tests;

public class DataDocumentTests
{

    [Fact]
    public void TestIntToDocument()
    {
        var mapper = new ObjectMapper(new MapperConfiguration().SetAutoRegisterTypes(true));

        int a = 123;
        DocValue? doc = mapper.Map<int, DocValue>(a);

        Assert.NotNull(doc);
        if (doc is not null)
        {
            Assert.Equal(a, doc.AsInt32);
        }
    }

}