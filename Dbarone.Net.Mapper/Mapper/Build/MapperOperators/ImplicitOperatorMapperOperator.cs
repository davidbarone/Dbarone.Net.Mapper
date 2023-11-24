using System.Reflection;

namespace Dbarone.Net.Mapper;

public class ImplicitOperatorMapperOperator : MapperOperator
{
    public ImplicitOperatorMapperOperator(MapperBuilder builder, BuildType from, BuildType to, MapperOperator parent = null) : base(builder, from, to, parent) { }

    public override int Priority => 40;

    private MethodInfo? GetImplicitCast()
    {
        var methods = From.Type.GetMethods(BindingFlags.Public | BindingFlags.Static);
        var method = methods
                        .FirstOrDefault(
                            m => m.ReturnType == To.Type &&
                            m.Name == "op_Implicit"
                        );
        if (method == null)
        {
            // try reverse conversion
            methods = To.Type.GetMethods(BindingFlags.Public | BindingFlags.Static);
            method = methods
                            .FirstOrDefault(
                                m => m.ReturnType == To.Type &&
                                m.Name == "op_Implicit"
                            );
        }
        return method;
    }

    public override bool CanMap()
    {
        // Check if types map directly via assignment
        var implicitOperator = GetImplicitCast();
        return (implicitOperator != null);
    }

    public override MapperDelegate GetMap()
    {
        var implicitOperator = GetImplicitCast();
        if (implicitOperator == null)
        {
            throw new MapperBuildException(From.Type, MapperEndPoint.Source, "", $"No implicit cast exists to {To.Type}.");
        }
        MapperDelegate mapping = (s, d) =>
        {
            d = implicitOperator.Invoke(null, new object[] { s })!;
            return d;
        };
        return mapping;
    }
}