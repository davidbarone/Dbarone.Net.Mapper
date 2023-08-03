namespace Dbarone.Net.Mapper.Tests;

public class EnumTypeNullable
{
    public AlphaEnum? EnumValue { get; set; } = null;

    public static EnumTypeNullable CreateMin()
    {
        return new EnumTypeNullable
        {
            EnumValue = AlphaEnum.A
        };
    }

    public static EnumTypeNullable CreateMax()
    {
        return new EnumTypeNullable
        {
            EnumValue = AlphaEnum.Z
        };
    }

    public static EnumTypeNullable CreateNull()
    {
        return new EnumTypeNullable
        {
            EnumValue = null
        };
    }
}