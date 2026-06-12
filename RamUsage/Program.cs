using RamUsage.Classes;
using RamUsage.Classes.Core;
using Spectre.Console;
using System.CommandLine;


namespace RamUsage;
internal partial class Program
{
    static void Main(string[] args)
    {
        RootCommand rootCommand = new("Memory details");
        MainOperation.Display();
    }

}

internal class MainOperation
{
    public static void Display()
    {
        SpectreConsoleHelpers.InfoPill(Justify.Left, "Memory");
        Console.WriteLine("\n");

        var memory = SystemMemory.GetMemoryUsage();

        AnsiConsole.MarkupLine($"    Total: [cyan]{memory.TotalGB:N2} GB[/]");
        AnsiConsole.MarkupLine($"Available: [yellow]{memory.AvailableGB:N2} GB[/]");
        AnsiConsole.MarkupLine($"     Used: [cyan]{memory.UsedGB:N2} GB[/]");
        AnsiConsole.MarkupLine($"   Used %: [yellow]{memory.PercentUsed:N1}%[/]");
        Console.WriteLine();
    }
}