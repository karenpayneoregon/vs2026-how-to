using DateTimeExtensions;
using DateTimeExtensions.TimeOfDay;
using DateTimeExtensions.WorkingDays;
using ExtensionsApp.Models;
using ExtensionsLibrary;
using Spectre.Console;
using SpectreConsoleLibrary.Core;
using System.Reflection;

namespace ExtensionsApp.Classes;

internal class Samples
{

    /// <summary>
    /// Retrieves and displays the target framework version of the current project.
    /// </summary>
    /// <remarks>
    /// This method uses the entry assembly to determine the target framework version.
    /// If the framework version is successfully retrieved, it is displayed in the console.
    /// Otherwise, an error message is displayed indicating that the framework version could not be determined.
    /// </remarks>
    public static void GetCurrentProjectFramework()
    {

        SpectreConsoleHelpers.PrintPink();

        Version? frameworkVersion = Assembly.GetEntryAssembly()?.GetTargetFrameworkVersion();
        // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
        if (frameworkVersion is not null)
        {
            AnsiConsole.MarkupLine($"[green bold]Project framework is[/][yellow] {frameworkVersion.Major}.{frameworkVersion.Minor}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Could not determine the target framework version.[/]");
        }
    }
    public static void RemoveExtraSpaces()
    {
        SpectreConsoleHelpers.PrintPink();
        
        const string text = "   This   is   an example   string with      extra spaces.   ";
        AnsiConsole.MarkupLine($"[yellow]Original:[/] '{text}'");
        AnsiConsole.MarkupLine($"[yellow]Trimmed End:[/] '{text.RemoveExtraSpaces(false,true)}'");
        AnsiConsole.MarkupLine($"[yellow]Not Trimmed Start:[/] '{text.RemoveExtraSpaces(false)}'");
        AnsiConsole.MarkupLine($"[yellow]Not Trimmed End:[/] '{text.RemoveExtraSpaces()}'");
        
        Console.WriteLine();
        
        
    }
    /// <summary>
    /// Demonstrates the usage of the <see cref="ComparerExtensions.IsBetween{T}(T,T,T)"/> method
    /// to check whether a value or date falls within a specified range.
    /// </summary>
    /// <remarks>
    /// This method showcases examples of checking both numeric and date ranges
    /// using the <c>IsBetween</c> extension method.
    /// </remarks>
    public static void BetweenExamples()
    {

        SpectreConsoleHelpers.PrintPink();
        
        int startValue = 5;
        int endValue = 15;
        int valueToCheck = 10;

        AnsiConsole.MarkupLine(valueToCheck.IsBetween(startValue, endValue)
            ? $"[green bold]{valueToCheck} is between {startValue} and {endValue}[/]"
            : $"[red]{valueToCheck} is NOT between {startValue} and {endValue}[/]");

        DateOnly startDate = new DateOnly(2026, 5, 1);
        DateOnly endDate = new DateOnly(2026, 5, 20);
        DateOnly dateToCheck = new DateOnly(2026, 5, 22);

        AnsiConsole.MarkupLine(dateToCheck.IsBetween(startDate, endDate)
            ? $"[green bold]{dateToCheck} is between {startDate} and {endDate}[/]"
            : $"[red]{dateToCheck} is NOT between {startDate} and {endDate}[/]");


        dateToCheck = new DateOnly(2026, 5, 18);

        AnsiConsole.MarkupLine(dateToCheck.IsBetween(startDate, endDate)
            ? $"[green bold]{dateToCheck} is between {startDate} and {endDate}[/]"
            : $"[red]{dateToCheck} is NOT between {startDate} and {endDate}[/]");

        Console.WriteLine();
    }

