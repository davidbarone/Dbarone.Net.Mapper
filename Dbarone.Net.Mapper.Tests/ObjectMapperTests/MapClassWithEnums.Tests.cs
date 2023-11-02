namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Mapper;
using Dbarone.Net.Extensions.Object;
using System.Linq.Expressions;

public enum DummyEnum
{
    A,
    B,
    C
}

public class EnumType
{
    public DummyEnum EnumValue { get; set; } = DummyEnum.A;

    public static EnumType CreateMin()
    {
        return new EnumType
        {
            EnumValue = DummyEnum.A
        };
    }

    public static EnumType CreateMax()
    {
        return new EnumType
        {
            EnumValue = DummyEnum.C
        };
    }
}

public class EnumTypeNullable
{
    public DummyEnum? EnumValue { get; set; } = null;

    public static EnumTypeNullable CreateMin()
    {
        return new EnumTypeNullable
        {
            EnumValue = DummyEnum.A
        };
    }

    public static EnumTypeNullable CreateMax()
    {
        return new EnumTypeNullable
        {
            EnumValue = DummyEnum.C
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

public class MapClassWithEnumsTests
{
    [Fact]
    public void Should_Map_Enum_Types()
    {
        EnumType obj1 = EnumType.CreateMax();
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<EnumType>()
        );
        var obj2 = mapper.Map<EnumType, EnumType>(obj1);
        Assert.True(obj1.ValueEquals(obj2));
    }

    [Fact]
    public void Should_Map_Enum_Types_Nullable()
    {
        EnumTypeNullable obj1 = EnumTypeNullable.CreateMax();
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<EnumTypeNullable>()
        );
        var obj2 = mapper.Map<EnumTypeNullable, EnumTypeNullable>(obj1);
        Assert.True(obj1.ValueEquals(obj2));
    }

    [Fact]
    public void Should_Map_Enum_Types_Nullable_Max()
    {
        EnumTypeNullable obj1 = EnumTypeNullable.CreateMax();
        var mapper = new ObjectMapper(new MapperConfiguration()
            .RegisterType<EnumTypeNullable>()
        );
        var obj2 = mapper.Map<EnumTypeNullable, EnumTypeNullable>(obj1);
        Assert.True(obj1.ValueEquals(obj2));
    }
}