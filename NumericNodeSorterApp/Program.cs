using Spectre.Console;
using System.Globalization;
using NumericSorterApp.Classes;
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

        var names = new List<string> { "item12", "item3", "item1", "item20" };

        names.Sort(new NumericStringComparer(CultureInfo.CurrentCulture));

        Console.WriteLine(string.Join(", ", names));
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Configures and displays a styled text path in the console using Spectre.Console.
    /// </summary>
    /// <remarks>
    /// This method creates a <see cref="TextPath"/> object with custom styles for the root, 
    /// separator, stem, and leaf components. The styled path is then rendered to the console.
    /// </remarks>
    private static void ConfigureAndDisplayTextPath()
    {
        var path = new TextPath("/home/user/projects/app/Program.cs")
            .RootStyle(new Style(Color.Yellow, decoration: Decoration.Bold))
            .SeparatorStyle(new Style(Color.DeepPink1))
            .StemStyle(new Style(Color.Cyan))
            .LeafStyle(new Style(Color.Green, decoration: Decoration.Underline));

        AnsiConsole.Write(path);
    }

    /// <summary>
    /// Displays a warning message in the console using Spectre.Console.
    /// </summary>
    /// <remarks>
    /// This method renders a multi-line warning message with styled text, including a yellow 
    /// "Warning" label and a dimmed message indicating that multiple issues were detected.
    /// </remarks>
    private static void DisplayWarningMessage()
    {
        var multiLine = new Markup(
            "\n\n" +
            "[yellow]Warning:[/] Multiple issues detected.\n" +
            "[dim]See log for details.[/]" +
            "\n\n"
        );
        AnsiConsole.Write(multiLine);
    }
}