    /// <summary>
    /// Increments a value and prints it to the console multiple times.
    /// </summary>
    /// <remarks>
    /// This method initializes an integer value, prints its initial state, 
    /// and then increments it in a loop, printing the updated value after each increment.
    /// The increment operation is performed using the <see cref="ExtensionsLibrary.IntExtensions.RefIncrement"/> extension method.
    /// </remarks>
    public static void IncrementAndPrintValues()
    {

        SpectreConsoleHelpers.PrintPink();


        List<int> values = [];
        int value = 42;

        values.Add(value);

        for (int index = 0; index < 10; index++)
        {
            value.RefIncrement();
            values.Add(value);
        }

        Console.WriteLine(string.Join(",", values));
        
        Console.WriteLine();
        
    }
    /// <summary>
    /// Demonstrates various extension methods and utilities for working with dates, times, and business logic.
    /// </summary>
    /// <remarks>
    /// This method showcases the following examples:
    /// <list type="bullet">
    /// <item>Checking if a specific date is a working day using a culture-specific calendar.</item>
    /// <item>Determining if a date falls within daylight saving time.</item>
    /// <item>Setting a specific time for a date and verifying if it is within business hours.</item>
    /// <item>Adding a specified number of working days to the current date.</item>
    /// <item>Checking if the current time falls between two specified times.</item>
    /// </list>
    /// The results of these operations are displayed in the console using Spectre.Console for formatting.
    /// </remarks>
    public static void DateOnlyExamples()
    {
        
        SpectreConsoleHelpers.PrintPink();

        var culture = new WorkingDayCultureInfo("en-US");

        var specificDate = new DateTime(DateTime.Now.Year, 7, 4); // Example holiday
        AnsiConsole.MarkupLine(specificDate.IsWorkingDay(culture)
            ? $"[green bold]{specificDate:yyyy-MM-dd} IS a working day.[/]"
            : $"[red bold]{specificDate:yyyy-MM-dd} is NOT a working day.[/]");

        AnsiConsole.MarkupLine(specificDate.IsDaylightSavingTime()
            ? $"[green bold]{specificDate:yyyy-MM-dd} IS in daylight saving time.[/]"
            : $"[red bold]{specificDate:yyyy-MM-dd} is NOT in daylight saving time.[/]");

        specificDate = new DateTime(2026, 4, 23);
        specificDate = specificDate.SetTime(10);
        AnsiConsole.MarkupLine(specificDate.IsWithinBusinessHours(new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0))
            ? $"[green bold]{specificDate:yyyy-MM-dd} IS within business hours.[/]"
            : $"[red bold]{specificDate:yyyy-MM-dd} is NOT within business hours.[/]");


        // Add 5 working days to a date
        DateTime futureDate = DateTime.Now.AddWorkingDays(5);
        AnsiConsole.MarkupLine($"[green bold]Add 5 working days to a {DateTime.Now:MM/dd/yyyy}:[/] " +
                               $"[yellow]{futureDate:yyyy-MM-dd}[/]");


        // Check if a time is between two other times
        bool isBetween = DateTime.Now.IsBetween(new Time(9), new Time(17));
        AnsiConsole.MarkupLine($"[green bold]Is the[/] [HotPink]{DateTime.Now:HH:mm tt}[/] [green bold]between 9 AM and 5 PM?[/] " +
                               $"[yellow]{isBetween.ToYesNo()}[/]");

        Console.WriteLine();
        
    }
    
    

    /// <summary>
    /// Demonstrates the randomization of an enumeration and displays the results in a formatted manner.
    /// </summary>
    /// <remarks>
    /// This method generates a set of random values from the <see cref="Country"/> enumeration, 
    /// orders them, and displays each value with its index using Spectre.Console for styled output.
    /// </remarks>
    public static void EnumRandomizeExample()
    {
        
        SpectreConsoleHelpers.PrintPink();



        var country = Country.Usa;

        IOrderedEnumerable<Country> randomCountries = country.Randomize(10).Order();

        foreach (var (index, item)  in randomCountries.Index())
        {
            Console.WriteLine($"{index, -4}{item}");
        }
        
        Console.WriteLine();
    }
}