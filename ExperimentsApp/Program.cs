using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace ExperimentsApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        DisplayItemDetails();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static void DisplayItemDetails()
    {
        var item = new Models.PartialExamples();
        AnsiConsole.MarkupLine($"[green]Capacity:[/] {item.Capacity}");
        AnsiConsole.WriteLine();
        SpectreConsoleHelpers.InfoPill(Justify.Left, "Item capacity is now: " + item.Capacity);
        SpectreConsoleHelpers.SuccessPill(Justify.Left, $"Item at index 0: {item[0]}");

        item[0] = "new one";
        SpectreConsoleHelpers.WarningPill(Justify.Left, $"Updated item at index 0: {item[0]}");

        var itemAtIndex3 = item.TryGetAt(3);
        AnsiConsole.MarkupLine($"[bold yellow]Item at index 3 (via TryGetAt):[/] {itemAtIndex3 ?? "null"}");

        var itemAtIndex10 = item.TryGetAt(10);
        AnsiConsole.MarkupLine($"[bold yellow]Item at index 10 (via TryGetAt):[/] {itemAtIndex10 ?? "null"}");
    }
}