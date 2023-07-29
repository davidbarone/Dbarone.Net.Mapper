namespace Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;

/// <summary>
/// General resolver for structs.
/// </summary>
public class StructMemberResolver : ClassMemberResolver
{
    public StructMemberResolver(Type type, MapperOptions options) : base(type, options) { }
    
    public override CreateInstance CreateInstance(params object[] args)
    {
        List<ParameterExpression> parameters = new List<ParameterExpression>();

        // args array (optional)
        parameters.Add(Expression.Parameter(typeof(object[]), "args"));

        // Create 'new object' expression. The expression must be boxed / cast to object to allow for structs
        var newExp = Expression.Convert(Expression.New(type), typeof(object));

        return Expression.Lambda<CreateInstance>(newExp, parameters).Compile();
    }
}