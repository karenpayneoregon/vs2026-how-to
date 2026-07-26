using NullConditionalAssignment.Models;
using Spectre.Console;
using SpectreConsoleLibrary.Core;
// ReSharper disable UseObjectOrCollectionInitializer
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable IDE0017

namespace NullConditionalAssignment;

internal partial class Program
{
    private static void Main(string[] args)
    {
        
        AnsiConsole.MarkupLine("[yellow bold]Null-conditional assignment examples[/]\n");

        //HandleAccountAssignment();
        //HandleCustomerOrderAssignment();
        HandleOrderItems();
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);

    }

    /// <summary>
    /// Handles the assignment of an order to a customer, demonstrating the use of null-conditional operators.
    /// </summary>
    /// <remarks>
    /// This method showcases how to safely assign an order to a customer's property using null-conditional operators.
    /// It also demonstrates how null-conditional assignments prevent exceptions when the target object is null.
    /// </remarks>
    private static void HandleCustomerOrderAssignment()
    {

        SpectreConsoleHelpers.PrintPink();
        
        Customer customer = new();

        customer?.Order = GetCurrentOrder();  // order is assigned to customer.Order 
        AnsiConsole.MarkupLine($"[DeepPink1]customer!.Order.Id[/] {customer!.Order.Id}");

        customer = null;

        // Null-conditional assignment, no exception is thrown, and the assignment is skipped
        customer?.Order = GetCurrentOrder(); 
        AnsiConsole.MarkupLine("[DeepPink1]No exceptions[/]");
    }

    private static Order GetCurrentOrder() => new() { Id = 111 };

    
    /// <summary>
    /// Demonstrates the use of null-conditional operators for assignment and manipulation of an <see cref="Account"/> object.
    /// </summary>
    /// <remarks>
    /// This method showcases examples of null-conditional assignments, including updating properties
    /// and safely handling null references without throwing exceptions. It also demonstrates the use of
    /// Spectre.Console for formatted console output.
    /// </remarks>
    private static void HandleAccountAssignment()
    {

        SpectreConsoleHelpers.PrintPink();
        
        Account? account = new()
        {
            Id = 1,
            Balance = 1_000.00m,
            RewardPoints = 100,
            Flags = 0b_0011,
            DisplayName = null
        };
        
        account?.Balance += 100m;
        account?.Balance -= 50m;
        account?.RewardPoints %= 10;
        account?.Flags >>= 1;

        Console.WriteLine();
        AnsiConsole.MarkupLine(account?.ToString() ?? "No account");
        Console.WriteLine();

        // Null-conditional assignment
        account?.DisplayName ??= "Unknown display name";
        Console.WriteLine($"Name: {account?.DisplayName}");

        // null out the account reference
        account = null;
        // Null-conditional assignment, no exception is thrown, and the assignment is skipped
        account?.DisplayName ??= "Default display name"; 
        Console.WriteLine("No exceptions");

        account?.Balance += 100m;
        account?.Balance -= 50m;
        account?.Flags >>= 1 ;

        Console.WriteLine();
        AnsiConsole.MarkupLine(account?.ToString() ?? "No account");
        
    }

    private static void HandleOrderItems()
    {
        SpectreConsoleHelpers.PrintPink();

        var order = new Order
        {
            Id = 123,
            OrderItems =
            [
                new OrderItem { OrderItemId = 1, Quantity = 2, UnitPrice = 10.50m },
                new OrderItem { OrderItemId = 2, Quantity = 1, UnitPrice = 25.00m }
            ]
        };

        // Display order with null-conditional access to OrderItems
        AnsiConsole.MarkupLine($"[DeepPink1]Order ID:[/] {order.Id}");
        AnsiConsole.MarkupLine("[DeepPink1]Order Items:[/]");
        order.OrderItems?.ToList()
            .ForEach(item =>
                AnsiConsole.MarkupLine($"     [DeepPink1]Item ID: " +
                                       $"{item.OrderItemId}, Quantity: " +
                                       $"{item.Quantity}, Unit Price: " +
                                       $"{item.UnitPrice:C}[/]"));

        Console.WriteLine();

        AnsiConsole.MarkupLine("[green bold]Order? anotherOrder = null[/]");
        // Null-conditional assignment for OrderItems
        Order? anotherOrder = null;
        anotherOrder?.OrderItems ??= []; // This will not execute as anotherOrder is null

        Console.WriteLine($"Another Order Items count: {anotherOrder?.OrderItems?.Count ?? 0}"); // Output: 0

        // Now assign an order to anotherOrder
        anotherOrder = new Order { Id = 456 };
        anotherOrder.OrderItems ??= new List<OrderItem>(); // This will assign a new list
        anotherOrder.OrderItems.Add(new OrderItem { OrderItemId = 3, Quantity = 5, UnitPrice = 5.00m });

        Console.WriteLine($"Another Order ID: {anotherOrder.Id}");
        AnsiConsole.WriteLine("Another Order Items:");
        anotherOrder.OrderItems.ForEach(item =>
            AnsiConsole.MarkupLine($" - Item ID: {item.OrderItemId}, " +
                                   $"Quantity: {item.Quantity}, Unit Price: {item.UnitPrice:C}"));

        Console.WriteLine();

        // Example with a null order and null-conditional member access
        Order? nullableOrder = null;
        Console.WriteLine($"Nullable Order Items count: " +
                          $"{nullableOrder?.OrderItems?.Count ?? 0}"); // Output: 0

        // Null-conditional assignment on a null object, does nothing
        nullableOrder?.OrderItems ??=
            [
                new OrderItem { OrderItemId = 4, Quantity = 1, UnitPrice = 99.99m }
            ];
        Console.WriteLine($"Nullable Order Items count after assignment attempt: " +
                          $"{nullableOrder?.OrderItems?.Count ?? 0}"); // Output: 0
    }

}