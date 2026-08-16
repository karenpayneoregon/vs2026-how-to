using ExceptionDispatchInfoApp.Classes;
using SpectreConsoleLibrary.Core;
using System.Runtime.ExceptionServices;
using Serilog;

namespace ExceptionDispatchInfoApp;

internal partial class Program
{
    
    private static void Main(string[] args)
    {
        HandleFileReadOperation1();
        
        //ReadFile();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Attempts to read a file and processes its content if it exists.
    /// </summary>
    /// <remarks>
    /// This method checks for the existence of a file with a predefined name. 
    /// If the file exists, it invokes <see cref="FileOperations.ReadFile"/> to read its content.
    /// </remarks>
    /// <exception cref="Exception">
    /// Thrown if an error occurs during the file reading process.
    /// </exception>
    private static void ReadFile()
    {
        
        var fileName = "nonexistentfile.txt";
        
        if (File.Exists(fileName))
        {
            var (success, lines, exception) = FileOperations.ReadFile();
        }
        else
        {
            SpectreConsoleHelpers.ErrorPill(Justify.Left, $"File '{fileName}' does not exist.");
            Log.Information("File '{FileName}' does not exist. Called from {Method} method.", 
                fileName, 
                nameof(ReadFile));
        }
    }

    /// <summary>
    /// Handles the operation of reading all lines from a file and processes any exceptions that occur during the operation.
    /// </summary>
    /// <remarks>
    /// This method attempts to read all lines from a file using <see cref="FileOperations.ReadAllLines"/>. 
    /// If an exception occurs, it uses <see cref="ExceptionDispatchInfo"/> to capture and rethrow the exception if needed.
    /// The method also logs the exception and displays error information using Spectre.Console helpers.
    /// </remarks>
    /// <exception cref="Exception">
    /// Thrown if the user chooses to rethrow the captured exception.
    /// </exception>
    private static void HandleFileReadOperation1()
    {
        var (lines, exceptionDispatchInfo) = FileOperations.ReadAllLines(); // file does not exist
        if (exceptionDispatchInfo is not null)
        {
            
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "An error occurred while reading the file:");
            ExceptionHelpers.ColorStandard(exceptionDispatchInfo.SourceException);
            Log.Error(exceptionDispatchInfo.SourceException, "An error occurred while reading the file.");

            if (AnsiConsole.Confirm("Continue with throw?"))
            {
                exceptionDispatchInfo!.Throw();
            }
            
        }
        else
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}

