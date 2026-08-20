using ReadJsonFromGitHubRepositoryApp.Classes;
using SpectreConsoleLibrary.Core;

namespace ReadJsonFromGitHubRepositoryApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        /*
         * Original source of the JSON
         * https://github.com/karenpayneoregon/json-samples/blob/master/unitedStates.json
         *
         * Below is set up to read the JSON from a GitHub repository in a raw format
         */
        const string url = "https://raw.githubusercontent.com/karenpayneoregon/json-samples/master/unitedStates.json";
        
        var states = await Operations.LoadStatesFromUrlAsync(url);
        
        foreach (var state in states)
        {
            AnsiConsole.MarkupLine($"[yellow]{state.Abbreviation,-5}[/][cyan]{state.Name}[/]");
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
        
    }

}