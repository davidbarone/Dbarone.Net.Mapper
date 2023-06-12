namespace Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;

/// <summary>
/// General resolver for classes.
/// </summary>
public class ClassMemberResolver : IMemberResolver
{
    public CreateInstance CreateInstance(Type type, params object[] args)
    {
        List<ParameterExpression> parameters = new List<ParameterExpression>();

        // args array (optional)
        parameters.Add(Expression.Parameter(typeof(object[]), "args"));
        return Expression.Lambda<CreateInstance>(Expression.New(type), parameters).Compile();
    }

    public Getter GetGetter(Type type, MemberInfo memberInfo)
    {
        if (memberInfo == null) throw new ArgumentNullException(nameof(memberInfo));

        // if has no read
        if (memberInfo is PropertyInfo && (memberInfo as PropertyInfo).CanRead == false) return null;

        var obj = Expression.Parameter(typeof(object), "o");
        var accessor = Expression.MakeMemberAccess(Expression.Convert(obj, memberInfo.DeclaringType), memberInfo);

        return Expression.Lambda<Getter>(Expression.Convert(accessor, typeof(object)), obj).Compile();
    }

    public Setter GetSetter(Type type, MemberInfo memberInfo)
    {
        if (memberInfo == null) throw new ArgumentNullException(nameof(memberInfo));

        var fieldInfo = memberInfo as FieldInfo;
        var propertyInfo = memberInfo as PropertyInfo;

        // if is property and has no write
        if (memberInfo is PropertyInfo && propertyInfo.CanWrite == false) return null;

        // if *Structs*, use direct reflection - net35 has no Expression.Unbox to cast target
        if (type.GetTypeInfo().IsValueType)
        {
            return memberInfo is FieldInfo ?
                (Setter)fieldInfo.SetValue :
                ((t, v) => propertyInfo.SetValue(t, v, null));
        }

        var dataType = memberInfo is PropertyInfo ?
            propertyInfo.PropertyType :
            fieldInfo.FieldType;

        var target = Expression.Parameter(typeof(object), "obj");
        var value = Expression.Parameter(typeof(object), "val");

        var castTarget = Expression.Convert(target, type);
        var castValue = Expression.ConvertChecked(value, dataType);

        var accessor = memberInfo is PropertyInfo ?
            Expression.Property(castTarget, propertyInfo) :
            Expression.Field(castTarget, fieldInfo);

        var assign = Expression.Assign(accessor, castValue);
        var conv = Expression.Convert(assign, typeof(object));

        return Expression.Lambda<Setter>(conv, target, value).Compile();
    }
}