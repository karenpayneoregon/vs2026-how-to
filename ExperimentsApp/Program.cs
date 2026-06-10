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

internal static partial class Program
{
    static void Main(string[] args)
    {
        //DisplayItemDetails();

        //DisplayCommaDelimitedMonths();
        //DecodeAllParameters();

        //DisplayCharacterOccurrences();

        //var msg = 3.14f.AsString();
        //Console.WriteLine(msg);
        
        BetweenSamples();
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Demonstrates the usage of the <see cref="ExtensionsLibrary.ComparerExtensions.IsBetween{T}(T, T)"/> extension method
    /// with different data types, such as integers and dates.
    /// </summary>
    /// <remarks>
    /// This method performs two examples:
    /// 1. Checks if an integer value is within a specified range.
    /// 2. Checks if a date is within a specified range of dates.
    /// The results are displayed in the console.
    /// </remarks>
    private static void BetweenSamples()
    {
        {
            int start = 10;
            int end = 20;
            Console.WriteLine(15.IsBetween(start, end) ? 
                "Is between" : "Is not between");
        }

        {
            var start = new DateOnly(2026, 6, 2);
            var end = new DateOnly(2026, 6, 30);

            Console.WriteLine(new DateOnly(2026, 6, 14).IsBetween(start, end) ? 
                "Is between" : "Is not between");
        }
    }

    // caution: this is just an example of pattern matching and extension methods,
    // not a recommended way to convert floats/doubles to strings
    private static string AsString(this object input) => input switch
        {
            float f => $"It's a float: {f}",
            double d => $"It's a double: {d}",
            _ => "Not a float or double"
        };

   

    /// <summary>
    /// Displays the occurrences of each character in a predefined string.
    /// </summary>
    /// <remarks>
    /// This method analyzes a hardcoded string to determine the frequency of each character.
    /// It utilizes the <see cref="ExtensionsLibrary.StringExtensions.Occurrences"/> extension method to calculate
    /// the occurrences and outputs the results to the console. Additionally, it uses
    /// <see cref="SpectreConsoleHelpers.PrintPink"/> to format the output.
    /// </remarks>
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

        // requires NuGet package Microsoft.AspNetCore.WebUtilities
        Dictionary<string, StringValues> queryParams = QueryHelpers.ParseQuery(uri.Query);
        
        foreach (var (key, value) in queryParams)
        {
            AnsiConsole.MarkupLine($"[yellow]Parameter:[/][b]{key}[/] [cyan]Value:[/]{string.Join(",", value)}");
        }

        Console.WriteLine();

    }

}