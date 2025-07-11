using Dbarone.Net.Document;
using Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;
using System.Security.Cryptography.X509Certificates;

namespace Dbarone.Net.Mapper.Tests;

public class ComplexType
{
    public string StringValue { get; set; } = "FooBar";

    public byte ByteValue { get; set; } = 123;

    public DateTime DateTimeValue { get; set; } = DateTime.Now;

    public int IntValue { get; set; } = 123;

    public FooBarBazEnumType EnumValue { get; set; } = FooBarBazEnumType.Baz;

    public int? NullableIntValue { get; set; } = null;
}

public enum FooBarBazEnumType
{
    Foo,
    Bar,
    Baz
}

public class ClassWithEnum
{
    public FooBarBazEnumType FooBarBaz { get; set; } = FooBarBazEnumType.Foo;
}

public class ClassWithNullable
{
    public int? Age { get; set; } = null;
    public int? Value { get; set; } = 123;
}

public class ClassWithDate
{
    public DateTime Dob { get; set; }
}

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

public class DataDocumentTests
{
    [Fact]
    public void TestIntToDocumentUsesImplicitOperator()
    {
        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<ImplicitOperatorMapperOperator>();
        var mapper = new ObjectMapper(conf);
        var op = mapper.GetMapperOperator<int, DocumentValue>();
        Assert.Equal(typeof(ImplicitOperatorMapperOperator), op.GetType());
    }

    [Fact]
    public void TestIntToDocument()
    {
        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<ImplicitOperatorMapperOperator>();
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
        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<ImplicitOperatorMapperOperator>();
        var mapper = new ObjectMapper(conf);

        var doc = new DocumentValue(123);
        int a = mapper.Map<DocumentValue, int>(doc);
        Assert.Equal(123, a);
    }

    [Fact]
    public void TestDateToDocument()
    {
        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<ImplicitOperatorMapperOperator>();
        var mapper = new ObjectMapper(conf);

        DateTime dt = DateTime.Now;
        DocumentValue? doc = mapper.Map<DateTime, DocumentValue>(dt);

        Assert.NotNull(doc);
        if (doc is not null)
        {
            Assert.Equal(dt, doc.AsDateTime);
        }
    }

    [Fact]
    public void TestDocumentToDate()
    {
        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<ImplicitOperatorMapperOperator>();
        var mapper = new ObjectMapper(conf);

        var d = DateTime.Now;
        var doc = new DocumentValue(d);
        DateTime dt = mapper.Map<DocumentValue, DateTime>(doc);
        Assert.Equal(d, dt);
    }

    [Fact]
    public void TestClassToDocument()
    {
        Customer cust = new Customer()
        {
            CustomerId = 123,
            CustomerName = "Acme Enterprises Ltd"
        };

        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<ImplicitOperatorMapperOperator>()
            .RegisterOperator<MemberwiseMapperOperator>();
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

        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<ImplicitOperatorMapperOperator>()
            .RegisterOperator<MemberwiseMapperDeferBuildOperator>();
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
            .RegisterOperator<MemberwiseDocumentValueMapperOperator>()
            .RegisterOperator<ImplicitOperatorMapperOperator>()
            .RegisterOperator<MemberwiseMapperOperator>();
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
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<MemberwiseDocumentValueMapperOperator>()
            .RegisterOperator<ImplicitOperatorMapperOperator>()
            .RegisterOperator<MemberwiseMapperDeferBuildOperator>();
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
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<EnumerableDocumentValueMapperOperator>()
            .RegisterOperator<MemberwiseDocumentValueMapperOperator>()
            .RegisterOperator<ImplicitOperatorMapperOperator>()
            .RegisterOperator<EnumerableMapperOperator>();
        var mapper = new ObjectMapper(conf);
        var doc = mapper.Map<int[], DocumentValue>(intArray);
        Assert.NotNull(doc);
        if (doc is not null)
        {
            Assert.Equal(DocumentType.Array, doc.Type);
            Assert.Equal(1, doc.AsArray.RawValue.First().AsInt32);
        }
    }

