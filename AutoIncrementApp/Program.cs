using AutoIncrementApp.Classes;
using AutoIncrementLibrary;
using SpectreConsoleLibrary.Core;

namespace AutoIncrementApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        if (FileOperations.FileDoesNotExists)
        {
            FileOperations.WriteCustomers(FileOperations.CreateCustomers());
        }

        // read customers from json file, display to the screen
        var customers = FileOperations.ReadCustomers();
        var table = CreateTable();
        foreach (var customer in customers)
        {
            table.AddRow(customer.Id.ToString(), customer.Name, customer.InvoiceValue);
        }

        AnsiConsole.Write(table);
        // update each customer by one accept Karen, increment by 10
        foreach (var customer in customers)
        {
            customer.InvoiceValue = customer.Name == "Karen Payne" ? 
                Helpers.NextInvoiceNumber(customer.InvoiceValue,10) : 
                Helpers.NextInvoiceNumber(customer.InvoiceValue);
        }
        // save back to json file followed by showing results to the screen
        FileOperations.WriteCustomers(customers);

        table.Rows.Clear();
        foreach (var customer in customers)
        {
            table.AddRow(customer.Id.ToString(), customer.Name, customer.InvoiceValue);
        }

        AnsiConsole.Write(table);
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    internal static Table CreateTable()
    {
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.LightSlateGrey)
            .Border(TableBorder.Square)
            .Title("[LightGreen]Customers[/]");

        table.AddColumn(new TableColumn("[cyan]Id[/]"));
        table.AddColumn(new TableColumn("[cyan]Name[/]"));
        table.AddColumn(new TableColumn("[cyan]Invoice number[/]"));

        return table;
    }
}