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
$@"# Assembly: {model.assembly}
## Contents
{model.toc}

{model.members}"},

                    // Type
                    {"type", (model) =>
$@"
---
## {model.IdParts.Name}
### Namespace:
`{model.IdParts.Namespace}`
### Summary:
{((model.Text == null || model.Text.Trim() == "") ? "TBD" : model.Text)}
"},

                    // Field
                    {"field", (model) =>
$@"> ### {model.IdParts.MemberType}: {model.IdParts.Name}
<small>id: `{model.IdParts.Id}`</small>

#### Summary
{model.summary}
"},
                    
                    // Property
                    {"property", (model) =>
$@"> ### {model.IdParts.MemberType}: {model.IdParts.Name}
<small>id: `{model.IdParts.Id}`</small>

#### Summary
{model.summary}
"},
                    // Method
                    {"method", (model) =>
$@"> ### {model.IdParts.MemberType}: {model.IdParts.Name}
<small>id: `{model.IdParts.Id}`</small>

#### Summary
{model.summary}
#### Type Parameters:
{model.typeparametersHeader}
{model.typeparameters}
#### Parameters:
{model.paramHeader}
{model.parameters}
#### Exceptions Thrown:
{model.exceptions}
#### Examples:
{model.examples}
"},
                    {"event", (model) => $"### {model.Name}\n{model.Text}\n---\n"},
                    {"summary", (model) => $"{model.Text}\n"},
                    {"remarks", (model) => $"\n>{model.Name}\n"},
                    {"example", (model) => $"\n{model.Text}\n"},
                    {"seePage", (model) => $"[[{model.Text}|{model.Name}]]"},
                    {"seeAnchor", (model) => $"[{model.Text}]({model.Name})"},
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
                        .Where(toc => toc.MemberType == MemberType.type)
                        .Select(toc => $"- [{toc.FullyQualifiedName}](#{toc.FullyQualifiedNameLink})\n")
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
                    {"seePage", x=> fxNameAndText("cref", x) },
                    {"seeAnchor", x=> {
                        var xx = fxNameAndText("cref", x);
                        xx["Name"] = xx["Name"].ToString().ToLower(); return new Dictionary<string, object> {{"Text", xx}}; }},
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
    public string Id { get; set; }

    /// <summary>
    /// The member name
    /// </summary>
    public MemberType MemberType { get; set; }
    public string FullyQualifiedName { get; set; }

    /// <summary>
    /// Unique link tag based on fully qualified name. Used for generaing links for TOC etc.
    /// </summary>
    public string FullyQualifiedNameLink { get; set; }

    /// <summary>
    /// Arguments (if exist for methods and properties).
    /// </summary>
    public string Arguments { get; set; }

    /// <summary>
    /// The namespace name.
    /// </summary>
    public string Namespace { get; set; }

    /// <summary>
    /// The name of the parent.
    /// </summary>
    public string Parent { get; set; }

    /// <summary>
    /// The name of the member.
    /// </summary>
    public string Name { get; set; }

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
        if ("FPEM".Contains(splits[0]))
        {
            this.Parent = nameParts[nameParts.Length - 2];
            this.Namespace = string.Join('.', nameParts.Take(nameParts.Length - 2));
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
        if (name == "see")
        {
            var anchor = el.Attribute("cref").Value.StartsWith("!:#");
            name = anchor ? "seeAnchor" : "seePage";
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