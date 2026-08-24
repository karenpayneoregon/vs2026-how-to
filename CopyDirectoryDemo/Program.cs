using CommonLibrary;
using CopyDirectoryDemo.Classes.Core;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace CopyDirectoryDemo;

internal partial class Program
{
    static void Main(string[] args)
    {
        SetFileTimestamps();
        //CopyFilesWithStatus();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Sets the file timestamps for a specified destination file to match those of a source file.
    /// </summary>
    /// <remarks>
    /// This method utilizes <see cref="CommonLibrary.FileOperations.SetFileDateTime"/> to copy 
    /// the creation, last write, and last access timestamps from the source file to the destination file.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown if the source or destination file paths are null, empty, or consist only of white-space characters.
    /// </exception>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the source or destination file does not exist.
    /// </exception>
    private static void SetFileTimestamps()
    {

        SpectreConsoleHelpers.PrintPink();


        if (File.Exists(@"C:\OED\NotePadFiles\GeneralStuff.txt") && File.Exists(@"C:\OED\Destination\GeneralStuff.txt"))
        {
            
            if (FileOperations.SetFileDateTime(@"C:\OED\NotePadFiles\GeneralStuff.txt", @"C:\OED\Destination\GeneralStuff.txt"))
            {
                AnsiConsole.Markup("[green bold]File timestamps updated successfully![/]");
            }
            else
            {
                SpectreConsoleHelpers.ErrorPill(Justify.Left, "Failed to update file timestamps.");
            }

        }
        else
        {
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "Source or destination file does not exist.");
        }
        
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
        const string sourceFolder = @"C:\OED\NotePadFiles";
        const string destinationFolder = @"C:\OED\Destination";

        if (Directory.Exists(sourceFolder) && Directory.Exists(destinationFolder))
        {

            Thread.Sleep(1000);

            AnsiConsole.Status()
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
