using ConversionLibrary;
using ConversionsApp.Classes.Core;
using System.Xml.Linq;
using ConversionsApp.Classes;
using ConversionsApp.Models;

namespace ConversionsApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        ProcessAndDisplayPeopleData();
        ConvertJsonToXmlAndProcessProducts();
        NewtonsoftOperations.PeopleJsonToXml();
        
        SpectreConsoleHelpers.ExitPrompt();
    }

    /// <summary>
    /// Processes and displays data about people by reading from a JSON file, converting it to XML,
    /// and displaying the information in the console.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations:
    /// 1. Reads a JSON file containing people data.
    /// 2. Converts the JSON data into an XML format and saves it to a file.
    /// 3. Reads the XML file to deserialize it into a <see cref="People"/> object.
    /// 4. Iterates through the deserialized data and displays each person's details in the console.
    /// 5. Converts the JSON data to another XML file using a secondary conversion method.
    /// </remarks>
    private static void ProcessAndDisplayPeopleData()
    {
        var fileName = "people.json";
        var json = File.ReadAllText(fileName);

        string xml = JsonOperations.ToXmlStacked(File.ReadAllText(fileName), "People", "Person");
        File.WriteAllText("People.xml", xml);

        
        People people = PeopleXmlReader.Read("people.xml");
        
        foreach (var person in people.Persons)
        {
            Console.WriteLine($"{person.Id}: {person.FirstName} {person.LastName}");
            Console.WriteLine($"    {person.Address.Street}, {person.Address.City} {person.Address.Postcode}");
        }

        JsonOperations.ConvertJsonToXml(json, "People_Converted.xml", "People", "Person"); 

    }

    /// <summary>
    /// Converts a JSON file containing product data to an XML file and processes the products.
    /// </summary>
    /// <remarks>
    /// This method reads a JSON file named <c>Products.json</c>, converts its content to an XML format,
    /// and writes the result to a file named <c>Products.xml</c>. It then parses the XML file to create
    /// a list of <see cref="Product"/> objects and displays a completion message.
    /// </remarks>
    /// <seealso cref="JsonOperations.ToXml(string, string, string)"/>
    /// <seealso cref="Product"/>
    private static void ConvertJsonToXmlAndProcessProducts()
    {
        var fileName = "Products.json";

        string xml = JsonOperations.ToXml(File.ReadAllText(fileName), "Products", "Product");
        File.WriteAllText("Products.xml", xml);


        XDocument doc = XDocument.Load("Products.xml");
        List<Product> products =
        [
            .. doc.Descendants("Product")
                .Select(p =>
                    new Product(
                        p.Element("ProductId")?.Value.ToInt(),
                        p.Element("ProductName")?.Value!,
                        p.Element("UnitsInStock")?.Value.ToInt(),
                        p.Element("UnitPrice")?.Value.ToDecimal(),
                        p.Element("CategoryId")?.Value.ToInt()))
        ];



        WindowHelpers.CenterLines("[white]Done[/]", "Inspect [cyan]Products.xml[/] in the executable folder");
    }
}
