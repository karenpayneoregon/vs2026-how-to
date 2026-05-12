using ConsoleConfigurationLibrary.Classes;
using Microsoft.EntityFrameworkCore;
using ParameterizedCollectionModeSample.Data;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace ParameterizedCollectionModeSample;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        await FiltersIdentifiers();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Retrieves and displays a filtered list of employees based on specific identifiers,
    /// ignoring both "SoftDelete" and "IsManager" query filters.
    /// </summary>
    /// <remarks>
    /// This method demonstrates the use of Entity Framework's query filter ignoring capabilities
    /// to fetch data that would otherwise be excluded by global query filters.
    /// </remarks>
    private static async Task FiltersIdentifiers()
    {

        SpectreConsoleHelpers.PrintPink();

        int[] ids = [1, 2, 3, 8, 10];

        await using var context = new Context();
        var employees = await context.Employees
            .TagWithCallSite()
            .Where(b => ((IEnumerable<int>)EF.Constant(ids)).Contains(b.Id))
            .ToListAsync();

        AnsiConsole.WriteLine();

        var table = CreateTable();

        foreach (var employee in employees)
        {
            table.AddRow(employee.Id.ToString(), $"{employee.FirstName} {employee.LastName}",
                employee.IsManager ? "Yes" : "No", employee.IsDeleted ? "Yes" : "No");
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

    }


    /// <summary>
    /// Creates and configures a table for displaying employee data.
    /// </summary>
    /// <returns>
    /// A <see cref="Spectre.Console.Table"/> instance with predefined columns for employee details.
    /// </returns>
    private static Table CreateTable()
    {
        var table = new Table();

        table.Title("[HotPink]Employees[/]");

        table.AddColumn("[bold HotPink]Id[/]");
        table.AddColumn("[bold HotPink]Name[/]");
        table.AddColumn("[bold HotPink]Is Manager[/]");
        table.AddColumn("[bold HotPink]Is Deleted[/]");

        return table;

    }
}
