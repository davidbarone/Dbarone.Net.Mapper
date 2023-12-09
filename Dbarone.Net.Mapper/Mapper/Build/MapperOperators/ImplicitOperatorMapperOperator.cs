using System.Reflection;

namespace Dbarone.Net.Mapper;

/// <summary>
/// Maps types where an implicit cast operator has been defined on either From or To type. Implicit types always succeed and never throw an exception.
/// </summary>
public class ImplicitOperatorMapperOperator : MapperOperator
{
    /// <summary>
    /// Creates a new <see cref="ImplicitOperatorMapperOperator"/> instance.
    /// </summary>
    /// <param name="builder">The <see cref="MapperBuilder"/> instance.</param>
    /// <param name="from">The From <see cref="BuildType"/> instance.</param>
    /// <param name="to">The To <see cref="BuildType"/> instance.</param>
    /// <param name="parent">An optional parent <see cref="MapperOperator"/> instance.</param>
    public ImplicitOperatorMapperOperator(MapperBuilder builder, BuildType from, BuildType to, MapperOperator? parent = null) : base(builder, from, to, parent) { }

    /// <summary>
    /// Overrides the priority of the <see cref="ImplicitOperatorMapperOperator"/> instance.
    /// </summary>
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

    /// <summary>
    /// The <see cref="ImplicitOperatorMapperOperator"/> operator is able to map when an implicit operator exists between the From and To types. 
    /// </summary>
    /// <returns>Returns true when an implicit operator exists between the From and To types.</returns>
    public override bool CanMap()
    {
        // Check if types map directly via assignment
        var implicitOperator = GetImplicitCast();
        return implicitOperator != null;
    }

    /// <summary>
    /// Mapping implementation for <see cref="ImplicitOperatorMapperOperator"/> type. 
    /// </summary>
    /// <param name="source">The source object.</param>
    /// <param name="target">The optional target object.</param>
    /// <returns>Returns a mapped object.</returns>
    /// <exception cref="MapperBuildException">Returns a <see cref="MapperBuildException"/> in the event of any failure to map the object.</exception>
    protected override object? MapInternal(object? source, object? target)
    {
        var implicitOperator = GetImplicitCast();
        if (implicitOperator == null)
        {
            throw new MapperBuildException(From.Type, MapperEndPoint.Source, "", $"No implicit cast exists to {To.Type}.");
        }
        target = implicitOperator.Invoke(null, new object?[] { source })!;
        return target;
    }
}