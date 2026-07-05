using ConsoleConfigurationLibrary.Classes;
using EntityFrameworkLibrary;
using Microsoft.EntityFrameworkCore;
using ParameterizedCollectionModeSample.Classes;
using ParameterizedCollectionModeSample.Data;
using Spectre.Console;
using SpectreConsoleLibrary.Core;
using ParameterizedCollectionModeSample.Models;

namespace ParameterizedCollectionModeSample;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        await Samples.FiltersIdentifiers();
        //await Samples.FixIsDeleted();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
