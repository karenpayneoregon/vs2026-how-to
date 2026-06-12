using RamUsage.Classes;
using RamUsage.Classes.Core;
using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace RamUsage;
internal partial class Program
{
    static async Task Main(string[] args)
    {

        try
        {
            RootCommand rootCommand = new("Memory details");
            rootCommand.SetHandler(MainOperation.Display);

            var commandLineBuilder = new CommandLineBuilder(rootCommand);

            commandLineBuilder.AddMiddleware(async (context, next) => { await next(context); });

            commandLineBuilder.UseDefaults();
            var parser = commandLineBuilder.Build();

            await parser.InvokeAsync(args);
            
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

}

internal class MainOperation
{
    public static void Display()
    {
        SpectreConsoleHelpers.InfoPill(Justify.Left, "Memory");
        Console.WriteLine("\n");

        var memory = SystemMemory.GetMemoryUsage();

        Console.WriteLine($"    Total: {memory.TotalGB:N2} GB");
        Console.WriteLine($"Available: {memory.AvailableGB:N2} GB");
        AnsiConsole.MarkupLine($"     Used: [cyan]{memory.UsedGB:N2} GB[/]");
        AnsiConsole.MarkupLine($"   Used %: [yellow]{memory.PercentUsed:N1}%[/]");
        Console.WriteLine();
    }
}