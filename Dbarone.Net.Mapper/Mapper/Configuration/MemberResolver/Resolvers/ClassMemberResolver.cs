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
    /// Set to true for dictionary and dynamic types, where the member information
    /// must be deferred until mapping time. If set to false, the member information
    /// is obtained at build time.
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

    /// <summary>
    /// Returns a getter delegate that gets a member value for an object.
    /// </summary>
    /// <param name="type">The type to get the getter for.</param>
    /// <param name="memberName">The member name.</param>
    /// <param name="options">The mapper options provided for the type.</param>
    /// <returns>Returns a getter object which, when invoked, will get a member value from an object.
    /// Returns a null reference if getter does not exist.</returns>
    public Getter? GetGetter(Type type, string memberName, MapperOptions options)
    {
        if (string.IsNullOrWhiteSpace(memberName)) throw new ArgumentException(nameof(memberName));

        var memberInfo = this.GetMembers(type, options)[memberName];

        if (memberInfo == null)
        {
            throw new ArgumentException(nameof(memberName));
        }

        // if has no read
        var memberInfoAsPropertyInfo = memberInfo as PropertyInfo;
        if (memberInfoAsPropertyInfo != null && memberInfoAsPropertyInfo.CanRead == false) return null;

        var obj = Expression.Parameter(typeof(object), "o");

        if (memberInfo.DeclaringType == null)
        {
            throw new Exception($"Cannot get getter for member [${memberName}] on type: ${type.Name}. Declaring type not available.");
        }
        else
        {
            var accessor = Expression.MakeMemberAccess(Expression.Convert(obj, memberInfo.DeclaringType), memberInfo);
            return Expression.Lambda<Getter>(Expression.Convert(accessor, typeof(object)), obj).Compile();
        }
    }

    /// <summary>
    /// Returns a setter delegate that sets a member value for an object. 
    /// </summary>
    /// <param name="type">The type to get the setter for.</param>
    /// <param name="memberName">The member name</param>
    /// <param name="options">The mapper options provided for the type.</param>
    /// <returns>Returns a setter object which, when invoked, will set a member value for an object.
    /// Returns a null reference if setter does not exist.</returns>
    public Setter? GetSetter(Type type, string memberName, MapperOptions options)
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
        if (propertyInfo != null && propertyInfo.CanWrite == false) return null;

        // if *Structs*, use direct reflection - net35 has no Expression.Unbox to cast target
        if (type.GetTypeInfo().IsValueType)
        {
            return fieldInfo != null ?
                (Setter)fieldInfo.SetValue :
                ((t, v) => { if (propertyInfo != null) { propertyInfo.SetValue(t, v, null); } });
        }

        var dataType = propertyInfo != null ?
            propertyInfo.PropertyType : (fieldInfo != null ? fieldInfo.FieldType : null);

        if (dataType == null)
        {
            throw new Exception($"Error in GetSetter(). Data type is null for member [${memberName}] for type [${type.Name}].");
        }
        else
        {
            var target = Expression.Parameter(typeof(object), "obj");
            var value = Expression.Parameter(typeof(object), "val");

            var castTarget = Expression.Convert(target, type);
            var castValue = Expression.ConvertChecked(value, dataType);

            var accessor = propertyInfo != null ?
                Expression.Property(castTarget, propertyInfo) : (fieldInfo != null ? Expression.Field(castTarget, fieldInfo) : null);

            if (accessor == null)
            {
                throw new Exception($"Error in GetSetter(). Accessor is null for member [${memberName}] for type [${type.Name}].");
            }
            else
            {
                var assign = Expression.Assign(accessor, castValue);
                var conv = Expression.Convert(assign, typeof(object));
                return Expression.Lambda<Setter>(conv, target, value).Compile();
            }
        }
    }

    /// <summary>
    /// Gets a member type.
    /// </summary>
    /// <param name="type">The type containing the member.</param>
    /// <param name="memberName">The member name.</param>
    /// <param name="options">The mapper options provided for the type.</param>
    /// <returns>Returns the member type.</returns>
    public Type GetMemberType(Type type, string memberName, MapperOptions options)
    {
        var memberInfo = this.GetMembers(type, options)[memberName];
        var propertyInfo = memberInfo as PropertyInfo;
        var fieldInfo = memberInfo as FieldInfo;

        if (propertyInfo != null)
        {
            return propertyInfo.PropertyType;
        }
        else if (fieldInfo!=null)
        {
            return fieldInfo.FieldType;
        }
        else
        {
            throw new Exception($"Cannot return member type for member [${memberName}] on type [{type.Name}].");
        }
    }

    /// <summary>
    /// Gets members on an instance. This method is normally implemented only if DeferMemberResolution is set to true.
    /// </summary>
    /// <param name="obj">The object instance.</param>
    /// <returns>A string array of member names.</returns>
    public string[] GetInstanceMembers(object obj)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Set to true if the current IMemberResolver can resolve members of the specified type.
    /// </summary>
    /// <param name="type">The type to resolve members for.</param>
    /// <returns>Returns true if the current IMemberResolver can resolve members of the specified type.</returns>
    public virtual bool CanResolveMembersForType(Type type)
    {
        return type.IsClass;
    }
}