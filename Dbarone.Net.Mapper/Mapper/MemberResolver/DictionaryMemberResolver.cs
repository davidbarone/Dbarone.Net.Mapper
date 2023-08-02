namespace Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;

/// <summary>
/// Member resolver for dictionaries.
/// </summary>
public class DictionaryMemberResolver : IMemberResolver
{
    private Type type;
    public DictionaryMemberResolver(Type type)
    {
        this.type = type;
    }

    public bool DeferMemberResolution => true;

    public string[] GetMembers() { throw new NotImplementedException(); }


    public Getter GetGetter(string memberName)
    {
        Getter func = (object obj) => (object)(obj as IDictionary<string, object>)[memberName];
        return func;
    }

    public Setter GetSetter(string memberName)
    {
        Setter action = delegate (object target, object value) { (target as IDictionary<string, object>)[memberName] = value; };
        return action;
    }

    public CreateInstance CreateInstance(params object[] args)
    {
        List<ParameterExpression> parameters = new List<ParameterExpression>();

        // args array (optional)
        parameters.Add(Expression.Parameter(typeof(object[]), "args"));

        return Expression.Lambda<CreateInstance>(Expression.New(this.type), parameters).Compile();
    }

    public Type GetMemberType(string memberName)
    {
        return typeof(object);
    }

    public string[] GetInstanceMembers(object obj)
    {
        return (obj as Dictionary<string, object>).Keys.ToArray();
    }
}