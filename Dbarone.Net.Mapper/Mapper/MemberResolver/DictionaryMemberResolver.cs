namespace Dbarone.Net.Mapper;
using System.Reflection;

/// <summary>
/// Member resolver for dictionaries.
/// </summary>
public class DictionaryMemberResolver : IMemberResolver
{
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
        throw new NotImplementedException();
    }

    public Type GetMemberType(string memberName)
    {
        throw new NotImplementedException();
    }
}