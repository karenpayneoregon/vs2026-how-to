using Microsoft.EntityFrameworkCore;
using Serilog;
using Spectre.Console;
using UseIdentityColumnSample.Data;
using UseIdentityColumnSample.Models;
using static SpectreConsoleLibrary.Core.SpectreConsoleHelpers;

namespace UseIdentityColumnSample;

internal partial class Program
{
    static async Task Main(string[] args)
    {

        await AddCustomers();
        await PrintCustomers();

        ExitPrompt(Justify.Left);
    }

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
    private static async Task AddCustomers()
    {
        PrintPink();

        List<Customer> customers =
        [
            new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" },
            new Customer { CustomerId = 2, FirstName = "Mary", LastName = "Smith" },
            new Customer { CustomerId = 3, FirstName = "Mark", LastName = "Lebow" },
            new Customer { CustomerId = 4, FirstName = "Amy", LastName = "Gallagher" },
            new Customer { CustomerId = 5, FirstName = "Peter", LastName = "Jones" }
        ];


        await using var context = new Context();
        
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();


        context.Customers.AddRange(customers);

        try
        {
            // Ensure SET IDENTITY_INSERT and SaveChanges run in the same transaction/connection
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Customer ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Customer OFF");

                await transaction.CommitAsync();

                SuccessPill(Justify.Left, "Customers added successfully.");
            }
            catch
            {
                try
                {
                    await transaction.RollbackAsync();
                }
                catch
                {
                    // ignore rollback failures - original exception will be rethrown
                }
                throw;
            }
        }
        catch (Exception e)
        {
            Log.Error(e, nameof(AddCustomers));
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
    private static async Task PrintCustomers()
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
