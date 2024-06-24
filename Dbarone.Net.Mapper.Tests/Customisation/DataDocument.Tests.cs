using Dbarone.Net.Document;
using Dbarone.Net.Mapper;
using System.Reflection;
using System.Linq.Expressions;
using Dbarone.Net.Extensions;
using System.Security.Cryptography.X509Certificates;

namespace Dbarone.Net.Mapper.Tests;

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
    public void TestDateToDocument()
    {
        var conf = new MapperConfiguration().SetAutoRegisterTypes(true).RegisterResolvers<DocumentMemberResolver>();
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
        var d = DateTime.Now;
        var conf = new MapperConfiguration().SetAutoRegisterTypes(true).RegisterResolvers<DocumentMemberResolver>();
        var mapper = new ObjectMapper(conf);
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
            .RegisterResolvers<DocumentMemberResolver>()
            .RegisterOperator<EnumerableDocumentValueMapperOperator>()
            .RegisterOperator<MemberwiseDocumentValueMapperOperator>();

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
            .RegisterOperator<MemberwiseDocumentValueMapperOperator>();

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
            .RegisterOperator<EnumerableDocumentValueMapperOperator>()
            .RegisterOperator<MemberwiseDocumentValueMapperOperator>();

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
            .RegisterOperator<EnumerableDocumentValueMapperOperator>()
            .RegisterOperator<MemberwiseDocumentValueMapperOperator>();

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
}