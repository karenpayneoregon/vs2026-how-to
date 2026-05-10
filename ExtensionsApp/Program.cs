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

        IncrementAndPrintValues();
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Increments a value and prints it to the console multiple times.
    /// </summary>
    /// <remarks>
    /// This method initializes an integer value, prints its initial state, 
    /// and then increments it in a loop, printing the updated value after each increment.
    /// The increment operation is performed using the <see cref="ExtensionsLibrary.IntExtensions.RefIncrement"/> extension method.
    /// </remarks>
    private static void IncrementAndPrintValues()
    {

        SpectreConsoleHelpers.PrintPink();


        List<int> values = [];
        int value = 42;
        
        values.Add(value);

        for (int index = 0; index < 10; index++)
        {
            value.RefIncrement();
            values.Add(value);
        }

        Console.WriteLine(string.Join(",", values));
    }
}