    [Fact]
    public void TestClassWithDateToDocument1()
    {
        DateTime d = DateTime.Now;
        var obj = new ClassWithDate { Dob = d };

        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<EnumerableDocumentValueMapperOperator>()
            .RegisterOperator<ImplicitOperatorMapperOperator>()
            .RegisterOperator<MemberwiseMapperOperator>();
        var mapper = new ObjectMapper(conf);
        var op = mapper.GetMapperOperator<ClassWithDate, DictionaryDocument>();

        var doc = mapper.Map<ClassWithDate, DictionaryDocument>(obj);
        Assert.NotNull(doc);
        Assert.Equal(d, doc!["Dob"].AsDateTime);
    }

    [Fact]
    public void TestClassWithDateToDocument2()
    {
        DateTime d = DateTime.Now;
        var obj = new ClassWithDate { Dob = d };

        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<MemberwiseDocumentValueMapperOperator>()
            .RegisterOperator<ImplicitOperatorMapperOperator>()
            .RegisterOperator<MemberwiseMapperOperator>();
        var mapper = new ObjectMapper(conf);

        var doc = (DocumentValue)mapper.Map(typeof(DocumentValue), obj)!;
        Assert.NotNull(doc);
        Assert.Equal(d, doc!["Dob"].AsDateTime);
    }

    [Fact]
    public void TestArrayOfObjectsToDocument()
    {
        Customer cust = new Customer()
        {
            CustomerId = 123,
            CustomerName = "Acme Ltd"
        };
        List<Customer> customers = new List<Customer>();
        customers.Add(cust);

        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<MemberwiseDocumentValueMapperOperator>()
            .RegisterOperator<EnumerableDocumentValueMapperOperator>()
            .RegisterOperator<ImplicitOperatorMapperOperator>()
            .RegisterOperator<MemberwiseMapperOperator>();
        var mapper = new ObjectMapper(conf);
        var doc = mapper.Map<List<Customer>, DocumentValue>(customers);
        Assert.NotNull(doc);
        if (doc is not null)
        {
            Assert.Equal(DocumentType.Array, doc.Type);
            Assert.Single(doc.AsArray);
            Assert.Equal(123, doc.AsArray.RawValue.First().AsDocument["CustomerId"].AsInt32);
        }
    }

    [Fact]
    public void DocumentNullTest()
    {
        Customer a = new Customer();
        a.CustomerId = 123;
        a.CustomerName = null;

        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<AssignableMapperOperator>()
            .RegisterOperator<ObjectSourceMapperOperator>()
            .RegisterOperator<ImplicitOperatorMapperOperator>()
            .RegisterOperator<MemberwiseMapperDeferBuildOperator>()
            .RegisterOperator<MemberwiseMapperOperator>();
        var mapper = new ObjectMapper(conf);
        var s = mapper.GetMapperOperator<Customer, DictionaryDocument>().PrettyPrint();

        var b = mapper.Map<Customer, DictionaryDocument>(a);
        Assert.IsType<DictionaryDocument>(b);
        Assert.NotNull(b);
        Assert.Equal(123, (int)b["CustomerId"]);
        Assert.Equal(DocumentType.Null, b["CustomerName"].Type);

        // Convert back
        s = mapper.GetMapperOperator<DictionaryDocument, Customer>().PrettyPrint();

        var c = mapper.Map<DictionaryDocument, Customer>(b);
        Assert.NotNull(c);
        Assert.IsType<Customer>(c);
        Assert.Null(c.CustomerName);
        Assert.Equal(123, c.CustomerId);
    }

