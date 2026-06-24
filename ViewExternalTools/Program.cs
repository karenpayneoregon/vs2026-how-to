using System.ComponentModel.Design;
using Spectre.Console;
using ViewExternalTools.Classes;
using ViewExternalTools.Classes.Core;
using ViewExternalTools.Models;

namespace ViewExternalTools;

internal partial class Program
{
    static void Main(string[] args)
    {
        if (Operations.FileExists)
        {
            List<ExternalTool> tools = Operations.ReadExternalTools().ToList();

            if (tools.Count == 0)
            {
                AnsiConsole.MarkupLine("[red bold]No external tools found[/]");
                SpectreConsoleHelpers.ExitPrompt(Justify.Left);
                return;
            }

            var table = CreateTable();

            foreach (var tool in tools)
            {
                table.AddRow(
                    tool.Index.ToString(),
                    Markup.Escape(tool.Title),
                    Markup.Escape(tool.Command),
                    Markup.Escape(tool.Arguments),
                    Markup.Escape(tool.InitialDirectory),
                    tool.IsGuiApp ? "[green]Yes[/]" : "[grey]No[/]",
                    tool.CloseOnExit ? "[green]Yes[/]" : "[grey]No[/]"
                );
            }

            AnsiConsole.Write(table);
            
            Operations.WriteToolsJson(tools);
            
        }
        else
        {
            SpectreConsoleHelpers.WarningPill(Justify.Left, "Configuration file does not exists");
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
        
    }

    private static Table CreateTable()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .Title("[bold cyan]External Tools[/]")
            .AddColumn(new TableColumn("[bold]Index[/]").RightAligned())
            .AddColumn("[bold]Title[/]")
            .AddColumn("[bold]Command[/]")
            .AddColumn("[bold]Arguments[/]")
            .AddColumn("[bold]Initial Directory[/]")
            .AddColumn(new TableColumn("[bold]GUI[/]").Centered())
            .AddColumn(new TableColumn("[bold]Close On Exit[/]").Centered());
        return table;
    }
}
