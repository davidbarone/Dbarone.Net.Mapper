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
        throw new NotImplementedException();
    }

    public Setter GetSetter(string memberName)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public string[] GetInstanceMembers(object obj)
    {
        return (obj as Dictionary<string, object>).Keys.ToArray();
    }
}