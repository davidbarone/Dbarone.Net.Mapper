namespace Dbarone.Net.Mapper.Tests;

public class ClassB
{
    public int IntValue { get; set; }
    public float FloatValue { get; set; }
    public string StringValue { get; set; } = default!;
    public EnumValue EnumValue { get; set; }
    public Nullable<short> ShortValue { get; set; } = short.MaxValue;
}
