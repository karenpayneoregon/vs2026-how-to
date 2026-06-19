using CommonLibrary;
using Spectre.Console;

namespace ExtensionsApp.Classes;

/// <summary>
/// Provides functionality for creating and managing calendar tables using Spectre.Console.
/// </summary>
/// <remarks>
/// This class includes methods to generate a visual representation of a calendar for a specified month.
/// It leverages Spectre.Console's <see cref="Table"/> to display the calendar in a tabular format.
/// </remarks>
public static class CalendarTable
{


    /// <summary>
    /// Creates a Spectre.Console <see cref="Table"/> representing the calendar for a specified month.
    /// </summary>
    /// <param name="month">The month for which to create the calendar table (1 to 12).</param>
    /// <returns>
    /// A <see cref="Table"/> object displaying the days of the specified month, organized by weeks.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the <paramref name="month"/> is not in the range 1 to 12.
    /// </exception>
    public static Table CreateMonthTable(int month)
    {
        var days = DateOnlyHelpers.GetMonthDays(month);

        var table = new Table()
            .Border(TableBorder.Rounded);

        AnsiConsole.MarkupLine($"[cyan]{days[0]:MMMM yyyy}[/]");

        table.AddColumn("[bold cyan]Sunday[/]");
        table.AddColumn("[bold cyan]Monday[/]");
        table.AddColumn("[bold cyan]Tuesday[/]");
        table.AddColumn("[bold cyan]Wednesday[/]");
        table.AddColumn("[bold cyan]Thursday[/]");
        table.AddColumn("[bold cyan]Friday[/]");
        table.AddColumn("[bold cyan]Saturday[/]");

        var week = new string[7];

        foreach (var day in days)
        {
            int columnIndex = (int)day.DayOfWeek;

            week[columnIndex] = day.Day.ToString();

            bool isSaturday = day.DayOfWeek == DayOfWeek.Saturday;
            bool isLastDay = day == days[^1];

            if (isSaturday || isLastDay)
            {
                table.AddRow(week.Select(d => d ?? "").ToArray());
                week = new string[7];
            }
        }

        return table;
    }
}