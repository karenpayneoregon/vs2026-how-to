using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParameterizedCollectionModeSample.Classes;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace ParameterizedCollectionModeSample;

internal partial class Program
{
    static async Task Main(string[] args)
    {

        AnsiConsole.MarkupLine($"{EnvironmentSettings.Instance.CurrentEnvironment}"); 
        await Samples.FiltersIdentifiers();
        //await Samples.FixIsDeleted();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
