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
        SpectreConsoleHelpers.PinkPill(Justify.Left, "Null-conditional assignment");

        Customer customer = new();

        customer?.Order = GetCurrentOrder();  // order is assigned to customer.Order 
        Console.WriteLine(customer!.Order.Id);

        customer = null;
        customer?.Order = GetCurrentOrder(); // Null-conditional assignment, no exception is thrown, and the assignment is skipped
        Console.WriteLine("No exceptions");
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);

    }

    private static Order GetCurrentOrder() => new() { Id = 111 };


}

public class Customer
{
    public Order Order { get; set; }
}

public class Order
{
    public int Id { get; set; }
}
