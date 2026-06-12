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
    static void Main(string[] args)
    {
        //DisplayItemDetails();

        //DisplayCommaDelimitedMonths();
        //DecodeAllParameters();

        //DisplayCharacterOccurrences();

        //var msg = 3.14f.AsString();
        //Console.WriteLine(msg);

        //BetweenSamples();

        //DeclarationAndTypePatterns();

        //PropertyPatternSample();

        //Console.WriteLine(TimeOfDay(13));

        PositionalPatternSample();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Demonstrates the use of positional patterns to identify and categorize monetary values.
    /// </summary>
    /// <remarks>
    /// This method retrieves a list of monetary values, iterates through them, and identifies each value
    /// based on its amount and currency code using pattern matching.
    /// </remarks>
    private static void PositionalPatternSample()
    {
        SpectreConsoleHelpers.PrintPink();

        var list = MockData.MoneyList();

        foreach (var money in list)
        {
            Console.WriteLine($"{money.Amount,-5}{money.CurrencyCode}: {Identify(money)}");
        }
    }

    /// <summary>
    /// Identifies and categorizes a monetary value based on its amount and currency code.
    /// </summary>
    /// <param name="money">The monetary value to be identified, represented by an instance of the <see cref="Money"/> class.</param>
    /// <returns>
    /// A string describing the category of the monetary value, such as "No US dollars", 
    /// "Small US dollar amount", "Large US dollar amount", "Positive euro amount", 
    /// "Negative amount", or "Some other amount".
    /// </returns>
    /// <remarks>
    /// This method uses C# positional patterns to match the <paramref name="money"/> parameter
    /// against predefined conditions based on its amount and currency code.
    /// </remarks>
    private static string Identify(Money money) => money switch
    {
        (0m, "USD") => "No US dollars",
        ( > 0m and < 100m, "USD") => "Small US dollar amount",
        ( >= 100m, "USD") => "Large US dollar amount",
        ( > 0m, "EUR") => "Positive euro amount",
        ( < 0m, _) => "Negative amount",
        (_, _) => "Some other amount"
    };
    
    
    /// <summary>
    /// Demonstrates the use of property patterns and switch expressions for evaluating 
    /// and categorizing a <see cref="Person"/> object based on its properties.
    /// </summary>
    /// <remarks>
    /// This method retrieves the first person from a mock data list and evaluates their 
    /// properties using property patterns. It checks if the person is an adult living in Oregon 
    /// and categorizes them into specific age groups or states using a switch expression.
    /// </remarks>
    /// <example>
    /// A person with an age of 25 and living in Oregon will be categorized as "Adult in Oregon".
    /// </example>
    private static void PropertyPatternSample()
    {

        SpectreConsoleHelpers.PrintPink();
        
        if (MockData.PeopleList().FirstOrDefault() is not Person person) return;
        if (person is { Age: >= 18, Address.State: "OR" })
        {
            Console.WriteLine($"{person.FirstName} is an adult living in Oregon.");
        }

        string category = person switch
        {
            { Age: < 13 } => "Child",
            { Age: >= 13 and < 18 } => "Teenager",
            { Age: >= 18, Address.State: "OR" } => "Adult in Oregon",
            { Age: >= 18 } => "Adult",
            _ => "Unknown"
        };

        Console.WriteLine($"{category} - Age {person.Age}");

    }

    /// <summary>
    /// Demonstrates the use of declaration and type patterns in C#.
    /// </summary>
    /// <remarks>
    /// This method iterates through a list of <see cref="IPerson"/> objects, displaying their details.
    /// It uses pattern matching with the <c>is</c> operator to determine if an object is of type <see cref="Employee"/>.
    /// If the object is an <see cref="Employee"/>, additional information such as the badge is displayed.
    /// The method also showcases type pattern matching and property access.
    /// </remarks>
    private static void DeclarationAndTypePatterns()
    {

        SpectreConsoleHelpers.PrintPink();

        List<IPerson> list = MockData.PeopleList();
        
        foreach (var person in list)
        {
            AnsiConsole.MarkupLine($"[yellow]Person:[/][bold]{person.FirstName} {person.LastName}[/]");
            // Demonstrates the 'is' pattern matching
            if (person is Employee employee)
            {
                AnsiConsole.MarkupLine($"  [green]Is an Employee.[/]");
                AnsiConsole.MarkupLine($"  [green]Badge:[/]{employee.Badge}");
            }
            else
            {
                AnsiConsole.MarkupLine($"  [grey]Not an Employee.[/]");
            }

            // Demonstrates type pattern matching and property access
            AnsiConsole.MarkupLine($"  [cyan]Birth Date:[/]{person.BirthDate}");
        }
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

        SpectreConsoleHelpers.PrintPink();

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

    public static string TimeOfDay(int hour) => hour switch
    {
        <= 12 => "Good Morning",
        <= 16 => "Good Afternoon",
        <= 20 => "Good Evening",
        _ => "Good Night"
    };

    public static string TimeOfDay() => DateTime.Now.Hour switch
    {
        <= 12 => "Good Morning",
        <= 16 => "Good Afternoon",
        <= 20 => "Good Evening",
        _ => "Good Night"
    };


    private static string GetSeasonSouthernHemisphere(DateTime date) => date.Month switch
    {
        >= 9 and <= 11 => "Spring",
        >= 6 and <= 8 => "Summer",
        >= 3 and <= 5 => "Autumn",
        12 or 1 or 2 => "Winter",
        _ => "Not a valid month"
    };

    private static string GetSeasonNorthernHemisphere(DateTime date) => date.Month switch
    {
        >= 3 and <= 5 => "Spring",
        >= 6 and <= 8 => "Summer",
        >= 9 and <= 11 => "Autumn",
        12 or 1 or 2 => "Winter",
        _ => "Not a valid month"
    };

    // is current culture in Northern Hemisphere
    private static bool IsNorthernHemisphere() => CultureInfo.CurrentCulture switch
    {
        { Name: "en-AU" } => true,
        { Name: "en-CA" } => true,
        { Name: "en-GB" } => true,
        { Name: "en-NZ" } => true,
        { Name: "en-US" } => true,
        _ => false
    };


}