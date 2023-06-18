/* ------------------------------------------------------
    Name: C# XML Comments to Markdown
    Purpose: Converts C# XML comments file to markdown.
    Notes: Based on https://gist.github.com/lontivero/593fc51f1208555112e0

    This script only documents from the XML file. It does not merge
    data from the assembly itself (via reflection). Therefore
    some things cannot be rendered easily:
    - Return types from methods
    - Full signature definition    

    Other libraries worth investigating:
    - https://github.com/lijunle/Vsxmd/blob/master/Vsxmd/Program.cs
    - https://stackoverflow.com/questions/1312166/print-full-signature-of-a-method-from-a-methodinfo
    
    Member ID strings
    - These need to be parsed & transformed to make more readable:
    - https://ewsoftware.github.io/XMLCommentsGuide/html/ee5d612e-914f-411f-bd95-23478b15e4de.htm
    - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/
   ------------------------------------------------------ */

#r "System.Xml.XDocument"
#r "System.Text.RegularExpressions"
#r "System.Console"

using System;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Dynamic;

Main(Args.ToArray());

static void Main(string[] args)
{
    Console.WriteLine("Begin: Document.csx...");
    var xmlFilePath = args[0];
    var mdFilePath = args[1];
    XmlToMarkdown(xmlFilePath, mdFilePath);
}
static void XmlToMarkdown(string xmlFilePath, string mdFilePath)
{
    Console.WriteLine(xmlFilePath);
    var xml = xmlFilePath;
    var doc = XDocument.Load(xml);
    var md = doc.Root.ToMarkDown();
    if (File.Exists(mdFilePath))
    {
        File.Delete(mdFilePath);
    }
    File.WriteAllText(mdFilePath, md);
}

static var fxNameAndText = new Func<string, XElement, IDictionary<string, object>>((att, node) => new Dictionary<string, object> {
                    {"Name", node.Attribute(att).Value},
                    {"Text", node.Nodes().ToMarkDown()}
});

static var fxIdAndText = new Func<string, XElement, IDictionary<string, object>>((att, node) => new Dictionary<string, object> {
                    {"IdParts", new IdParts(node.Attribute(att).Value)},
                    {"Text", node.Nodes().ToMarkDown()}
});

