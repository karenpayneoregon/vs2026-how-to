using CommonLibrary;
using Spectre.Console;
using SpectreConsoleLibrary.Core;
using System.CommandLine;

namespace FrameworkLifeCycle;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        RootCommand rootCommand = new("Get dotnet framework life cycles");

        var releases = await DotNetReleaseService.GetReleaseIndexAsync();

        SpectreConsoleHelpers.InfoPill(Justify.Left, $"Found {releases.Count} releases.");
        Console.WriteLine();

        var table = CreateReleaseInfoTable();

        foreach (var item in releases)
        {

            var eolText = item.EndOfLifeDate.HasValue
                ? item.EndOfLifeDate.Value.ToString("MM/dd/yyyy")
                : "Not set";

            var releaseType = item.ReleaseType ?? "Unknown";
            if (releaseType != "Unknown")
            {
                releaseType = releaseType.ToUpper();
            }

            table.AddRow(
                FrameworkUtilities.IsProjectFramework(item.ChannelVersion ?? ""),
                item.LatestRelease ?? "",
                releaseType,
                eolText,
                Colorize(item.SupportPhase ?? "Unknown"));
        }

        AnsiConsole.Write(table);
        Console.WriteLine();

    }

    /// <summary>
    /// Creates and configures a table to display .NET release information.
    /// </summary>
    /// <returns>
    /// A <see cref="Spectre.Console.Table"/> instance with predefined columns for 
    /// displaying .NET release details such as channel, latest release, release type, 
    /// end-of-life date, and support phase.
    /// </returns>
    private static Table CreateReleaseInfoTable()
    {
        var table = new Table().Title("[bold blue] .NET Release Information [/]");
        table.AddColumn(new TableColumn("[bold yellow]Channel[/]"));
        table.AddColumn(new TableColumn("[bold yellow]Latest[/]"));
        table.AddColumn(new TableColumn("[bold yellow]ReleaseType[/]"));
        table.AddColumn(new TableColumn("[bold yellow]End Of Life Date[/]"));
        table.AddColumn(new TableColumn("[bold yellow]Support[/]"));
        return table;
    }

    /// <summary>
    /// Applies <a href="https://spectreconsole.net/console/reference/color-reference">color formatting</a> to the specified input string based on its content.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <returns>
    /// 
    /// <para>
    ///    A colorized string formatted for <a href="https://spectreconsole.net/console">Spectre.Console</a>, based on the content of the input (case-insensitive):
    /// </para>
    /// 
    /// <list type="bullet">
    ///    <item>Returns a green-colored string if the input contains "active".</item>
    ///    <item>Returns a red-colored string if the input contains "eol".</item>
    ///    <item>Returns a yellow-colored string if the input contains "preview".</item>
    ///    <item>Returns the original input string if no match is found.</item>
    /// </list>
    /// </returns>
    private static string Colorize(string input) =>
        input switch
        {
            { } s when s.Contains("active", StringComparison.OrdinalIgnoreCase) => "[green]Active[/]",
            { } s when s.Contains("eol", StringComparison.OrdinalIgnoreCase) => "[red]eol[/]",
            { } s when s.Contains("preview", StringComparison.OrdinalIgnoreCase) => "[yellow]preview[/]",
            _ => input,
        };


}
