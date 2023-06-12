namespace Dbarone.Net.Mapper;
using System.Reflection;

/// <summary>
/// Member resolver for dictionaries.
/// </summary>
public class DictionaryMemberResolver : IMemberResolver
{
    public Getter GetGetter(Type type, MemberInfo memberInfo)
    {
        throw new NotImplementedException();
    }

    public Setter GetSetter(Type type, MemberInfo memberInfo)
    {
        throw new NotImplementedException();
    }

    public CreateInstance CreateInstance(Type type, params object[] args)
    {
        throw new NotImplementedException();
    }
}