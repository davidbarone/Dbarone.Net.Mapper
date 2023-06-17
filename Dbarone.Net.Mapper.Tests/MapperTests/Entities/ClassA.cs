namespace Dbarone.Net.Mapper.Tests;

public class ClassA
{
    public int IntValue { get; set; } = int.MaxValue;
    public float FloatValue { get; set; } = float.MaxValue;
    public string StringValue { get; set; } = "foobar";
    public EnumValue EnumValue { get; set; } = EnumValue.A;
    public Nullable<short> ShortValue { get; set; } = short.MaxValue;
}
