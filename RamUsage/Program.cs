using RamUsage.Classes;
using RamUsage.Classes.Core;
using Spectre.Console;
using System.CommandLine;
using static System.Net.Mime.MediaTypeNames;


namespace RamUsage;

internal partial class Program
{
    static void Main(string[] args)
    {
        RootCommand rootCommand = new("Memory details");

        if (IsHelpRequested(args))
        {
            DisplayHelp(rootCommand);
            return;
        }

        MainOperation.Display();
    }

    /// <summary>
    /// Determines whether the help information is requested based on the provided command-line arguments.
    /// </summary>
    /// <param name="args">
    /// An array of command-line arguments passed to the application.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if any of the arguments match the help request options 
    /// ("-help", "--help", "-h", or "/?"); otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method checks for common help request arguments in a case-insensitive manner.
    /// </remarks>
    private static bool IsHelpRequested(string[] args)
    {
        return args.Any(arg =>
            string.Equals(arg, "-help", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(arg, "--help", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(arg, "-h", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(arg, "/?", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Displays help information for the application.
    /// </summary>
    /// <param name="rootCommand">
    /// The <see cref="RootCommand"/> instance containing the description and options for the application.
    /// </param>
    /// <remarks>
    /// This method outputs the application's description, usage instructions, and available options
    /// in a formatted manner using the Spectre.Console library.
    /// </remarks>
    private static void DisplayHelp(RootCommand rootCommand)
    {
        AnsiConsole.MarkupLine($"[bold]{rootCommand.Description}[/]");
        Console.WriteLine();
        AnsiConsole.MarkupLine("[bold]Usage:[/]");
        AnsiConsole.MarkupLine("  RamUsage");
        AnsiConsole.MarkupLine("  RamUsage -help");
        Console.WriteLine();
        AnsiConsole.MarkupLine("[bold]Options:[/]");
        AnsiConsole.MarkupLine("  -help, --help, -h, /?    Show help information.");
        Console.WriteLine();
        AnsiConsole.MarkupLine("[bold]Description:[/]");
        AnsiConsole.MarkupLine("  Displays total, available, used, and percentage used system memory.");
    }
}

internal class MainOperation
{
    /// <summary>
    /// Displays detailed system memory usage information and the top five applications by RAM usage.
    /// </summary>
    /// <remarks>
    /// This method retrieves memory usage statistics using the <see cref="SystemMemory.GetMemoryUsage"/> method
    /// and outputs the total, available, and used memory, as well as the percentage of memory used.
    /// It also retrieves the top five applications consuming the most RAM using the <see cref="RamUsageService.GetTopFiveApplicationsByRam"/> method.
    /// The output is formatted using the Spectre.Console library for enhanced readability.
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

        var service = new RamUsageService();
        var applications = service.GetTopFiveApplicationsByRam();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[bold]PID[/]")
            .AddColumn("[bold]Process[/]")
            .AddColumn("[bold]Window Title[/]")
            .AddColumn("[bold]RAM Used[/]");

        foreach (var app in applications)
        {
            table.AddRow(
                app.ProcessId.ToString(),
                Markup.Escape(app.ProcessName),
                Markup.Escape(app.WindowTitle ?? "(no visible window title)"),
                $"[green]{app.WorkingSetMegabytes:N2} MB[/]"
            );
        }

        Console.WriteLine("\n\n");
        AnsiConsole.MarkupLine("[bold yellow]Top 10 Applications by RAM Usage[/]");

        AnsiConsole.Write(table);

        Console.WriteLine("\n\n");
    }
}