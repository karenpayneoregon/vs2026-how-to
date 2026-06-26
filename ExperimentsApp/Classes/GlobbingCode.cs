using CommonLibrary;
using SpectreConsoleLibrary.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentsApp.Classes;

internal class GlobbingCode
{
    /// <summary>
    /// Demonstrates the usage of globbing patterns to search for files within a specified directory,
    /// while including and excluding specific patterns.
    /// </summary>
    /// <remarks>
    /// This method utilizes asynchronous operations to retrieve a list of files matching the specified
    /// include and exclude patterns. The results are then displayed in a formatted manner.
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task DemonstrateGlobbing()
    {

        SpectreConsoleHelpers.PrintPink();

        string parentFolder = @$"C:\Users\{Environment.UserName}";
        Console.WriteLine(parentFolder);

        string[] includePatterns = ["**/*.docx"];
        string[] excludePatterns = [
            "**/Recent/**",
            "**/Templates/**",
            "**/SendTo/**",
            "**/Cookies/**",
            "**/History/**",
            "**/Application Data/**",
            "**/Local Settings/**",
            "**/NetHood/**",
            "**/PrintHood/**",
            "**/Start Menu/**",
            "**/Content.IE5/**",
            "**/Temporary Internet Files/**",
            "**/My Documents/**",
            "**/My Music/**",
            "**/My Pictures/**",
            "**/My Videos/**"
        ];
        
        var files = await GlobbingOperations.FindAsync(parentFolder, includePatterns, excludePatterns);

        foreach (var (index, file) in files.OrderBy(x => x.FileName).Index())
        {
            Console.WriteLine($"{index,-5}{file.Folder}\\{file.FileName}");
        }

    }
}
