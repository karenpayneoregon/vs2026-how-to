using Spectre.Console;
using SpectreConsoleLibrary.Core;
using System.Configuration;
using System.Globalization;
using ExperimentsApp.Classes;
using ExtensionsLibrary;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using PartialExamples = ExperimentsApp.Classes.PartialExamples;

namespace ExperimentsApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        //DisplayItemDetails();

        //DisplayCommaDelimitedMonths();
        DecodeAllParameters();

        DisplayCharacterOccurrences();


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static void DisplayCharacterOccurrences()
    {

        SpectreConsoleHelpers.PrintPink();
        
        var words = "Karen Payne posted this";
        AnsiConsole.MarkupLine($"[yellow]{words}[/]\n");
        var results = words.Occurrences();
        foreach (var item in results)
        {
            Console.WriteLine($"'{item.Character}' {item.Occurrences}");
        }
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

        SpectreConsoleHelpers.PrintPink();
        
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
    ///
    /// Requires NuGet package Microsoft.AspNetCore.WebUtilities
    /// 
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

    /// <summary>
    /// Decodes and displays all query parameters from a predefined web address.
    /// </summary>
    /// <remarks>
    /// This method parses the query string of a specified URI and outputs each parameter
    /// and its corresponding value(s) to the console using Spectre.Console for formatting.
    /// </remarks>
    public static void DecodeAllParameters()
    {
        
        SpectreConsoleHelpers.PrintPink();
        
        
        const string webAddress = "https://someapp?u=F20184418231.37&lang=EN&topic=whatever";
        
        var uri = new Uri(webAddress);

        Dictionary<string, StringValues> queryParams = QueryHelpers.ParseQuery(uri.Query);
        
        foreach (var (key, value) in queryParams)
        {
            AnsiConsole.MarkupLine($"[yellow]Parameter:[/][b]{key}[/] [cyan]Value:[/]{string.Join(",", value)}");
        }

        Console.WriteLine();

    }

}