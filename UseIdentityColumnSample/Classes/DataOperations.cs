using Serilog;
using Spectre.Console;
using Microsoft.EntityFrameworkCore;
using UseIdentityColumnSample.Data;
using UseIdentityColumnSample.Models;

using static SpectreConsoleLibrary.Core.SpectreConsoleHelpers;

namespace UseIdentityColumnSample.Classes;

public static class DataOperations
{
    /// <summary>
    /// Adds a predefined list of customers to the database, ensuring the database is reset and identity insert is enabled.
    /// </summary>
    /// <remarks>
    /// This method performs the following steps:
    /// 1. Deletes the existing database (if any).
    /// 2. Creates a new database.
    /// 3. Adds a predefined list of customers to the database.
    /// 4. Enables identity insert for the <c>dbo.Customer</c> table during the operation.
    /// 5. Saves the changes to the database.
    /// 6. Disables identity insert after the operation.
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task AddCustomers()
    {
        PrintPink();

        List<Customer> customers =
        [
            new Customer { FirstName = "John", LastName = "Doe" },
            new Customer { FirstName = "Mary", LastName = "Smith" },
            new Customer { FirstName = "Mark", LastName = "Lebow" },
            new Customer { FirstName = "Amy", LastName = "Gallagher" },
            new Customer { FirstName = "Peter", LastName = "Jones" }
        ];

        await using var context = new Context();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        AnsiConsole.MarkupLine("[yellow]Database created[/] :check_mark:");

        context.Customers.AddRange(customers);

        try
        {
            await context.SaveChangesAsync();

            SuccessPill(Justify.Left, "Customers added successfully.");
            Log.Information("Save changes successful in {X}", nameof(AddCustomers));
        }
        catch (Exception e)
        {
            Log.Error(e, nameof(AddCustomers));
            ErrorPill(Justify.Left, "Save failed, see log");
        }
    }

    /// <summary>
    /// Adds a single predefined customer to the database.
    /// </summary>
    /// <remarks>
    /// This method performs the following steps:
    /// 1. Creates a new instance of the database context.
    /// 2. Adds a customer with predefined details to the database.
    /// 3. Attempts to save the changes asynchronously.
    /// 4. Logs success or failure messages and displays corresponding console output.
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task AddCustomer()
    {
        PrintPink();


        await using var context = new Context();

        Customer customer = new Customer { FirstName = "Anne", LastName = "Adams" };
        context.Customers.Add(customer);

        try
        {
            await context.SaveChangesAsync();

            SuccessPill(Justify.Left, "Customers added successfully.");
            Log.Information("Save changes successful in {X}", nameof(AddCustomer));
        }
        catch (Exception e)
        {
            Log.Error(e, nameof(AddCustomer));
            ErrorPill(Justify.Left, "Save failed, see log");
        }
    }


    /// <summary>
    /// Asynchronously retrieves and prints a list of customers from the database.
    /// </summary>
    /// <remarks>
    /// This method connects to the database, retrieves all customer records, and displays 
    /// their details in the console using Spectre.Console for formatting.
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task PrintCustomers()
    {

        PrintPink();

        await using var context = new Context();

        var customers = await context.Customers.ToListAsync();

        AnsiConsole.MarkupLine("[cyan]Customers:[/]");

        foreach (var customer in customers)
        {
            AnsiConsole.MarkupLine($"[green]{customer.CustomerId}[/]: {customer.FirstName} {customer.LastName}");
        }

    }
}
