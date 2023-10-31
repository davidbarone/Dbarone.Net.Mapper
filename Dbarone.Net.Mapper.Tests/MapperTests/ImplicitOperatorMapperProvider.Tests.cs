using Dbarone.Net.Mapper;

/// <summary>
/// Test class which offers implicit conversion from Int32 type.
/// </summary>
public class ImplicitOperatorTest
{
    public string StringValue { get; set; }
    public ImplicitOperatorTest(int i)
    {
        StringValue = new string('X', i);
    }

    public static implicit operator ImplicitOperatorTest(int i) => new ImplicitOperatorTest(i);
}

public class ImplicitOperatorMapperProviderTests
{

    [Fact]
    public void Map_Int_To_Float_Should_Map()
    {
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<int>()
            .RegisterType<ImplicitOperatorTest>()
        );

        int len = 10;
        var expected = new string('X', len);  // 'XXXXXXXXXX'
        var actual = mapper.MapOne<int, ImplicitOperatorTest>(len);
        Assert.Equal(expected, actual.StringValue);
    }
}