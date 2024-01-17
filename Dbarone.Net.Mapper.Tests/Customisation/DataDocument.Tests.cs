using Dbarone.Net.Document;
using Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;

namespace Dbarone.Net.Mapper.Tests;

/// <summary>
/// Member resolver for documents.
/// </summary>
public class DocumentMemberResolver : IMemberResolver
{
    /// <summary>
    /// Set to true for document types.
    /// </summary>
    public bool DeferBuild => true;

    /// <summary>
    /// Returns a getter delegate that gets a member value for an object.
    /// </summary>
    /// <param name="type">The type to get the getter for.</param>
    /// <param name="memberName">The member name.</param>
    /// <param name="options">The mapper options provided for the type.</param>
    /// <returns>Returns a getter object which, when invoked, will get a member value from an object.
    /// Returns a null reference if getter does not exist.</returns>
    public Getter GetGetter(Type type, string memberName, MapperOptions options)
    {
        Getter func = (object obj) =>
        {
            var objDict = obj as IDictionary<string, DocValue>;
            if (objDict != null)
            {
                return (object)objDict[memberName];
            }
            else
            {
                throw new Exception("Source object not a valid document type.");
            }
        };
        return func;
    }

    /// <summary>
    /// Returns a setter delegate that sets a member value for an object. 
    /// </summary>
    /// <param name="type">The type to get the setter for.</param>
    /// <param name="memberName">The member name</param>
    /// <param name="options">The mapper options provided for the type.</param>
    /// <returns>Returns a setter object which, when invoked, will set a member value for an object.
    /// Returns a null reference if setter does not exist.</returns>
    public Setter GetSetter(Type type, string memberName, MapperOptions options)
    {
        Setter action = delegate (object target, object? value)
        {
            var objDict = target as IDictionary<string, DocValue>;
            if (objDict != null)
            {
                objDict[memberName] = (DocValue)value;
            }
            else
            {
                throw new Exception("Target must implement IDictionary<string, DocValue>.");
            }
        };
        return action;
    }

    /// <summary>
    /// Returns a CreateInstance delegate that can create a new instance of a particular type.
    /// </summary>
    /// <param name="type">The type to create the CreateInstance delegate for.</param>
    /// <param name="args">The arguments to provide to the constructor function to create the new instance.</param>
    /// <returns>Returns a delegate that, when invoked, will create a new instance of an object.</returns>
    public CreateInstance CreateInstance(Type type, params object?[]? args)
    {
        List<ParameterExpression> parameters = new List<ParameterExpression>();

        // args array (optional)
        parameters.Add(Expression.Parameter(typeof(object[]), "args"));

        return Expression.Lambda<CreateInstance>(Expression.New(type), parameters).Compile();
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
        return typeof(DocValue);
    }

    /// <summary>
    /// Gets members on an instance. This method is normally implemented only if DeferMemberResolution is set to true.
    /// </summary>
    /// <param name="obj">The object instance.</param>
    /// <returns>A string array of member names.</returns>
    public string[] GetInstanceMembers(object obj)
    {
        var objDict = obj as IDictionary<string, DocValue>;
        if (objDict != null)
        {
            return objDict.Keys.ToArray();
        }
        else
        {
            throw new Exception("Object must implement IDictionary<string, DocValue> interface.");
        }
    }

    /// <summary>
    /// Returns the member names for a type.
    /// </summary>
    /// <param name="type">The type to get the members for.</param>
    /// <param name="options">The options.</param>
    /// <returns></returns>
    public string[] GetTypeMembers(Type type, MapperOptions options)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Set to true if the current IMemberResolver can resolve members of the specified type.
    /// </summary>
    /// <param name="type">The type to resolve members for.</param>
    /// <returns>Returns true if the current IMemberResolver can resolve members of the specified type.</returns>
    public bool CanResolveMembersForType(Type type)
    {
        return type.IsAssignableTo(typeof(DocValue));
    }

    /// <summary>
    /// Returns true if types supported by this resolver have members.
    /// </summary>
    public virtual bool HasMembers => true;
}

public class DataDocumentTests
{
    [Fact]
    public void TestIntToDocumentUsesImplicitOperator()
    {
        var conf = new MapperConfiguration().SetAutoRegisterTypes(true).RegisterResolvers<DocumentMemberResolver>();
        var mapper = new ObjectMapper(conf);
        var op = mapper.GetMapperOperator<int, DocValue>();
        Assert.Equal(typeof(ImplicitOperatorMapperOperator), op.GetType());
    }

    [Fact]
    public void TestIntToDocument()
    {
        var conf = new MapperConfiguration().SetAutoRegisterTypes(true).RegisterResolvers<DocumentMemberResolver>();
        var mapper = new ObjectMapper(conf);

        int a = 123;
        DocValue? doc = mapper.Map<int, DocValue>(a);

        Assert.NotNull(doc);
        if (doc is not null)
        {
            Assert.Equal(a, doc.AsInt32);
        }
    }

    [Fact]
    public void TestDocumentToInt()
    {
        var conf = new MapperConfiguration().SetAutoRegisterTypes(true).RegisterResolvers<DocumentMemberResolver>();
        var mapper = new ObjectMapper(conf);
        var doc = new DocValue(123);
        int a = mapper.Map<DocValue, int>(doc);
        Assert.Equal(123, a);
    }
}