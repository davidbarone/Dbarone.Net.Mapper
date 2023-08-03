namespace Dbarone.Net.Mapper.Tests;

public class EnumType
{
    public AlphaEnum EnumValue { get; set; } = AlphaEnum.A;

    public static EnumType CreateMin()
    {
        return new EnumType
        {
            EnumValue = AlphaEnum.A
        };
    }

    public static EnumType CreateMax()
    {
        return new EnumType
        {
            EnumValue = AlphaEnum.Z
        };
    }
}
