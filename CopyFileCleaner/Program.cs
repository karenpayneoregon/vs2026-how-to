using CopyFileCleaner.Classes.Core;
using Microsoft.Extensions.Options;
using Spectre.Console;



namespace CopyFileCleaner;

internal partial class Program
{
    private static readonly string[] TargetFileEndings =
    [
        " - Copy.png",
        " - Copy.jpg",
        " - Copy.zip",
        " - Copy (2).zip",
        " - Copy.snag",
        " - Copy.snagx"
    ];

    private static void Main(string[] args)
    {
        /*
         * C:\Users\paynek\OneDrive - Oregon\Documents\Visual Studio 18
         * C:\Users\paynek\Documents\Visual Studio 18\Templates\ProjectTemplates
         * C:\Users\paynek\Documents\Visual Studio 2022\Templates\ProjectTemplates
         * 
         * C:\Users\paynek\Documents\Snagit
         */
        var options = new CleanerOptions(
            RootFolder: @"C:\Users\paynek\Documents\Visual Studio 2022\Templates\ProjectTemplates",
            //RootFolder: @"C:\Users\paynek\OneDrive - Oregon\Documents\Visual Studio 18",
            DryRun: false);

        //if (!Directory.Exists(options.RootFolder))
        //{
        //    Console.Error.WriteLine($"Folder does not exist: {options.RootFolder}");

        //    return;
        //}
        
        

        var rootFolder = Path.GetFullPath(Environment.ExpandEnvironmentVariables(options.RootFolder));
        var test = Directory.Exists(rootFolder);
        
        AnsiConsole.MarkupLine($"[cyan]Folder exists {test}[/]");

        Console.WriteLine($"Root folder : {options.RootFolder}");
        Console.WriteLine($"Mode        : {(options.DryRun ? "Dry run" : "Delete")}");
        Console.WriteLine();

        var matchedCount = 0;
        var deletedCount = 0;
        var failedCount = 0;

        foreach (var filePath in EnumerateFilesSafely(options.RootFolder))
        {
            if (!IsCopyFile(filePath))
            {
                continue;
            }

            matchedCount++;

            if (options.DryRun)
            {
                Console.WriteLine($"Would delete: {filePath}");
                continue;
            }

            if (TryDeleteFile(filePath, out var errorMessage))
            {
                deletedCount++;
                Console.WriteLine($"Deleted: {filePath}");
            }
            else
            {
                failedCount++;
                Console.Error.WriteLine($"Failed: {filePath}");
                Console.Error.WriteLine($"        {errorMessage}");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Matched : {matchedCount}");
        Console.WriteLine($"Deleted : {deletedCount}");
        Console.WriteLine($"Failed  : {failedCount}");

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static bool IsCopyFile(string filePath)
    {
        var fileName = Path.GetFileName(filePath).Trim();

        return TargetFileEndings.Any(ending => fileName.EndsWith(ending, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Enumerates all files within the specified root folder and its subdirectories safely.
    /// </summary>
    /// <param name="rootFolder">The root folder to start enumerating files from.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of file paths found within the folder hierarchy.</returns>
    /// <remarks>
    /// This method handles exceptions such as <see cref="UnauthorizedAccessException"/>, <see cref="IOException"/>, 
    /// and <see cref="System.Security.SecurityException"/> that may occur during file or directory access.
    /// If an exception occurs, the folder or subfolder causing the issue is skipped, and the method continues processing.
    /// </remarks>
    private static IEnumerable<string> EnumerateFilesSafely(string rootFolder)
    {
        var pendingFolders = new Stack<string>();
        pendingFolders.Push(rootFolder);

        while (pendingFolders.Count > 0)
        {
            var currentFolder = pendingFolders.Pop();

            Console.WriteLine($"Scanning: {currentFolder}");

            string[] files;

            try
            {
                files = Directory.GetFiles(currentFolder);
            }
            catch (Exception exception) when (exception is UnauthorizedAccessException or IOException or System.Security.SecurityException)
            {
                Console.Error.WriteLine($"Skipped folder: {currentFolder}");

                Console.Error.WriteLine($"                {exception.Message}");

                continue;
            }

            foreach (var file in files)
            {
                yield return file;
            }

            string[] childFolders;

            try
            {
                childFolders = Directory.GetDirectories(currentFolder);
            }
            catch (Exception exception) when (exception is UnauthorizedAccessException or IOException or System.Security.SecurityException)
            {
                Console.Error.WriteLine($"Could not enumerate subfolders: {currentFolder}");

                Console.Error.WriteLine($"                                {exception.Message}");

                continue;
            }

            foreach (var childFolder in childFolders)
            {
                pendingFolders.Push(childFolder);
            }
        }
    }

    private static bool TryDeleteFile(string filePath, out string errorMessage)
    {
        errorMessage = string.Empty;

        try
        {
            var fullPath = Path.GetFullPath(filePath);
            var fileInfo = new FileInfo(fullPath);

            fileInfo.Refresh();

            if (!fileInfo.Exists)
            {
                errorMessage = $"File was not found: {fullPath}";
                return false;
            }

            var attributes = fileInfo.Attributes;

            attributes &= ~FileAttributes.ReadOnly;
            attributes &= ~FileAttributes.Hidden;
            attributes &= ~FileAttributes.System;

            fileInfo.Attributes = attributes;

            fileInfo.Delete();
            fileInfo.Refresh();

            if (fileInfo.Exists)
            {
                errorMessage = "Delete completed without an exception, " +
                               "but the file still exists.";

                return false;
            }

            return true;
        }
        catch (Exception exception)
        {
            errorMessage = $"{exception.GetType().Name}: {exception.Message}";

            return false;
        }
    }
}

internal sealed record CleanerOptions(string RootFolder, bool DryRun);