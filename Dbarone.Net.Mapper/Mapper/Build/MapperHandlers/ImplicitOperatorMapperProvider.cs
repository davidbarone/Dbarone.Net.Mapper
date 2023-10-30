
using System.Reflection;

namespace Dbarone.Net.Mapper;

public class ImplicitOperatorMapperProvider : IMapperProvider
{
    private MethodInfo? GetImplicitCast(Type fromType, Type toType)
    {
        var method = fromType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .FirstOrDefault(
                            m => m.ReturnType == toType &&
                            m.Name == "op_Implicit"
                        );
        return method;
    }

    public bool CanCreateMapFor(Type from, Type to)
    {
        // Check if types map directly via assignment
        var implicitOperator = GetImplicitCast(from, to);
        return (implicitOperator != null);
    }

    public MapperDelegate GetMapFor(Type from, Type to, MapperBuilder builder)
    {
        var implicitOperator = GetImplicitCast(from, to);
        if (implicitOperator == null)
        {
            throw new Exception("whoops");
        }
        MapperDelegate mapping = (s, d) =>
        {
            d = implicitOperator.Invoke(s, new object[] { })!;
            return d;
        };
        return mapping;
    }
}