static var templates = new Dictionary<string, Func<dynamic, string>>  {

                    // Document / Assembly
                    {"doc", (model) =>
$@"<a id='top'></a>
# Assembly: {model.assembly}
## Contents
{model.toc}

{model.members}"},

                    // Type
                    {"type", (model) =>
$@"
---
## {model.IdParts.FullyQualifiedName}
### Namespace:
`{model.IdParts.Namespace}`
### Summary:
{((model.Text == null || model.Text.Trim() == "") ? "TBD" : model.Text)}
"},

                    // Field
                    {"field", (model) =>
$@">### {model.IdParts.MemberType}: {model.IdParts.Name}
#### Summary
{model.summary}

<small>[Back to top](#top)</small>
"},
                    
                    // Property
                    {"property", (model) =>
$@">### {model.IdParts.MemberType}: {model.IdParts.Name}
#### Summary
{model.summary}

<small>[Back to top](#top)</small>
"},
                    // Method
                    {"method", (model) =>
$@">### <a id='{model.IdParts.FullyQualifiedNameLink}'></a>{model.IdParts.MemberType}: {model.IdParts.Name}
#### Signature
{model.signature}
#### Summary
{model.summary}
#### Type Parameters:
{model.typeparametersHeader}
{model.typeparameters}
#### Parameters:
{model.paramHeader}
{model.parameters}
#### Exceptions:
{model.exceptions}
#### Examples:
{model.examples}

<small>[Back to top](#top)</small>
"},
                    {"event", (model) => $"### {model.Name}\n{model.Text}\n---\n"},
                    {"summary", (model) => $"{model.Text}\n"},
                    {"remarks", (model) => $"\n>{model.Name}\n"},
                    {"example", (model) => $"\n{model.Text}\n"},
                    {"see", (model) => $"[{model.IdParts.Name}](#{model.IdParts.FullyQualifiedNameLink})"},
                    {"param", (model) => $"|{model.Name}: |{model.Text}|\n" },
                    {"typeparam", (model) => $"|{model.Name}: |{model.Text}|\n" },
                    {"exception", (model) => $"\nException thrown: [{model.Name}](#{model.Name}): {model.Text}\n" },
                    {"returns", (model) => $"Returns: {model.Text}\n"},
                    {"none", (model) => ""}  };

static var methods = new Dictionary<string, Func<XElement, IDictionary<string, object>>>
                {
                    {"doc", x=> new Dictionary<string, object> {
                        {"assembly", x.Element("assembly").Element("name").Value},
                        {"members", x.Element("members").Elements("member").ToMarkDown()},
                        {"toc", x.Element("members").Elements("member")
                        .Select(toc => new IdParts(toc.Attribute("name").Value))
                        //.Where(toc => toc.MemberType == MemberType.type)
                        .Select(toc => (toc.MemberType==MemberType.type ? "- " : "  - ") + $"[{toc.Name}](#{toc.FullyQualifiedNameLink})\n")
                        .Aggregate("", (current, next) => current + "" + next)}
                    }},
                    {"type", x=> fxIdAndText("name", x)},

                    //{"field", x=> fxNameAndText("name", x)},
                    {"field", x=> new Dictionary<string, object> {
                        {"IdParts", new IdParts(x.Attribute("name").Value)},
                        {"summary", x.Elements("summary").ToMarkDown()}
                    }},

                    //{"property", x=> fxNameAndText("name", x)},
                    {"property", x=> new Dictionary<string, object> {
                        {"IdParts", new IdParts(x.Attribute("name").Value)},
                        {"summary", x.Elements("summary").ToMarkDown()}
                    }},

                    //{"method",x=>d("name", x)},
                    {"method", x=> new Dictionary<string, object>{
                        {"IdParts", new IdParts(x.Attribute("name").Value)},
                        {"signature", CreateSignature(x.Attribute("name").Value, x.Elements("typeparam").ToList(), x.Elements("param").ToList())},
                        {"summary", x.Elements("summary").ToMarkDown()},
                        {"typeparametersHeader", x.Elements("typeparam").Any() ? "|Param | Description |\n|-----|-----|" : "None"},
                        {"typeparameters", x.Elements("typeparam").Any() ? x.Elements("typeparam").ToMarkDown() : ""},
                        {"paramHeader", x.Elements("param").Any() ? "|Name | Description |\n|-----|------|" : "None"},
                        {"parameters", x.Elements("param").Any() ? x.Elements("param").ToMarkDown() : ""},
                        {"exceptions", (x.Element("exception") != null) ? x.Element("exception").ToMarkDown() : "None"},
                        {"examples", x.Elements("example").Any() ? $"\n#### Examples:\n{x.Elements("example").ToMarkDown()}\n" : "None"},
                    }},
                    {"event", x=>fxNameAndText("name", x)},
                    {"summary", x=> new Dictionary<string, object> {{"Text", x.Nodes().ToMarkDown()}}},
                    {"remarks", x => new Dictionary<string, object> {{"Text", x.Nodes().ToMarkDown()}}},
                    {"example", x => new Dictionary<string, object> {{"Text", x.ToCodeBlock()}}},
                    {"see", x=> {
                        var nameAndText = fxNameAndText("cref", x);
                        nameAndText["IdParts"] = new IdParts(nameAndText["Name"].ToString());
                        return nameAndText;
                    }},
                    {"param", x => fxNameAndText("name", x) },
                    {"typeparam", x => fxNameAndText("name", x) },
                    {"exception", x => fxNameAndText("cref", x) },
                    {"returns", x => new Dictionary<string, object> {{"Text", x.Nodes().ToMarkDown()}}},
                    {"none", x => new Dictionary<string, object> {}}
                };


/// <summary>
/// The member type.
/// </summary>
internal enum MemberType : byte
{
    @namespace,
    type,
    field,
    property,
    method,
    @event,
    errorString
}


/// <summary>
/// Represents the parts making up a comment document id.
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>
internal class IdParts
{
    /// <summary>
    /// The full member id string. In format [MemberType]:[memberid]
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// The member type.
    /// </summary>
    public MemberType MemberType { get; set; } = MemberType.errorString;

    /// <summary>
    /// The id value after the initial ':' character.
    /// </summary>
    public string FullyQualifiedName { get; set; } = default!;

    /// <summary>
    /// Unique link tag based on fully qualified name. Used for generaing links for TOC etc.
    /// </summary>
    public string FullyQualifiedNameLink { get; set; } = default!;

    /// <summary>
    /// The number of generic type arguments on the parent type / class. 
    /// </summary>
    public int ParentTypeArguments { get; set; } = default!;

    /// <summary>
    /// The number of generic type arguments on the current member. 
    /// </summary>
    public int TypeArguments { get; set; } = default!;

    /// <summary>
    /// Arguments (if exist for methods and properties).
    /// </summary>
    public string Arguments { get; set; } = "";

    /// <summary>
    /// The namespace name.
    /// </summary>
    public string Namespace { get; set; } = default!;

    /// <summary>
    /// The name of the parent.
    /// </summary>
    public string Parent { get; set; } = default!;

    /// <summary>
    /// The name of the member.
    /// </summary>
    public string Name { get; set; } = default!;

    public IdParts(string id)
    {
        Console.WriteLine($"Processing: {id}...");
        this.Id = id;
        var splits = id.Split(':');
        switch (splits[0])
        {
            case "N": this.MemberType = MemberType.@namespace; break;
            case "F": this.MemberType = MemberType.field; break;
            case "P": this.MemberType = MemberType.property; break;
            case "T": this.MemberType = MemberType.type; break;
            case "E": this.MemberType = MemberType.@event; break;
            case "M": this.MemberType = MemberType.method; break;
            case "!": this.MemberType = MemberType.errorString; break;
            default: this.MemberType = MemberType.errorString; break;
        }
        this.FullyQualifiedName = splits[1];
        this.FullyQualifiedNameLink = this.FullyQualifiedName.Replace(".", "").ToLower();
        var fqnParts = this.FullyQualifiedName.Split('(');  // look for first '('. Required for Methods and properties with arguments.
        if (fqnParts.Length == 2)
        {
            this.Arguments = fqnParts[1];
            this.Arguments = this.Arguments.Substring(0, this.Arguments.Length - 1);    // remove last ')'
        }
        var nameParts = fqnParts[0].Split('.');
        this.Name = nameParts[nameParts.Length - 1];

        // Check name for generics. (contains '``' characters)
        var nameGenericSplits = this.Name.Split("``");
        this.Name = nameGenericSplits[0];
        if (nameGenericSplits.Length == 2)
        {
            this.TypeArguments = int.Parse(nameGenericSplits[1]);
            switch (this.TypeArguments)
            {
                case 1:
                    this.Name += "<T>";
                    break;
                case 2:
                    this.Name += "<T, U>";
                    break;
                case 3:
                    this.Name += "<T, U, V>";
                    break;
                case 4:
                    this.Name += "<T, U, V, W>";
                    break;
            }
        }

        if ("FPEM".Contains(splits[0]))
        {
            // For fields, properties, events, methods, the
            // parent == containing type.
            this.Parent = nameParts[nameParts.Length - 2];
            this.Namespace = string.Join('.', nameParts.Take(nameParts.Length - 2));

            // Check type for generics (single '`' character)
            var parentGenericsSplits = this.Parent.Split('`');
            this.Parent = parentGenericsSplits[0];
            if (parentGenericsSplits.Length == 2)
            {
                this.ParentTypeArguments = int.Parse(parentGenericsSplits[1]);
                switch (this.ParentTypeArguments)
                {
                    case 1:
                        this.Parent += "<A>";
                        break;
                    case 2:
                        this.Parent += "<A, B>";
                        break;
                    case 3:
                        this.Parent += "<A, B, C>";
                        break;
                    case 4:
                        this.Parent += "<A, B, C, D>";
                        break;
                }
            }
        }
        this.Namespace = string.Join('.', nameParts.Take(nameParts.Length - 1));
    }
}

/// <summary>
/// Converts an IDictionary to a dynamic type.
/// </summary>
/// <param name="dict"></param>
/// <returns></returns>
internal static dynamic DictionaryToExpando(IDictionary<string, object> dict)
{
    var expando = new ExpandoObject();
    var expandoDict = (IDictionary<string, object>)expando;
    foreach (var kvp in dict)
    {
        if (kvp.Value is IDictionary<string, object>)
        {
            var expandoValue = DictionaryToExpando((IDictionary<string, object>)kvp.Value);
            expandoDict.Add(kvp.Key, expandoValue);
        }
        else if (kvp.Value is System.Collections.ICollection)
        {
            // iterate through the collection and convert any string-object dictionaries
            // along the way into expando objects
            var itemList = new List<object>();
            foreach (var item in (System.Collections.ICollection)kvp.Value)
            {
                if (item is IDictionary<string, object>)
                {
                    var expandoItem = DictionaryToExpando((IDictionary<string, object>)item);
                    itemList.Add(expandoItem);
                }
                else
                {
                    itemList.Add(item);
                }
            }
            expandoDict.Add(kvp.Key, itemList);
        }
        else
        {
            expandoDict.Add(kvp);
        }
    }
    return expando;
}

internal static string ToMarkDown(this XNode e)
{
    string name;
    if (e.NodeType == XmlNodeType.Element)
    {
        var el = (XElement)e;

        // Get the name of the element (or if the
        // element is a 'member' element, get the name attribute).
        name = el.Name.LocalName;

        if (name == "member")
        {
            var idParts = new IdParts(el.Attribute("name").Value);
            name = idParts.MemberType.ToString();
        }

        var model = DictionaryToExpando(methods[name](el));
        Console.WriteLine($"About to render template for [{name}]...");
        return templates[name](model);
    }

    if (e.NodeType == XmlNodeType.Text)
        return Regex.Replace(((XText)e).Value.Replace('\n', ' '), @"\s+", " ");

    return "";
}


internal static string ToMarkDown(this IEnumerable<XNode> es)
{
    if (es != null && es.Any())
    {
        return es.Aggregate("", (current, x) => current + x.ToMarkDown());
    }
    else
    {
        return "";
    }
}

static string ToCodeBlock(this XElement el)
{
    string s = "";
    foreach (var childNode in el.Nodes())
    {
        if (childNode.NodeType == XmlNodeType.Text)
        {
            s = s + $"\n{childNode.ToString().Trim()}\n";
        }
        else if (childNode.NodeType == XmlNodeType.Element && ((XElement)childNode).Name == "code")
        {
            var code = (childNode as XElement).Value;
            var lines = code.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var blank = lines[0].TakeWhile(x => x == ' ').Count() - 4;
            code = string.Join("\n", lines.Select(x => new string(x.SkipWhile((y, i) => i < blank).ToArray())));
            code = $"``` c#\n{code}\n```";
            s = s + code;
        }
    }
    return s;
}

/// <summary>
/// Creates a pseudo signature that can be displayed in documentation. Merges the member id string
/// and type parameters and parameters. Does not generate completely accurate signature, as some
/// information can only be derived by reflecting over the actual assembly (for example method
/// return types are not stored in the documentation - they have to be read from assembly's meta
/// data. Additionally, the scope of members is not defined).
/// </summary>
/// <param name="id"></param>
/// <param name="typeParameters"></param>
/// <param name="typeParameters"></param>
internal static string CreateSignature(string id, IList<XElement> typeParameters, IList<XElement> parameters)
{
    var idParts = new IdParts(id);
    List<string> typeParameterNames = typeParameters.Select(p => p.Attribute("name").Value).ToList();
    List<string> parameterNames = parameters.Select(p => p.Attribute("name").Value).ToList();

    // Type<T>.Method<U>(parm1type parm1name, ...)
    var arguments = idParts.Arguments;
    List<string> argumentTypes = new List<string>();
    if (arguments.Length > 0)
    {
        Console.WriteLine(arguments);
        Console.WriteLine("---------------------------");
        bool inField = false;
        string current = "";
        foreach (var c in arguments)
        {
            if (c == '[' || c == '{')
            {
                current += c;
                inField = true;
            }
            else if ((c == ']' || c == '}') && inField == true)
            {
                current += c;
                inField = false;
            }
            else if (inField == false && c == ',')
            {
                argumentTypes.Add(current);
                current = "";
            }
            else
            {
                current += c;
            }
        }
        argumentTypes.Add(current);
    }

    if (parameterNames.Count() != argumentTypes.Count())
    {
        throw new Exception($"Member: {idParts.Name}, Parameter count: {parameterNames.Count} != argument types: {argumentTypes.Count()}.");
    }

    // Finally, construct the signature:
    string signatureArgs = "";
    for (var i = 0; i < argumentTypes.Count(); i++)
    {
        signatureArgs += $"{argumentTypes[i]} {parameterNames[i]}, ";
    }
    if (signatureArgs.Length > 2)
    {
        signatureArgs = signatureArgs.Substring(0, signatureArgs.Length - 2);
    }
    signatureArgs = signatureArgs.Replace("{", "<").Replace("}", ">");
    for (var i = 0; i < typeParameterNames.Count(); i++)
    {
        signatureArgs = signatureArgs.Replace($"``{i}", typeParameterNames[i]);
    }
    return $"``` c#\n{idParts.Parent}.{idParts.Name}({signatureArgs})\n```";

}