using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

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
        foreach (var child in element.EnumerateObject()
                     .Select(property => new XElement(property.Name, property.Value.ToString()))) parent.Add(child);
    }

    public static string ToXmlStacked(string json, string rootElementName, string itemElementName)
    {
        using JsonDocument document = JsonDocument.Parse(json);

        XElement root = new(rootElementName);

        if (document.RootElement.ValueKind != JsonValueKind.Array)
            throw new JsonException("Expected the JSON root element to be an array.");

        foreach (JsonElement item in document.RootElement.EnumerateArray())
        {
            XElement itemElement = new(itemElementName);

            AddObjectProperties(itemElement, item);

            root.Add(itemElement);
        }

        XDocument xmlDocument = new(new XDeclaration("1.0", "utf-8", null), root);

        return xmlDocument.ToString();
    }

    private static void AddObjectProperties(XElement parent, JsonElement jsonObject)
    {
        foreach (JsonProperty property in jsonObject.EnumerateObject())
        {
            AddElement(parent, property.Name, property.Value);
        }
    }

    private static void AddElement(XElement parent, string elementName, JsonElement value)
    {
        switch (value.ValueKind)
        {
            case JsonValueKind.Object:
                {
                    XElement child = new(elementName);

                    AddObjectProperties(child, value);

                    parent.Add(child);
                    break;
                }

            case JsonValueKind.Array:
                {
                    XElement array = new(elementName);

                    foreach (JsonElement item in value.EnumerateArray())
                    {
                        AddElement(array, "Item", item);
                    }

                    parent.Add(array);
                    break;
                }

            case JsonValueKind.String:
                parent.Add(new XElement(elementName, value.GetString()));
                break;

            case JsonValueKind.Number:
                parent.Add(new XElement(elementName, value.GetRawText()));
                break;

            case JsonValueKind.True:
            case JsonValueKind.False:
                parent.Add(new XElement(elementName, value.GetBoolean()));
                break;

            case JsonValueKind.Null:
            case JsonValueKind.Undefined:
                parent.Add(new XElement(elementName));
                break;
        }
    }

    public static string ConvertXmlToJson<T>(string xmlString)
    {
        // 1. Deserialize XML to C# Object
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        using StringReader stringReader = new StringReader(xmlString);
        T obj = (T)xmlSerializer.Deserialize(stringReader)!;

        // 2. Serialize C# Object to JSON using System.Text.Json
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(obj, options);
    }

    /// <summary>
    /// Converts a JSON string to its XML representation.
    /// </summary>
    /// <param name="json">The JSON string to convert. The root element of the JSON must be an array.</param>
    /// <param name="rootElementName">The name of the root element in the resulting XML document.</param>
    /// <param name="itemElementName">The name of each item element in the resulting XML document.</param>
    /// <returns>A string containing the XML representation of the provided JSON.</returns>
    /// <exception cref="JsonException">Thrown when the root element of the JSON is not an array.</exception>
    /// <remarks>
    /// This method parses a JSON string and converts it into an XML document. 
    /// It assumes that the JSON root element is an array and creates an XML structure 
    /// with a specified root element and item element names.
    /// </remarks>
    public static void ConvertJsonToXml(string json, string outputFilePath, string rootElementName, string itemElementName)
    {
        using JsonDocument document = JsonDocument.Parse(json);

        XElement root = new(rootElementName);

        if (document.RootElement.ValueKind != JsonValueKind.Array)
        {
            throw new JsonException(
                "Expected the JSON root element to be an array.");
        }

        foreach (JsonElement item in document.RootElement.EnumerateArray())
        {
            XElement itemElement = new(itemElementName);

            AddObjectProperties(itemElement, item);

            root.Add(itemElement);
        }

        XDocument xmlDocument = new(new XDeclaration("1.0", "utf-8", null), root);

        xmlDocument.Save(outputFilePath);
    }

}
