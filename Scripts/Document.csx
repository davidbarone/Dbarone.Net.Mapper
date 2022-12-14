/* ------------------------------------------------------
   https://gist.github.com/lontivero/593fc51f1208555112e0
   ------------------------------------------------------
*/

#r "System.Xml.XDocument"
#r "System.Text.RegularExpressions"
#r "System.Console"

using System;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;

var xmlFilePath = Args[0];
var mdFilePath = Args[1];
Main(xmlFilePath, mdFilePath);

static void Main(string xmlFilePath, string mdFilePath)
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

static var d = new Func<string, XElement, string[]>((att, node) => new[] {
                    node.Attribute(att).Value,
                    node.Nodes().ToMarkDown()
});

static var templates = new Dictionary<string, string>  {
                    {"doc", "# {0}\n{1}"},
                    {"type", "\n\n>## {0}\n\n{1}\n---\n"},
                    {"field", "### {0}\n{1}\n---\n"},
                    {"property", "### {0}\n{1}\n---\n"},
                    {"method", "### {0}\n{1}{2}{3}{4}\n---\n"},
                    {"event", "### {0}\n{1}\n---\n"},
                    {"summary", "{0}\n"},
                    {"remarks", "\n>{0}\n"},
                    {"example", "_C# code_\n```c#\n{0}\n```\n"},
                    {"seePage", "[[{1}|{0}]]"},
                    {"seeAnchor", "[{1}]({0})"},
                    {"param", "|{0}: |{1}|\n" },
                    {"typeparam", "|{0}: |{1}|\n" },
                    {"exception", "\nException thrown: [{0}](#{0}): {1}\n" },
                    {"returns", "Returns: {0}\n"},
                    {"none", ""}  };

static var methods = new Dictionary<string, Func<XElement, IEnumerable<string>>>
                {
                    {"doc", x=> new[]{
                        x.Element("assembly").Element("name").Value,
                        x.Element("members").Elements("member").ToMarkDown()
                    }},
                    {"type", x=>d("name", x)},
                    {"field", x=> d("name", x)},
                    {"property", x=> d("name", x)},
                    //{"method",x=>d("name", x)},
                    {"method", x=> new[]{
                        x.Attribute("name").Value,
                        x.Elements("summary").ToMarkDown(),
                        "|Name | Description |\n|-----|------|\n",
                        x.Elements("param").Any() ? x.Elements("param").ToMarkDown() : "",
                        (x.Element("exception") != null) ? x.Element("exception").ToMarkDown() : ""
                    }},
                    {"event", x=>d("name", x)},
                    {"summary", x=> new[]{ x.Nodes().ToMarkDown() }},
                    {"remarks", x => new[]{x.Nodes().ToMarkDown()}},
                    {"example", x => new[]{x.Value.ToCodeBlock()}},
                    {"seePage", x=> d("cref", x) },
                    {"seeAnchor", x=> { var xx = d("cref", x); xx[0] = xx[0].ToLower(); return xx; }},
                    {"param", x => d("name", x) },
                    {"typeparam", x => d("name", x) },
                    {"exception", x => d("cref", x) },
                    {"returns", x => new[]{x.Nodes().ToMarkDown()}},
                    {"none", x => new string[0]}
                };

internal static string ToMarkDown(this XNode e)
{
    string name;
    if (e.NodeType == XmlNodeType.Element)
    {
        var el = (XElement)e;
        name = el.Name.LocalName;
        if (name == "member")
        {
            switch (el.Attribute("name").Value[0])
            {
                case 'F': name = "field"; break;
                case 'P': name = "property"; break;
                case 'T': name = "type"; break;
                case 'E': name = "event"; break;
                case 'M': name = "method"; break;
                default: name = "none"; break;
            }
        }
        if (name == "see")
        {
            var anchor = el.Attribute("cref").Value.StartsWith("!:#");
            name = anchor ? "seeAnchor" : "seePage";
        }
        var vals = methods[name](el).ToArray();
        string str = "";
        switch (vals.Length)
        {
            case 1: str = string.Format(templates[name], vals[0]); break;
            case 2: str = string.Format(templates[name], vals[0], vals[1]); break;
            case 3: str = string.Format(templates[name], vals[0], vals[1], vals[2]); break;
            case 4: str = string.Format(templates[name], vals[0], vals[1], vals[2], vals[3]); break;
            case 5: str = string.Format(templates[name], vals[0], vals[1], vals[2], vals[3], vals[4]); break;
        }

        return str;
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
    } else {
        return "";
    }
}

static string ToCodeBlock(this string s)
{
    var lines = s.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
    var blank = lines[0].TakeWhile(x => x == ' ').Count() - 4;
    return string.Join("\n", lines.Select(x => new string(x.SkipWhile((y, i) => i < blank).ToArray())));
}