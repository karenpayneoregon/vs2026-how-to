using EntityFrameworkLibrary;
using Microsoft.EntityFrameworkCore;
using NamedQueryFiltersApp.Data;
using NamedQueryFiltersApp.Models;
using Spectre.Console;
using SpectreConsoleLibrary.Core;
namespace NamedQueryFiltersApp;

internal partial class Program
{
    private static async Task Main(string[] args)
    {

        await Task.Delay(0);
        
        //await PerformDelete();
        
        //BothFiltersEnabled();
        //IgnoreIsManagerFilters();
        //IgnoreSoftDeleteFilters();
        //IgnoreBothFilters();
        //DisplayEmployeeQueryFilters();


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
        
    }

    /// <summary>
    /// Displays the query filters applied to the <see cref="Employee"/> entity.
    /// </summary>
    /// <remarks>
    /// This method checks if the <see cref="Employee"/> entity has any query filters applied 
    /// in the current <see cref="DbContext"/>. If filters are found, it retrieves and displays 
    /// their names and expressions in a formatted output using Spectre.Console. If no filters 
    /// are applied, a message indicating the absence of filters is displayed.
    /// </remarks>
    private static void DisplayEmployeeQueryFilters()
    {
        using var context = new Context();
        if (context.HasQueryFilter<Employee>())
        {
            var filters = context.GetQueryFilters<Employee>();
            
            if (filters is null) return;
            
            foreach (var (index, filter) in filters.Index())
            {
                AnsiConsole.MarkupLine($"{index, -4}" +
                                       $"[cyan]Name[/] {filter.Key} " +
                                       $"[cyan]Expression[/] {filter.Expression}");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]No query filters found for Employee entity.[/]");
        }
    }

    /// <summary>
    /// Retrieves and displays a list of employees while ignoring specific query filters.
    /// </summary>
    /// <remarks>
    /// This method bypasses the "SoftDelete" and "IsManager" query filters to fetch all employees,
    /// regardless of their deletion status or managerial role. The retrieved data is displayed in a
    /// formatted table using Spectre.Console.
    /// </remarks>
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
    /// Retrieves and displays a list of employees while ignoring specific query filters, such as the soft delete filter.
    /// </summary>
    /// <remarks>
    /// This method uses Entity Framework Core's query filter functionality to bypass filters applied to the 
    /// <see cref="Context.Employees"/> DbSet. The retrieved data is displayed in a formatted table using Spectre.Console.
    /// </remarks>
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

    /// <summary>
    /// Retrieves and displays a list of employees while ignoring the "IsManager" query filter.
    /// </summary>
    /// <remarks>
    /// This method demonstrates how to bypass specific query filters in Entity Framework Core
    /// by ignoring the "IsManager" filter. The retrieved employee data is displayed in a formatted table
    /// using Spectre.Console.
    /// </remarks>
    private static void IgnoreIsManagerFilters()
    {

        SpectreConsoleHelpers.PrintPink();

        using var context = new Context();
        var employees = context.Employees
            .IgnoreQueryFilters(["IsManager"])
            .TagWithDebugInfo("Running IgnoreQueryFilters([\"IsManager\"]")
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
    /// Displays a list of employees in a formatted table, including their ID, name, 
    /// manager status, and deletion status.
    /// </summary>
    /// <remarks>
    /// This method retrieves all employees from the database, formats their details 
    /// into a table, and outputs the table to the console using Spectre.Console.
    /// </remarks>
    private static void BothFiltersEnabled()
    {

        SpectreConsoleHelpers.PrintPink();

        using var context = new Context();
        var employees = context.Employees
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
    /// Deletes an employee record from the database based on a predefined ID.
    /// </summary>
    /// <remarks>
    /// This method retrieves an employee with a specific ID from the database. If the employee exists, 
    /// it removes the employee and saves the changes to the database. The method provides feedback 
    /// on the operation's success or failure using Spectre.Console for console output.
    /// </remarks>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// </returns>
    private static async Task PerformDelete()
    {

        SpectreConsoleHelpers.PrintPink();

        int id = 2;

        await using var context = new Context();
        var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        
        if (employee is not null)
        {
            context.Employees.Remove(employee).State = EntityState.Deleted;
            var affected = await context.SaveChangesAsync();
            AnsiConsole.MarkupLine(affected > 0
                ? $"[green]Successfully deleted employee with ID {id}.[/]"
                : $"[red]Failed to delete employee with ID {id}. Affected rows: {affected}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[yellow]Employee with ID {id} not found.[/]");
        }
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
