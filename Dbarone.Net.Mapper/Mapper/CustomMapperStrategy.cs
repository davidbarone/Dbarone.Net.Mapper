namespace Dbarone.Net.Mapper;
using System.Linq.Expressions;
using System.Reflection;

/// <summary>
/// Implements a custom mapper strategy.
/// </summary>
public class CustomMapperStrategy<T, U> : IMapperStrategy
{
    Dictionary<Expression<Func<T, object>>, Expression<Func<U, object>>> _customMappingRules = new();

    /// <summary>
    /// Configures the CustomMapperStrategy using a set of expressions.
    /// </summary>
    /// <param name="from">The from expression.</param>
    /// <param name="to">The to expression.</param>
    /// <returns>A configured custom mapper strategy.</returns>
    public CustomMapperStrategy<T, U> Configure(Expression<Func<T, object>> from, Expression<Func<U, object>> to)
    {
        _customMappingRules.Add(from, to);
        return this;
    }

    /// <summary>
    /// Custom implementation of MapTypes method.
    /// </summary>
    /// <param name="T">The source type.</param>
    /// <param name="U">The target type.</param>
    /// <returns></returns>
    public IList<PropertyMap> MapTypes(Type T, Type U)
    {
        List<PropertyMap> map = new();

        foreach (var key in _customMappingRules.Keys)
        {
            var fromExpression = key;
            var toExpression = _customMappingRules[key];

            var fromBody = fromExpression.Body;
            if (fromBody.NodeType == ExpressionType.Convert)
                fromBody = ((UnaryExpression)fromBody).Operand;

            var sourceProperty = (fromBody as MemberExpression)?.Member as PropertyInfo;

            var toBody = toExpression.Body;
            if (toBody.NodeType == ExpressionType.Convert)
                toBody = ((UnaryExpression)toBody).Operand;

            var targetProperty = (toBody as MemberExpression)?.Member as PropertyInfo;

            map.Add(new PropertyMap
            {
                SourceProperty = sourceProperty,
                TargetProperty = targetProperty
            });
        }
        return map;
    }
}