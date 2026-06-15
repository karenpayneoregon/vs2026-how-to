using ExtensionsApp.Classes;
using ExtensionsLibrary;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace ExtensionsApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        SpectreConsoleHelpers.PinkPill(Justify.Left, "Extensions");
        Console.WriteLine("\n");

        //Samples.IncrementAndPrintValues();
        //Samples.DateOnlyExamples();
        //Samples.BetweenExamples();
        //Samples.RemoveExtraSpaces();
        //Samples.GetCurrentProjectFramework();


        await Samples.EnumRandomizeExample();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
