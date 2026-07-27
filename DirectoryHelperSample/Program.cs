using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace DirectoryHelperSample;

internal partial class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine(":desktop_computer: [DeepPink1]Directory Helpers[/]");
        Console.WriteLine();
        
        AnsiConsole.MarkupLine(":file_folder: [DeepPink1]SolutionFolder[/]");
        Console.WriteLine(DirectoryHelper.SolutionFolder());
        
        AnsiConsole.MarkupLine(":file_folder: [DeepPink1]ProjectFolder[/]");
        Console.WriteLine(DirectoryHelper.ProjectFolder());
        AnsiConsole.MarkupLine(":gem_stone: [DeepPink1]ProjectName[/]");
        Console.WriteLine(DirectoryHelper.ProjectName());

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}
