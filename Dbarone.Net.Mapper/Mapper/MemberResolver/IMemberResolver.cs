namespace Dbarone.Net.Mapper;
using System.Reflection;

/// <summary>
/// Interface for describing methods to 
/// </summary>
public interface IMemberResolver
{
    Getter GetGetter(Type type, MemberInfo memberInfo);
    Setter GetSetter(Type type, MemberInfo memberInfo);
}