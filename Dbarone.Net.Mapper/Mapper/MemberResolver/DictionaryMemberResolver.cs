namespace Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;

/// <summary>
/// Member resolver for dictionaries.
/// </summary>
public class DictionaryMemberResolver : IMemberResolver
{
    public bool DeferMemberResolution => true;

    public Getter GetGetter(Type type, string memberName, MapperOptions options)
    {
        Getter func = (object obj) => (object)(obj as IDictionary<string, object>)[memberName];
        return func;
    }

    public Setter GetSetter(Type type, string memberName, MapperOptions options)
    {
        Setter action = delegate (object target, object value) { (target as IDictionary<string, object>)[memberName] = value; };
        return action;
    }

    public CreateInstance CreateInstance(Type type, params object[] args)
    {
        List<ParameterExpression> parameters = new List<ParameterExpression>();

        // args array (optional)
        parameters.Add(Expression.Parameter(typeof(object[]), "args"));

        return Expression.Lambda<CreateInstance>(Expression.New(type), parameters).Compile();
    }

    public Type GetMemberType(Type type, string memberName, MapperOptions options)
    {
        return typeof(object);
    }

    public string[] GetInstanceMembers(object obj)
    {
        return (obj as Dictionary<string, object>).Keys.ToArray();
    }

    public string[] GetTypeMembers(Type type, MapperOptions options)
    {
        throw new NotImplementedException();
    }

    public bool CanResolveMembersForType(Type type)
    {
        return type.IsDictionaryType();
    }
}