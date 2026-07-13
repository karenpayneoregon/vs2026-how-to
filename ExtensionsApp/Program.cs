using ExtensionsApp.Classes;
using ExtensionsLibrary;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace ExtensionsApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        //SpectreConsoleHelpers.PinkPill(Justify.Left, "Extensions");
        //Console.WriteLine("\n");

        //Samples.IncrementAndPrintValues();
        //Samples.DateOnlyExamples();
        //Samples.BetweenExamples();
        //Samples.RemoveExtraSpaces();
        //Samples.GetCurrentProjectFramework();
        //Samples.EnumRandomizeExample();


        DateOnlySamples.PriorWeek();
        DateOnlySamples.ThisWeek();
        DateOnlySamples.NextWeek();
        DateOnlySamples.GetMonthDays(DateTime.Now.Month);

        Console.WriteLine("\n\n\n\n");
        
        
        const string text = "Splits a \"PascalCase\" or \"camelCase\" string into " +
                            "'separate' \"words\" by inserting \"spaces\" before \"uppercase letters\".";

        // Splits a PascalCase or camelCase string into separate words by inserting spaces before uppercase letters.
        Console.WriteLine(text.RemoveQuotes());


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}
