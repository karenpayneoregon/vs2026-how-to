using Spectre.Console;
using SpectreConsoleLibrary.Core;
using System.Configuration;
using System.Globalization;
using PartialExamples = ExperimentsApp.Classes.PartialExamples;

namespace ExperimentsApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        //DisplayItemDetails();

        DisplayCommaDelimitedMonths();


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Displays a comma-delimited list of full month names in the console.
    /// </summary>
    /// <remarks>
    /// This method retrieves the full month names from the current culture's <see cref="DateTimeFormatInfo"/> 
    /// and formats them as a comma-delimited string. The output is displayed using both Spectre.Console markup 
    /// and standard console output.
    /// </remarks>
    private static void DisplayCommaDelimitedMonths()
    {
        AnsiConsole.MarkupLine("[yellow]Comma delimited full month names[/]");
        CommaDelimitedStringCollection months = [];

        months.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames[..^1]);
        Console.WriteLine($"\t{months}");
    }

    /// <summary>
    /// Displays detailed information about an instance of <see cref="ExperimentsApp.Classes.PartialExamples"/> 
    /// in the console using Spectre.Console helpers.
    /// </summary>
    /// <remarks>
    /// This method demonstrates the usage of various Spectre.Console helpers to display formatted 
    /// information, including capacity, indexed items, and attempts to retrieve items at specific indices.
    /// </remarks>
    private static void DisplayItemDetails()
    {

        SpectreConsoleHelpers.PrintPink();
        PartialExamples item = new();
        AnsiConsole.MarkupLine($"[green]Capacity:[/] {item.Capacity}");
        AnsiConsole.WriteLine();
        SpectreConsoleHelpers.InfoPill(Justify.Left, "Item capacity is now: " + item.Capacity);
        SpectreConsoleHelpers.SuccessPill(Justify.Left, $"Item at index 0: {item[0]}");

        item[0] = "new one";
        SpectreConsoleHelpers.WarningPill(Justify.Left, $"Updated item at index 0: {item[0]}");

        var itemAtIndex3 = item.TryGetAt(3);
        AnsiConsole.MarkupLine($"[bold yellow]Item at index 3 (via TryGetAt):[/] {itemAtIndex3 ?? "null"}");

        var itemAtIndex10 = item.TryGetAt(10);
        AnsiConsole.MarkupLine($"[bold yellow]Item at index 10 (via TryGetAt):[/] {itemAtIndex10 ?? "null"}");

        Console.WriteLine();
        
    }
    
}