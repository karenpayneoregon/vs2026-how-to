using ParameterizedCollectionModeSample.Classes;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace ParameterizedCollectionModeSample;

internal partial class Program
{
    static async Task Main(string[] args)
    {

        AnsiConsole.MarkupLine($"[bold]Environment[/] {EnvironmentSettings.Instance.CurrentEnvironment}\n"); 
        await Samples.FiltersIdentifiers();
        //await Samples.FixIsDeleted();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
