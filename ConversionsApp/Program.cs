using ConversionLibrary;
using ConversionsApp.Classes.Core;
using Spectre.Console;
using System.Xml;
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
        SpectreConsoleHelpers.ExitPrompt();
    }

    private static void ProcessAndDisplayPeopleData()
    {
        var fileName = "people.json";

        string xml = JsonOperations.ToXmlStacked(File.ReadAllText(fileName), "People", "Person");
        File.WriteAllText("People.xml", xml);

        
        People people = PeopleXmlReader.Read("people.xml");
        
        foreach (var person in people.Persons)
        {
            Console.WriteLine($"{person.Id}: {person.FirstName} {person.LastName}");
            Console.WriteLine($"    {person.Address.Street}, {person.Address.City} {person.Address.Postcode}");
        }
    }

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
