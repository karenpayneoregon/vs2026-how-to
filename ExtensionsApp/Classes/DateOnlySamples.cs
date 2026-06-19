using CommonLibrary;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace ExtensionsApp.Classes;

/// <summary>
/// Provides methods for displaying date-related information in a formatted table.
/// </summary>
/// <remarks>
/// This class includes methods to display dates for the prior week, current week, next week, 
/// and a calendar for a specific month. It utilizes the Spectre.Console library for table formatting 
/// and depends on <see cref="CommonLibrary.DateOnlyHelpers"/> for date calculations.
/// </remarks>
internal class DateOnlySamples
{
    /// <summary>
    /// Displays a table of dates for the prior week, starting from the previous Sunday.
    /// </summary>
    /// <remarks>
    /// This method uses <see cref="CommonLibrary.DateOnlyHelpers.PriorWeeksDates"/> to retrieve the dates
    /// and formats them into a table using Spectre.Console.
    /// </remarks>
    public static void PriorWeek()
    {

        SpectreConsoleHelpers.PrintPink();
        var list = DateOnlyHelpers.PriorWeeksDates();

        var table = new Table();
        table.Title("[cyan]Prior Week Dates[/]");

        table.AddColumn("Day of Week");
        table.AddColumn("Date");

        foreach (var date in list)
        {
            table.AddRow(date.DayOfWeek.ToString(), date.ToString("yyyy-MM-dd"));
        }

        AnsiConsole.Write(table);

        Console.WriteLine();
    }

    /// <summary>
    /// Displays a table of dates for the next week, starting from the next occurrence of Sunday.
    /// </summary>
    /// <remarks>
    /// This method retrieves the dates for the next week using <see cref="CommonLibrary.DateOnlyHelpers.NextWeeksDates"/> 
    /// and displays them in a formatted table using the Spectre.Console library.
    /// </remarks>
    public static void NextWeek()
    {

        SpectreConsoleHelpers.PrintPink();
        var list = DateOnlyHelpers.NextWeeksDates();

        var table = new Table();
        table.Title("[cyan]Next Week Dates[/]");

        table.AddColumn("Day of Week");
        table.AddColumn("Date");

        foreach (var date in list)
        {
            table.AddRow(date.DayOfWeek.ToString(), date.ToString("yyyy-MM-dd"));
        }

        AnsiConsole.Write(table);

        Console.WriteLine();
    }
    
    /// <summary>
    /// Displays the dates of the current week in a formatted table.
    /// </summary>
    /// <remarks>
    /// This method retrieves the dates for the current week, starting from Sunday, 
    /// and displays them in a table format using Spectre.Console.
    /// </remarks>
    public static void ThisWeek()
    {
        
        SpectreConsoleHelpers.PrintPink();
        var list = DateOnlyHelpers.GetCurrentWeekDates(DayOfWeek.Sunday);
        var table = new Table();
        table.Title("[cyan]This Week Dates[/]");
        table.AddColumn("Day of Week");
        table.AddColumn("Date");
        foreach (var date in list)
        {
            table.AddRow(date.DayOfWeek.ToString(), date.ToString("yyyy-MM-dd"));
        }

        AnsiConsole.Write(table);
        Console.WriteLine();
    }

    /// <summary>
    /// Displays a calendar table for the specified month in the console.
    /// </summary>
    /// <param name="month">
    /// The month for which the calendar table should be displayed. 
    /// The value should be an integer between 1 and 12, representing the months January to December.
    /// </param>
    public static void GetMonthDays(int month)
    {
        SpectreConsoleHelpers.PrintPink();
        var table = CalendarTable.CreateMonthTable(6);
        AnsiConsole.Write(table);
    }
}
