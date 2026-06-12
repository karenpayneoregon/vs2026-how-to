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
    /// <summary>
    /// Displays detailed system memory usage information in the console.
    /// </summary>
    /// <remarks>
    /// This method retrieves memory usage statistics using the <see cref="SystemMemory.GetMemoryUsage"/> method
    /// and outputs the total, available, and used memory, as well as the percentage of memory used.
    /// It utilizes the Spectre.Console library to format the output with styled markup.
    /// </remarks>
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