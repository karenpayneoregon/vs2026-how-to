using Spectre.Console;
using SpectreConsoleLibrary.Core;
#pragma warning disable IDE0017

namespace NullConditionalAssignment;

internal partial class Program
{
    static void Main(string[] args)
    {
        SpectreConsoleHelpers.PinkPill(Justify.Left, 
            "Null-conditional assignment");

        Customer customer = new();
        customer?.Order = GetCurrentOrder();
        Console.WriteLine(customer!.Order.Id);

        customer = null;
        customer?.Order = GetCurrentOrder(); // Null-conditional assignment
        Console.WriteLine("No exceptions");

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
    
    private static Order GetCurrentOrder()
    {
        return new Order { Id = 111 };
    }
}

public class Customer
{
    public Order Order { get; set; }
}

public class Order
{
    public int Id { get; set; }
}
