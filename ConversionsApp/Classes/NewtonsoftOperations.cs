using Newtonsoft.Json;
using System.Xml.Linq;
using File = System.IO.File;

namespace ConversionsApp.Classes;

internal class NewtonsoftOperations
{
    /// <summary>
    /// Converts a JSON file containing people data into an XML file.
    /// </summary>
    /// <remarks>
    /// This method reads a JSON file named <c>people.json</c>, converts its content into an XML format,
    /// and saves the resulting XML to a file named <c>people1.xml</c>. If the JSON content starts with an array,
    /// it wraps the array in an object with a <c>Person</c> property to ensure valid XML structure.
    ///
    /// Try passing both people.json and people1.json to see the same output in the XML file.
    /// 
    /// </remarks>
    public static void PeopleJsonToXml()
    {
        var fileName = "people1.json";
        var json = File.ReadAllText(fileName);
        
        if (json.TrimStart().StartsWith("["))
            json = $"{{ \"Person\": {json} }}";

        XDocument xmlNode = JsonConvert.DeserializeXNode(json, "People")!;
        xmlNode.Declaration = new XDeclaration("1.0", null, "no");

        var xmlFileName = "people1.xml";
        xmlNode.Save(xmlFileName);
    }
    
}
