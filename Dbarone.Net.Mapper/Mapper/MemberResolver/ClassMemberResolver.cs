namespace Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;

/// <summary>
/// General resolver for classes.
/// </summary>
public class ClassMemberResolver : IMemberResolver
{
    /// <summary>
    /// Gets the type members for reference types.
    /// </summary>
    /// <param name="type">The type to get the members for.</param>
    /// <param name="options">The options.</param>
    /// <returns>An array of member names.</returns>
    public string[] GetTypeMembers(Type type, MapperOptions options)
    {
        return GetMembers(type, options).Select(m => m.Key).ToArray();
    }

    private IDictionary<string, MemberInfo> GetMembers(Type type, MapperOptions options)
    {
        BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
        if (options.IncludePrivateMembers)
        {
            bindingFlags |= BindingFlags.NonPublic;
        }
        return type.GetMembers(bindingFlags)
            .Where(m => m.MemberType == MemberTypes.Property || (options.IncludeFields && m.MemberType == MemberTypes.Field))
            .ToDictionary(m => m.Name, m => m);
    }

    /// <summary>
    /// Sets to false for reference types.
    /// </summary>
    public bool DeferMemberResolution => false;

    /// <summary>
    /// Returns a delete that creates instances for reference types.
    /// </summary>
    /// <param name="type">The type to create the delegate for.</param>
    /// <param name="args">The arguments provides to the constructor.</param>
    /// <returns>Returns a delegate that can create an instance.</returns>
    public virtual CreateInstance CreateInstance(Type type, params object[] args)
    {
        List<ParameterExpression> parameters = new List<ParameterExpression>();

        // args array (optional)
        parameters.Add(Expression.Parameter(typeof(object[]), "args"));

        return Expression.Lambda<CreateInstance>(Expression.New(type), parameters).Compile();
    }

    public Getter GetGetter(Type type, string memberName, MapperOptions options)
    {
        if (string.IsNullOrWhiteSpace(memberName)) throw new ArgumentException(nameof(memberName));

        var memberInfo = this.GetMembers(type, options)[memberName];

        if (memberInfo == null)
        {
            throw new ArgumentException(nameof(memberName));
        }

        // if has no read
        if (memberInfo is PropertyInfo && (memberInfo as PropertyInfo).CanRead == false) return null;

        var obj = Expression.Parameter(typeof(object), "o");
        var accessor = Expression.MakeMemberAccess(Expression.Convert(obj, memberInfo.DeclaringType), memberInfo);

        return Expression.Lambda<Getter>(Expression.Convert(accessor, typeof(object)), obj).Compile();
    }

    public Setter GetSetter(Type type, string memberName, MapperOptions options)
    {
        if (string.IsNullOrWhiteSpace(memberName)) throw new ArgumentException(nameof(memberName));

        var memberInfo = this.GetMembers(type, options)[memberName];

        if (memberInfo == null)
        {
            throw new ArgumentException(nameof(memberName));
        }

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

    public Type GetMemberType(Type type, string memberName, MapperOptions options)
    {
        var memberInfo = this.GetMembers(type, options)[memberName];

        if (memberInfo is PropertyInfo) return (memberInfo as PropertyInfo).PropertyType;
        else if (memberInfo is FieldInfo) return (memberInfo as FieldInfo).FieldType;
        else
        {
            throw new Exception("whoops");
        }
    }

    public string[] GetInstanceMembers(object obj)
    {
        throw new NotImplementedException();
    }

    public bool CanResolveMembersForType(Type type){
        return !type.IsClass;
    }

}