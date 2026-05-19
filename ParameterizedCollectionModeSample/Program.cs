using ConsoleConfigurationLibrary.Classes;
using EntityFrameworkLibrary;
using Microsoft.EntityFrameworkCore;
using ParameterizedCollectionModeSample.Classes;
using ParameterizedCollectionModeSample.Data;
using Spectre.Console;
using SpectreConsoleLibrary.Core;
using ParameterizedCollectionModeSample.Models;

namespace ParameterizedCollectionModeSample;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        //await FiltersIdentifiers();
        await FixIsDeleted();

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
    /// Updates the <see cref="Employee.IsDeleted"/> property to <c>false</c> for a predefined set of employee IDs
    /// and displays the updated employee information in a formatted table.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations:
    /// <list type="bullet">
    /// <item><description>Updates the <see cref="Employee.IsDeleted"/> property for specific employees in the database.</description></item>
    /// <item><description>Fetches the updated employee records from the database.</description></item>
    /// <item><description>Displays the updated records in a table format using Spectre.Console.</description></item>
    /// </list>
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private static async Task FixIsDeleted()
    {

        SpectreConsoleHelpers.PrintPink();

        int[] ids = [2, 4, 5, 6, 7, 8, 9, 11, 12, 13, 15, 16, 17, 18, 19, 20, 24, 25];


        await using var context = new Context();
        
        await context.Employees
            .TagWithDebugInfo("ExecuteUpdateAsync operation")
            .Where(b => ((IEnumerable<int>)EF.Constant(ids)).Contains(b.Id))
            .ExecuteUpdateAsync(x => x
                .SetProperty(u => u.IsDeleted, false));


        var employees = await context.Employees
            .TagWithCallSite()
            .Where(b => ((IEnumerable<int>)EF.Constant(ids)).Contains(b.Id))
            .ToListAsync();

        AnsiConsole.WriteLine();

        var table = CreateTableFixDeleted();

        foreach (var (row, employee) in employees.Index())
        {

            table.AddRow(row.ToString(),
                employee.Id.ToString(),
                row.IsEven()
                    ? $"[HotPink]{employee.FirstName} {employee.LastName}[/]"
                    : $"{employee.FirstName} {employee.LastName}",
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
    private static Table CreateTableFixDeleted()
    {
        var table = new Table();

        table.Title("[HotPink]Employees[/]");

        table.AddColumn("[bold HotPink]Index[/]");
        table.AddColumn("[bold HotPink]Id[/]");
        table.AddColumn("[bold HotPink]Name[/]");
        table.AddColumn("[bold HotPink]Is Manager[/]");
        table.AddColumn("[bold HotPink]Is Deleted[/]");

        return table;

    }
}
