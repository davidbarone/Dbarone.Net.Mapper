namespace Dbarone.Net.Mapper;

/// <summary>
/// Interface for describing methods to 
/// </summary>
public interface IMemberResolver
{
    Getter GetGetter(Type type, string memberName);
    Setter GetSetter(Type type, string memberName);
}