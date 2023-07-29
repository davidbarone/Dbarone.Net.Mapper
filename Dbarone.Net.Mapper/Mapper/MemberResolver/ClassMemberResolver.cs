namespace Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;

/// <summary>
/// General resolver for classes.
/// </summary>
public class ClassMemberResolver : IMemberResolver
{
    protected Type type;
    protected IDictionary<string, MemberInfo> members;

    public ClassMemberResolver(Type type, MapperOptions options)
    {
        this.type = type;
        this.members = GetTypeMembers(options);
    }

    private IDictionary<string, MemberInfo> GetTypeMembers(MapperOptions options)
    {
        BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
        if (options.IncludePrivateMembers)
        {
            bindingFlags |= BindingFlags.NonPublic;
        }
        var members = this.type.GetMembers(bindingFlags).Where(m => m.MemberType == MemberTypes.Property || (options.IncludeFields && m.MemberType == MemberTypes.Field));
        return members.ToDictionary(m => m.Name, m => m);
    }

    public bool DeferMemberResolution => false;

    public string[] GetMembers() { return this.members.Keys.ToArray(); }

    public virtual CreateInstance CreateInstance(params object[] args)
    {
        List<ParameterExpression> parameters = new List<ParameterExpression>();

        // args array (optional)
        parameters.Add(Expression.Parameter(typeof(object[]), "args"));

        return Expression.Lambda<CreateInstance>(Expression.New(this.type), parameters).Compile();
    }

    public Getter GetGetter(string memberName)
    {
        if (string.IsNullOrWhiteSpace(memberName)) throw new ArgumentException(nameof(memberName));

        var memberInfo = this.members[memberName];

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

    public Setter GetSetter(string memberName)
    {
        if (string.IsNullOrWhiteSpace(memberName)) throw new ArgumentException(nameof(memberName));

        var memberInfo = this.members[memberName];

        if (memberInfo == null)
        {
            throw new ArgumentException(nameof(memberName));
        }

        var fieldInfo = memberInfo as FieldInfo;
        var propertyInfo = memberInfo as PropertyInfo;

        // if is property and has no write
        if (memberInfo is PropertyInfo && propertyInfo.CanWrite == false) return null;

        // if *Structs*, use direct reflection - net35 has no Expression.Unbox to cast target
        if (this.type.GetTypeInfo().IsValueType)
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

    public Type GetMemberType(string memberName)
    {
        var member = this.members[memberName];
        if (member is PropertyInfo) return (member as PropertyInfo).PropertyType;
        else if (member is FieldInfo) return (member as FieldInfo).FieldType;
        else
        {
            throw new Exception("whoops");
        }
    }
}