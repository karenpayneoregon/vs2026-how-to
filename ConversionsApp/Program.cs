using ConversionLibrary;
using ConversionsApp.Classes.Core;
using Spectre.Console;

namespace ConversionsApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        
        var fileName = "Products.json";

        string xml = JsonOperations.ToXml(File.ReadAllText(fileName), "Products", "Products");
        File.WriteAllText("Products.xml", xml);
        
        WindowHelpers.CenterLines("[white]Done[/]", "Inspect [cyan]Products.xml[/] in the executable folder");

        SpectreConsoleHelpers.ExitPrompt();
    }

}
