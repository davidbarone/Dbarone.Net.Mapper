
using System.Reflection;

namespace Dbarone.Net.Mapper;

public class ImplicitOperatorMapperProvider : IMapperProvider
{
    private MethodInfo? GetImplicitCast(Type fromType, Type toType)
    {
        var methods = fromType.GetMethods(BindingFlags.Public | BindingFlags.Static);
        var method = methods
                        .FirstOrDefault(
                            m => m.ReturnType == toType &&
                            m.Name == "op_Implicit"
                        );
        if (method == null)
        {
            // try reverse conversion
            methods = toType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            method = methods
                            .FirstOrDefault(
                                m => m.ReturnType == toType &&
                                m.Name == "op_Implicit"
                            );
        }
        return method;
    }

    public bool CanCreateMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        // Check if types map directly via assignment
        var implicitOperator = GetImplicitCast(from.Type, to.Type);
        return (implicitOperator != null);
    }

    public MapperDelegate GetMapFor(BuildType from, BuildType to, MapperBuilder builder)
    {
        var implicitOperator = GetImplicitCast(from.Type, to.Type);
        if (implicitOperator == null)
        {
            throw new Exception("whoops");
        }
        MapperDelegate mapping = (s, d) =>
        {
            d = implicitOperator.Invoke(null, new object[] { s })!;
            return d;
        };
        return mapping;
    }
}