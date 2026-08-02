using System.Runtime.CompilerServices;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace ModuleInitializersApp2.Classes;

public class Helpers
{
    [ModuleInitializer]
    public static void Initialize()
    {
        
        SpectreConsoleHelpers.SetEncoding();
        
        AnsiConsole.MarkupLine(":collision: [DeepPink1]Application is starting...[/]");
        SpectreConsoleHelpers.PinkPill(Justify.Left,
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
                ? "Application is running in Development mode."
                : "Application is running in Production mode.");
    }
}