using EntityFrameworkLibrary;
using Microsoft.EntityFrameworkCore;
using NamedQueryFiltersApp.Data;
using NamedQueryFiltersApp.Models;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace NamedQueryFiltersApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        Normal();
        //IgnoreIsManagerFilters();
        //IgnoreSoftDeleteFilters();
        //IgnoreBothFilters();
        //DisplayEmployeeQueryFilters();


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

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

    private static void Normal()
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
