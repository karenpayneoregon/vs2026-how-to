using MemoryCollectionAppsettingsSample.Classes;
using MemoryCollectionAppsettingsSample.Classes.Core;
using Spectre.Console;

namespace MemoryCollectionAppsettingsSample;

internal partial class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[cyan]From InMemoryCollection[/]");
        AnsiConsole.MarkupLine($"Help desk email [green bold]{AppConfiguration.Instance.HelpDesk.Email}[/]");
        AnsiConsole.MarkupLine($"Help desk phone [green bold]{AppConfiguration.Instance.HelpDesk.Email}[/]");
        AnsiConsole.MarkupLine("[cyan]In appsettings.json[/]");
        AnsiConsole.MarkupLine($"Connection [green bold]{AppConfiguration.Instance.MainConnection};[/]");
        Console.WriteLine();
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

}
