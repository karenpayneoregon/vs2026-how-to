using ConsoleConfigurationLibrary.Models;
using ExtensionsLibrary.Classes;
using PropertyExistsApp.Classes;
using PropertyExistsApp.Classes.Core;
using Spectre.Console;

namespace PropertyExistsApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        if (JsonUtilities.PropertyExists("HelpDesk","Phone"))
        {
            var helpDesk = AppConfiguration.Instance.HelpDesk;
            Console.WriteLine(helpDesk.Phone);
        }
        else
        {
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "Property 'Phone' does not exist in section 'HelpDesk'.");
        }

        if (JsonUtilities.AllPropertiesExist("ConnectionStrings", ["MainConnection", "SecondaryConnection"]))
        {
            Console.WriteLine($"All properties exists for {nameof(ConnectionStrings)} ");
        }
        else
        {
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "One or more properties do not exist in section 'ConnectionStrings'.");
        }


        if (JsonUtilities.AllPropertiesExist("ConnectionStrings", ["MainConnection", "Secondary_Connection"]))
        {
            Console.WriteLine($"All properties exists for {nameof(ConnectionStrings)} ");
        }
        else
        {
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "One or more properties do not exist in section 'ConnectionStrings'.");
        }


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

}
