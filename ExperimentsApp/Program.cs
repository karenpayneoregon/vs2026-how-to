using Spectre.Console;
using SpectreConsoleLibrary.Core;
using System.Configuration;
using System.Globalization;
using ExperimentsApp.Classes;
using ExperimentsApp.Interfaces;
using ExperimentsApp.Models;
using ExtensionsLibrary;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using PartialExamples = ExperimentsApp.Classes.PartialExamples;

namespace ExperimentsApp;


internal static partial class Program
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <param name="args">An array of command-line arguments passed to the application.</param>
    /// <remarks>
    /// This method initializes and executes various sample demonstrations and utilities 
    /// provided by the application, such as pattern matching, data processing, and console interactions.
    /// It also includes a prompt for the user to exit the application.
    /// </remarks>
    static async Task Main(string[] args)
    {
        await Task.Delay(1);
        
        //DisplayItemDetails();

        //DisplayCommaDelimitedMonths();
        //DecodeAllParameters();

        //DisplayCharacterOccurrences();

        //var msg = 3.14f.AsString();
        //Console.WriteLine(msg);

        //Samples.BetweenSamples();
        //Samples.DeclarationAndTypePatterns();
        //Samples.PropertyPatternSample();
        //Console.WriteLine(TimeOfDay(13));
        //Samples.PositionalPatternSample();
        //Samples.CombineIEnumerableInt();
        //Samples.CombineIEnumerableString();
        //Samples.CombineStringList();
        //Samples.CombineIEnumerableStringSimple();
        //Samples.CombineIEnumerableInt();
        //Samples.LogicalPattern();

        await GlobbingCode.DemonstrateGlobbing();
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
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



}