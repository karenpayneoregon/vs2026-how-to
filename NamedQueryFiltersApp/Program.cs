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
        IgnoreSoftDeleteFilters();
        //IgnoreBothFilters();


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

        var table = new Table();
        table.Title("Employees");
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Is Manager");
        table.AddColumn("Is Deleted");

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

        var table = new Table();
        table.Title("Employees");
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Is Manager");
        table.AddColumn("Is Deleted");

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

        var table = new Table();
        table.Title("Employees");
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Is Manager");
        table.AddColumn("Is Deleted");

        foreach (var employee in employees)
        {
            table.AddRow(employee.Id.ToString(), $"{employee.FirstName} {employee.LastName}",
                employee.IsManager ? "Yes" : "No", employee.IsDeleted ? "Yes" : "No");
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }
}
