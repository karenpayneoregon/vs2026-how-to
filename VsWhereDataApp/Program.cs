using Spectre.Console;
using System.Diagnostics;
using VsWhereDataApp.Classes;
using System.CommandLine;
namespace VsWhereDataApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        try
        {
            RootCommand rootCommand = new("Visual Studio details");


            if (IsHelpRequested(args))
            {
                DisplayHelp(rootCommand);
                return;
            }
            
            MainOperation.Display();

            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, new ExceptionSettings
            {
                Format = ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks,
                Style = new ExceptionStyle
                {
                    Exception = new Style().Foreground(Color.Grey),
                    Message = new Style().Foreground(Color.White),
                    NonEmphasized = new Style().Foreground(Color.Cornsilk1),
                    Parenthesis = new Style().Foreground(Color.Cornsilk1),
                    Method = new Style().Foreground(Color.Red),
                    ParameterName = new Style().Foreground(Color.Cornsilk1),
                    ParameterType = new Style().Foreground(Color.Red),
                    Path = new Style().Foreground(Color.Red),
                    LineNumber = new Style().Foreground(Color.Cornsilk1),
                }
            });
        }

    }

    private static bool IsHelpRequested(string[] args)
    {
        return args.Any(arg =>
            string.Equals(arg, "-help", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(arg, "--help", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(arg, "-h", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(arg, "/?", StringComparison.OrdinalIgnoreCase));
    }

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
        AnsiConsole.MarkupLine("  Provides details for Microsoft Visual Studio");
    }

}

