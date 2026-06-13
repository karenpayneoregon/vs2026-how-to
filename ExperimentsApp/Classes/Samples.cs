using ExperimentsApp.Interfaces;
using ExperimentsApp.Models;
using ExtensionsLibrary;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Spectre.Console;
using SpectreConsoleLibrary.Core;
using System.Globalization;

namespace ExperimentsApp.Classes;

internal class Samples
{
    /// <summary>
    /// Demonstrates the use of positional patterns to identify and categorize monetary values.
    /// </summary>
    /// <remarks>
    /// This method retrieves a list of monetary values, iterates through them, and identifies each value
    /// based on its amount and currency code using pattern matching.
    /// </remarks>
    public static void PositionalPatternSample()
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
    public static void PropertyPatternSample()
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
    public static void DeclarationAndTypePatterns()
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

    public static void LogicalPattern()
    {
        SpectreConsoleHelpers.PrintPink();

        List<IPerson> list = MockData.PeopleList();

        Console.WriteLine(list.FirstOrDefault() is not Employee ? "Not an employee" : "Is an employee");
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
    public static void BetweenSamples()
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

    /// <summary>
    /// Determines the time of day based on the provided hour and returns an appropriate greeting.
    /// </summary>
    /// <param name="hour">The hour of the day (0-23).</param>
    /// <returns>
    /// A string representing the greeting corresponding to the time of day:
    /// "Good Morning" for hours 0-12,
    /// "Good Afternoon" for hours 13-16,
    /// "Good Evening" for hours 17-20,
    /// and "Good Night" for hours 21-23.
    /// </returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when the <paramref name="hour"/> is outside the valid range of 0-23.
    /// </exception>
    public static string TimeOfDay(int hour) => hour switch
    {
        <= 12 => "Good Morning",
        <= 16 => "Good Afternoon",
        <= 20 => "Good Evening",
        _ => "Good Night"
    };

    /// <summary>
    /// Determines the time of day based on the current hour and returns an appropriate greeting.
    /// </summary>
    /// <returns>
    /// A string representing a greeting based on the current time of day:
    /// "Good Morning", "Good Afternoon", "Good Evening", or "Good Night".
    /// </returns>
    /// <remarks>
    /// This method uses the current hour of the system's local time to categorize the time of day
    /// and return a corresponding greeting.
    /// </remarks>
    public static string TimeOfDay() => DateTime.Now.Hour switch
    {
        <= 12 => "Good Morning",
        <= 16 => "Good Afternoon",
        <= 20 => "Good Evening",
        _ => "Good Night"
    };

    /// <summary>
    /// Determines the season in the Southern Hemisphere based on the provided date.
    /// </summary>
    /// <param name="date">The <see cref="DateTime"/> object representing the date to evaluate.</param>
    /// <returns>
    /// A <see cref="string"/> representing the season in the Southern Hemisphere:
    /// "Spring", "Summer", "Autumn", or "Winter". Returns "Not a valid month" if the month is invalid.
    /// </returns>
    /// <remarks>
    /// In the Southern Hemisphere:
    /// - Spring occurs from September to November.
    /// - Summer occurs from June to August.
    /// - Autumn occurs from March to May.
    /// - Winter occurs in December, January, and February.
    /// </remarks>
    public static string GetSeasonSouthernHemisphere(DateTime date) => date.Month switch
    {
        >= 9 and <= 11 => "Spring",
        >= 6 and <= 8 => "Summer",
        >= 3 and <= 5 => "Autumn",
        12 or 1 or 2 => "Winter",
        _ => "Not a valid month"
    };

    /// <summary>
    /// Determines the season in the Northern Hemisphere based on the provided date.
    /// </summary>
    /// <param name="date">The <see cref="DateTime"/> object representing the date to evaluate.</param>
    /// <returns>
    /// A <see cref="string"/> representing the season in the Northern Hemisphere:
    /// "Spring", "Summer", "Autumn", or "Winter". Returns "Not a valid month" if the month is invalid.
    /// </returns>
    /// <remarks>
    /// The method uses a switch expression to map the month of the provided date to the corresponding season.
    /// </remarks>
    public static string GetSeasonNorthernHemisphere(DateTime date) => date.Month switch
    {
        >= 3 and <= 5 => "Spring",
        >= 6 and <= 8 => "Summer",
        >= 9 and <= 11 => "Autumn",
        12 or 1 or 2 => "Winter",
        _ => "Not a valid month"
    };

    public static bool IsNorthernHemisphere() => CultureInfo.CurrentCulture switch
    {
        { Name: "en-AU" } => true,
        { Name: "en-CA" } => true,
        { Name: "en-GB" } => true,
        { Name: "en-NZ" } => true,
        { Name: "en-US" } => true,
        _ => false
    };

    public static void CombineIEnumerableInt()
    {

        SpectreConsoleHelpers.PrintPink();
        
        IEnumerable<int> first = [1, 2];
        IEnumerable<int> second = [3, 4];
        IEnumerable<int> third = [5, 6];
        IEnumerable<int> fourth = [7, 8];
        IEnumerable<int> fifth = [9, 10];

        IEnumerable<int> combined = IEnumerable<int>.Combine(first, second, third, fourth, fifth);

        foreach (int number in combined)
        {
            Console.WriteLine(number);
        }
    }

    public static void CombineIEnumerableString()
    {
        SpectreConsoleHelpers.PrintPink();

        IEnumerable<string> maleFirstNames = ["James", "John", "Robert"];
        IEnumerable<string> femaleFirstNames = ["Mary", "Patricia", "Jennifer"];

        IEnumerable<string> combinedFirstNames = IEnumerable<string>.Combine(maleFirstNames, femaleFirstNames);

        foreach (string firstName in combinedFirstNames)
        {
            Console.WriteLine(firstName);
        }
    }

    public static void CombineStringList()
    {
        SpectreConsoleHelpers.PrintPink();

        var list1 = List1(out var list2, out var list3);

        // Create a collection of sequences
        IEnumerable<IEnumerable<string>> allLists = [list1, list2, list3];

        // Flatten into one sequence: Apple, Banana, Cherry, Date, Elderberry
        IEnumerable<string> combined = allLists.SelectMany(x => x);
        
        foreach (var item in combined)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        
    }

    public static void CombineIEnumerableStringSimple()
    {
        SpectreConsoleHelpers.PrintPink();

        var list1 = List1(out var list2, out var list3);

        // Combine using the generic Combine method
        var combined = IEnumerable<string>.Combine(list1, list2, list3);

        foreach (var item in combined)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        
    }

    /// <summary>
    /// Creates and returns a list of strings while also initializing two additional lists as out parameters.
    /// </summary>
    /// <param name="list2">An output parameter that will be initialized with a list of strings.</param>
    /// <param name="list3">An output parameter that will be initialized with another list of strings.</param>
    /// <returns>A list of strings containing the initial set of values.</returns>
    /// <remarks>
    /// This method initializes three lists of strings. The first list is returned as the result, 
    /// while the other two lists are provided as out parameters.
    /// </remarks>
    private static List<string> List1(out List<string> list2, out List<string> list3)
    {
        List<string> list1 = ["Apple", "Banana"];
        list2 = ["Cherry", "Date"];
        list3 = ["Elderberry"];
        return list1;
    }
}