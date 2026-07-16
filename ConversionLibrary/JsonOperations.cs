using System.Text.Json;
using System.Xml.Linq;

namespace ConversionLibrary;

/// <summary>
/// Provides operations for converting JSON data to other formats.
/// </summary>
/// <remarks>
/// This class includes methods to facilitate the transformation of JSON strings into XML representations.
/// It is designed to handle JSON data where the root element is an array.
/// </remarks>
public class JsonOperations
{
    /// <summary>
    /// Converts a JSON string to an XML representation.
    /// </summary>
    /// <param name="json">The JSON string to be converted. The root element of the JSON must be an array.</param>
    /// <param name="rootElementName">The name of the root element in the resulting XML.</param>
    /// <param name="itemElementName">The name of each item element in the resulting XML.</param>
    /// <returns>A string containing the XML representation of the provided JSON.</returns>
    /// <exception cref="ArgumentException">Thrown when the root element of the JSON is not an array.</exception>
    public static string ToXml(string json, string rootElementName, string itemElementName)
    {
        
        using var doc = JsonDocument.Parse(json);
        XElement root = new(rootElementName);

        if (doc.RootElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var element in doc.RootElement.EnumerateArray())
            {
                var item = new XElement(itemElementName);
                ParseJsonElement(element, item);
                root.Add(item);
            }
        }
        else
        {
            throw new ArgumentException("JSON root must be an array.");
        }

        var document = new XDocument(root);
        var declaration = new XDeclaration("1.0", null, "no");

        return $"{declaration}{Environment.NewLine}{root}";
        
    }


    /// <summary>
    /// Parses a <see cref="System.Text.Json.JsonElement"/> and appends its content to the specified <see cref="System.Xml.Linq.XElement"/>.
    /// </summary>
    /// <param name="element">The JSON element to parse.</param>
    /// <param name="parent">The XML element to which the parsed content will be added.</param>
    private static void ParseJsonElement(JsonElement element, XElement parent)
    {
        foreach (var child in element.EnumerateObject().Select(property => new XElement(property.Name, property.Value.ToString())))
        {
            parent.Add(child);
        }
    }
}
