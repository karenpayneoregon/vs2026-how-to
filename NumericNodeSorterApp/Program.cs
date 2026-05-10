using Spectre.Console;
using System.Globalization;
using CommonLibrary;
using NumericSorterApp.Classes.Core;

namespace NumericSorterApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        SpectreConsoleHelpers.PinkPill(Justify.Left, "Sorting");
        
        Console.WriteLine("\n");
        
        string[] files = ["file10.txt", "file2.txt", "file1.txt", "fileA.txt"];

        Array.Sort(files, new NumericStringComparer(CultureInfo.CurrentCulture));

        Console.WriteLine(string.Join(", ", files));

        List<string> names = ["item12", "item3", "item1", "item20"];

        names.Sort(new NumericStringComparer(CultureInfo.CurrentCulture));

        Console.WriteLine(string.Join(", ", names));
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

}
