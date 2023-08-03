namespace Dbarone.Net.Mapper.Tests;
using Dbarone.Net.Mapper;
using Dbarone.Net.Extensions.Object;
using System.Linq.Expressions;

public class MapOneTests
{
    [Fact]
    public void Should_Map_Builtin_Value_Types_Min() {
        BuiltinValueTypes obj1 = BuiltinValueTypes.CreateMin();
        var mapper = MapperConfiguration.Create().RegisterType<BuiltinValueTypes>().Build();
        var obj2 = mapper.MapOne<BuiltinValueTypes, BuiltinValueTypes>(obj1);
        Assert.True(obj1.ValueEquals(obj2));
    }

    [Fact]
    public void Should_Map_Builtin_Value_Types_Max() {
        BuiltinValueTypes obj1 = BuiltinValueTypes.CreateMax();
        var mapper = MapperConfiguration.Create().RegisterType<BuiltinValueTypes>().Build();
        var obj2 = mapper.MapOne<BuiltinValueTypes, BuiltinValueTypes>(obj1);
        Assert.True(obj1.ValueEquals(obj2));
    }

    [Fact]
    public void Should_Map_Builtin_Value_Types_Nullable() {
        BuiltinValueTypesNullable obj1 = BuiltinValueTypesNullable.CreateNull();
        var mapper = MapperConfiguration.Create().RegisterType<BuiltinValueTypesNullable>().Build();
        var obj2 = mapper.MapOne<BuiltinValueTypesNullable, BuiltinValueTypesNullable>(obj1);
        Assert.True(obj1.ValueEquals(obj2));
    }

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