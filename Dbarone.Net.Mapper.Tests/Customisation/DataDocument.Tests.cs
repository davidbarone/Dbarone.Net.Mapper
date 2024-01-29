using Dbarone.Net.Document;
using Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;
using System.Security.Cryptography.X509Certificates;

namespace Dbarone.Net.Mapper.Tests;

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
}

public class Address
{
    public int AddressId { get; set; }
    public string AddressLine1 { get; set; } = default!;
    public string AddressLine2 { get; set; } = default!;
}

public class CustomerWithNestedObject : Customer
{
    public Address Address { get; set; }
}

/// <summary>
/// Member resolver for documents.
/// </summary>
public class DocumentMemberResolver : AbstractMemberResolver, IMemberResolver
{
    /// <summary>
    /// Set to true for document types.
    /// </summary>
    public override bool DeferBuild => true;

    public override bool IsEnumerable => true;

    /// <summary>
    /// Returns a getter delegate that gets a member value for an object.
    /// </summary>
    /// <param name="type">The type to get the getter for.</param>
    /// <param name="memberName">The member name.</param>
    /// <param name="options">The mapper options provided for the type.</param>
    /// <returns>Returns a getter object which, when invoked, will get a member value from an object.
    /// Returns a null reference if getter does not exist.</returns>
    public override Getter GetGetter(Type type, string memberName, MapperOptions options)
    {
        Getter func = (object obj) =>
        {
            var objDict = obj as IDictionary<string, DocumentValue>;
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
    public override Setter GetSetter(Type type, string memberName, MapperOptions options)
    {
        Setter action = delegate (object target, object? value)
        {
            var objDict = target as IDictionary<string, DocumentValue>;
            if (objDict != null)
            {
                objDict[memberName] = (DocumentValue)value;
            }
            else
            {
                throw new Exception("Target must implement IDictionary<string, DocumentValue>.");
            }
        };
        return action;
    }

    /// <summary>
    /// Gets a member type.
    /// </summary>
    /// <param name="type">The type containing the member.</param>
    /// <param name="memberName">The member name.</param>
    /// <param name="options">The mapper options provided for the type.</param>
    /// <returns>Returns the member type.</returns>
    public override Type GetMemberType(Type type, string memberName, MapperOptions options)
    {
        return typeof(DocumentValue);
    }

    /// <summary>
    /// Gets members on an instance. This method is normally implemented only if DeferMemberResolution is set to true.
    /// </summary>
    /// <param name="obj">The object instance.</param>
    /// <returns>A string array of member names.</returns>
    public override string[] GetInstanceMembers(object obj)
    {
        var objDict = obj as IDictionary<string, DocumentValue>;
        if (objDict != null)
        {
            return objDict.Keys.ToArray();
        }
        else
        {
            throw new Exception("Object must implement IDictionary<string, DocumentValue> interface.");
        }
    }

    /// <summary>
    /// Set to true if the current IMemberResolver can resolve members of the specified type.
    /// </summary>
    /// <param name="type">The type to resolve members for.</param>
    /// <returns>Returns true if the current IMemberResolver can resolve members of the specified type.</returns>
    public override bool CanResolveMembersForType(Type type)
    {
        return type.IsAssignableTo(typeof(DocumentValue));
    }

    /// <summary>
    /// Returns true if types supported by this resolver have members.
    /// </summary>
    public override bool HasMembers => true;
}

public class DataDocumentTests
{
    [Fact]
    public void TestIntToDocumentUsesImplicitOperator()
    {
        var conf = new MapperConfiguration().SetAutoRegisterTypes(true).RegisterResolvers<DocumentMemberResolver>();
        var mapper = new ObjectMapper(conf);
        var op = mapper.GetMapperOperator<int, DocumentValue>();
        Assert.Equal(typeof(ImplicitOperatorMapperOperator), op.GetType());
    }

    [Fact]
    public void TestIntToDocument()
    {
        var conf = new MapperConfiguration().SetAutoRegisterTypes(true).RegisterResolvers<DocumentMemberResolver>();
        var mapper = new ObjectMapper(conf);

        int a = 123;
        DocumentValue? doc = mapper.Map<int, DocumentValue>(a);

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
        var doc = new DocumentValue(123);
        int a = mapper.Map<DocumentValue, int>(doc);
        Assert.Equal(123, a);
    }

    [Fact]
    public void TestClassToDocument()
    {
        Customer cust = new Customer()
        {
            CustomerId = 123,
            CustomerName = "Acme Enterprises Ltd"
        };
        var conf = new MapperConfiguration().SetAutoRegisterTypes(true).RegisterResolvers<DocumentMemberResolver>();
        var mapper = new ObjectMapper(conf);
        var doc = mapper.Map<Customer, DictionaryDocument>(cust);
        Assert.NotNull(doc);
        if (doc is not null)
        {
            Assert.Equal(123, doc["CustomerId"].AsInt32);
        }
    }

    [Fact]
    public void TestDocumentToClass()
    {
        DictionaryDocument doc = new DictionaryDocument();
        doc.Add("CustomerId", 123);
        doc.Add("CustomerName", "Acme Enterprises Ltd");

        var conf = new MapperConfiguration().SetAutoRegisterTypes(true).RegisterResolvers<DocumentMemberResolver>();
        var mapper = new ObjectMapper(conf);
        var cust = mapper.Map<DictionaryDocument, Customer>(doc);
        Assert.NotNull(cust);
        if (cust is not null)
        {
            Assert.Equal(123, cust.CustomerId);
        }
    }

    [Fact]
    public void TestClassWithNestedClassToDocument()
    {
        CustomerWithNestedObject cust = new CustomerWithNestedObject()
        {
            CustomerId = 123,
            CustomerName = "Acme Enterprises Ltd",
            Address = new Address()
            {
                AddressId = 456,
                AddressLine1 = "123 Acacia Avenue",
                AddressLine2 = "Somewhere"
            }
        };

        var conf = new MapperConfiguration()
        .SetAutoRegisterTypes(true)
        .RegisterResolvers<DocumentMemberResolver>()
        .RegisterOperator<MemberwiseDocumentValueMapperOperator>();
        var mapper = new ObjectMapper(conf);
        var doc = mapper.Map<CustomerWithNestedObject, DictionaryDocument>(cust);

        if (doc is not null)
        {
            Assert.Equal(123, doc["CustomerId"].AsInt32);
            //Assert.Equal("123 Acacia Avenue", doc["CustomerId"].AsDocument["AddressLine1"]);
        }
    }

    [Fact]
    public void TestDocumentToClassWithNestedClass()
    {
        DictionaryDocument doc = new DictionaryDocument();
        DictionaryDocument add = new DictionaryDocument();

        doc.Add("CustomerId", 123);
        doc.Add("CustomerName", "Acme Enterprises Ltd");
        add.Add("AddressId", 456);
        add.Add("AddressLine1", "123 Acacia Avenue");
        add.Add("AddressLine2", "Somewhere");
        doc.Add("Address", add);

        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>();

        var mapper = new ObjectMapper(conf);
        var operation = mapper.GetMapperOperator<DictionaryDocument, CustomerWithNestedObject>();
        var s = operation.PrettyPrint();
        var cust = mapper.Map<DictionaryDocument, CustomerWithNestedObject>(doc);
        Assert.NotNull(cust);
        if (cust is not null)
        {
            Assert.Equal(456, cust.Address.AddressId);
        }
    }

    [Fact]
    public void TestArrayIntsToDocument()
    {
        var intArray = new int[] { 1, 2, 3, 4, 5 };
        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>();
        var mapper = new ObjectMapper(conf);
        var doc = mapper.Map<int[], DocumentValue>(intArray);
        Assert.NotNull(doc);
        if (doc is not null)
        {
            Assert.Equal(DocumentType.Array, doc.Type);
        }
    }
}