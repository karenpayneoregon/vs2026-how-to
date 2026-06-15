using Spectre.Console;
using System.Globalization;
using CommonLibrary;
using SpectreConsoleLibrary.Core;

namespace NumericSorterApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        
        SortAndDisplayNumericStrings();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static void SortAndDisplayNumericStrings()
    {
        SpectreConsoleHelpers.PrintPink();
        
        Console.WriteLine("\n");
        
        string[] files = ["file10.txt", "file2.txt", "Pets.json", "file1.txt", "fileA.txt"];

        Array.Sort(files, new NumericStringComparer(CultureInfo.CurrentCulture));

        Console.WriteLine(string.Join(", ", files));

        List<string> names = ["item12", "item3", "item1", "item20"];

        names.Sort(new NumericStringComparer(CultureInfo.CurrentCulture));

        Console.WriteLine(string.Join(", ", names));
    }
}
