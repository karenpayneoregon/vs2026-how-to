using ExtensionsApp.Classes;
using ExtensionsLibrary;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace ExtensionsApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        SpectreConsoleHelpers.PinkPill(Justify.Left, "Extensions");
        Console.WriteLine("\n");

        Samples.IncrementAndPrintValues();
        Samples.DateOnlyExamples();
        Samples.BetweenExamples();
        Samples.RemoveExtraSpaces();


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