    [Fact]
    public void DocumentNullableTest()
    {
        ClassWithNullable a = new ClassWithNullable();
        var conf = new MapperConfiguration()
           .SetAutoRegisterTypes(true)
           .RegisterResolvers<DocumentMemberResolver>()
           .RegisterOperator<MemberwiseDocumentValueMapperOperator>()
           .RegisterOperator<EnumerableDocumentValueMapperOperator>()
           .RegisterOperator<NullableSourceMapperOperator>()
           .RegisterOperator<ImplicitOperatorMapperOperator>()
           .RegisterOperator<MemberwiseMapperOperator>();

        var mapper = new ObjectMapper(conf);
        var op = mapper.GetMapperOperator<ClassWithNullable, DictionaryDocument>();
        var str = op.PrettyPrint();

        // Map POCO to document, including null values;
        var b = mapper.Map<ClassWithNullable, DictionaryDocument>(a);
        Assert.IsType<DictionaryDocument>(b);
        Assert.Equal(DocumentType.Null, b["Age"].Type);
        Assert.Equal(123, b["Value"].AsInt32);

        // Map back to POCO
        var c = mapper.Map<DictionaryDocument, ClassWithNullable>(b);
        Assert.IsType<ClassWithNullable>(c);
        Assert.Null(c.Age);
        Assert.Equal(123, c.Value);
    }

    [Fact]
    public void DocumentWithEnumTest()
    {
        ClassWithEnum a = new ClassWithEnum();
        a.FooBarBaz = FooBarBazEnumType.Baz;

        var conf = new MapperConfiguration()
            .SetAutoRegisterTypes(true)
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<EnumSourceValueMapperOperator>()
            .RegisterOperator<EnumTargetValueMapperOperator>()
            .RegisterOperator<ImplicitOperatorMapperOperator>()
            .RegisterOperator<MemberwiseMapperDeferBuildOperator>()
            .RegisterOperator<MemberwiseMapperOperator>();

        var mapper = new ObjectMapper(conf);
        var op = mapper.GetMapperOperator<ClassWithEnum, DictionaryDocument>();
        var s = op.PrettyPrint();
        // Map POCO to document
        var b = mapper.Map<ClassWithEnum, DictionaryDocument>(a);
        Assert.IsType<DictionaryDocument>(b);

        // Map back to POCO
        op = mapper.GetMapperOperator<DictionaryDocument, ClassWithEnum>();
        s = op.PrettyPrint();
        var c = mapper.Map<DictionaryDocument, ClassWithEnum>(b);
        Assert.IsType<ClassWithEnum>(c);
        Assert.Equal(FooBarBazEnumType.Baz, c.FooBarBaz);
    }

    [Fact]
    public void TestComplexDocument()
    {
        var complexValue = new ComplexType();

        var conf = new MapperConfiguration()
           .SetAutoRegisterTypes(true)
           .RegisterResolvers<DocumentMemberResolver>()
           .RegisterOperator<MemberwiseDocumentValueMapperOperator>()
           .RegisterOperator<NullableSourceMapperOperator>()
           .RegisterOperator<EnumSourceValueMapperOperator>()
           .RegisterOperator<EnumTargetValueMapperOperator>()
           .RegisterOperator<AssignableMapperOperator>()
           .RegisterOperator<ObjectSourceMapperOperator>()
           .RegisterOperator<ImplicitOperatorMapperOperator>()
           .RegisterOperator<MemberwiseMapperDeferBuildOperator>()
           .RegisterOperator<MemberwiseMapperOperator>();
        var mapper = new ObjectMapper(conf);
        var op = mapper.GetMapperOperator<ComplexType, DictionaryDocument>();

        // Map POCO to document
        var b = mapper.Map<ComplexType, DictionaryDocument>(complexValue);
        Assert.IsType<DictionaryDocument>(b);

        // Serialize + deserialize:
        DocumentSerializer ser = new DocumentSerializer();
        var bytes = ser.Serialize(b);
        var c = (DictionaryDocument)ser.Deserialize(bytes);

        // Map back to POCO
        var d = mapper.Map(typeof(ComplexType), c);
        Assert.IsType<ComplexType>(d);
    }
}