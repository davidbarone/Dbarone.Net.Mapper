namespace Dbarone.Net.Mapper;

/// <summary>
/// General resolver for classes.
/// </summary>
public class ClassMemberResolver : IMemberResolver
{
    public Getter GetGetter(Type type, string memberName)
    {
        throw new NotImplementedException();
    }

    public Setter GetSetter(Type type, string memberName)
    {
        throw new NotImplementedException();
    }
}