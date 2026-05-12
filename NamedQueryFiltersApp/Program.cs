using Microsoft.EntityFrameworkCore;
using NamedQueryFiltersApp.Data;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace NamedQueryFiltersApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        //IgnoreIsManagerFilters();
        //IgnoreSoftDeleteFilters();
        //IgnoreBothFilters();
        IgnoreBothFiltersIdentifiers();


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static void IgnoreBothFilters()
    {

        SpectreConsoleHelpers.PrintPink();


        using var context = new Context();
        var employees = context.Employees
            .IgnoreQueryFilters(["SoftDelete"])
            .IgnoreQueryFilters(["IsManager"])
            .ToList();
            
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
    /// Retrieves and displays a filtered list of employees based on specific identifiers,
    /// ignoring both "SoftDelete" and "IsManager" query filters.
    /// </summary>
    /// <remarks>
    /// This method demonstrates the use of Entity Framework's query filter ignoring capabilities
    /// to fetch data that would otherwise be excluded by global query filters.
    /// </remarks>
    private static void IgnoreBothFiltersIdentifiers()
    {

        SpectreConsoleHelpers.PrintPink();

        int[] ids = [1, 2, 3, 8, 10];

        using var context = new Context();
        var employees = context.Employees
            .IgnoreQueryFilters(["SoftDelete"])
            .IgnoreQueryFilters(["IsManager"])
            .Where(b => ((IEnumerable<int>)EF.Constant(ids)).Contains(b.Id))
            .ToList();

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

    private static void IgnoreSoftDeleteFilters()
    {

        SpectreConsoleHelpers.PrintPink();
        
        using var context = new Context();
        var employees = context.Employees
            .IgnoreQueryFilters(["SoftDelete"])
            .ToList();

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

    private static void IgnoreIsManagerFilters()
    {

        SpectreConsoleHelpers.PrintPink();
        
        using var context = new Context();
        var employees = context.Employees
            .IgnoreQueryFilters(["IsManager"])
            .ToList();

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
