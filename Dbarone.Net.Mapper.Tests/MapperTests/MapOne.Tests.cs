namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Mapper;
using Dbarone.Net.Extensions.Object;
using System.Linq.Expressions;

public class MapOneTests
{
   [Fact]
    public void Should_Map_Enum_Types() {
        EnumType obj1 = EnumType.CreateMax();
        var mapper = MapperConfiguration.Create().RegisterType<EnumType>().Build();
        var obj2 = mapper.MapOne<EnumType, EnumType>(obj1);
        Assert.True(obj1.ValueEquals(obj2));
    }

   [Fact]
    public void Should_Map_Enum_Types_Nullable() {
        EnumTypeNullable obj1 = EnumTypeNullable.CreateNull();
        var mapper = MapperConfiguration.Create().RegisterType<EnumTypeNullable>().Build();
        var obj2 = mapper.MapOne<EnumTypeNullable, EnumTypeNullable>(obj1);
        Assert.True(obj1.ValueEquals(obj2));
    }

   [Fact]
    public void Should_Map_Enum_Types_Nullable_Max() {
        EnumTypeNullable obj1 = EnumTypeNullable.CreateMax();
        var mapper = MapperConfiguration.Create().RegisterType<EnumTypeNullable>().Build();
        var obj2 = mapper.MapOne<EnumTypeNullable, EnumTypeNullable>(obj1);
        Assert.True(obj1.ValueEquals(obj2));
    }

}