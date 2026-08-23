using CommonLibrary;
using CopyDirectoryDemo.Classes.Core;
using Spectre.Console;

namespace CopyDirectoryDemo;

internal partial class Program
{
    static void Main(string[] args)
    {
        CopyFilesWithStatus();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Copies all text files from a source folder to a destination folder while displaying a status indicator in the console.
    /// </summary>
    /// <remarks>
    /// This method uses the Spectre.Console library to display a spinner and status message during the file copy operation.
    /// It ensures that both the source and destination folders exist before proceeding. If either folder does not exist,
    /// an error message is displayed using the <see cref="SpectreConsoleHelpers.ErrorPill(Justify, string)"/> method.
    /// </remarks>
    /// <exception cref="System.IO.DirectoryNotFoundException">
    /// Thrown if the source or destination folder does not exist.
    /// </exception>
    private static void CopyFilesWithStatus()
    {
        var sourceFolder = @"C:\OED\NotePadFiles";
        var destinationFolder = @"C:\OED\Destination";

        if (Directory.Exists(sourceFolder) && Directory.Exists(destinationFolder))
        {
            
            Thread.Sleep(1000); 
            
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .Start("Copying files..", ctx =>
                {
                    FileOperations.CopyFolder(sourceFolder, destinationFolder, "*.txt");

                });
            
            AnsiConsole.Markup("[green bold]Completed![/]");
        }
        else
        {
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "Source or destination folder does not exist.");
        }
    }
}
