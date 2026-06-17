using Spectre.Console;
using VsWhereDataApp.Models;

namespace VsWhereDataApp.Classes;

internal class MainOperation
{
    /// <summary>
    /// Displays information about Visual Studio installations in the console.
    /// </summary>
    /// <remarks>
    /// This method generates a JSON file containing Visual Studio installation data, reads the data,
    /// and filters out installations with paths containing "BuildTools". It then formats and displays
    /// the details of the remaining installations, including edition, version, display version, path,
    /// install date, update date, and release notes.
    /// </remarks>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the required `vswhere.exe` file is not found.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if there is an issue with generating or reading the JSON data.
    /// </exception>
    public static void Display()
    {
        string outputPath = Path.Combine(AppContext.BaseDirectory, "vs.json");
        FileOperations.GenerateDataJson(outputPath);

        // may need to modify
        var data = FileOperations.ReadDataJson(outputPath);

        foreach (var installation in data.Where(installation => !installation.InstallationPath.Contains("BuildTools")))
        {
            Console.WriteLine();
            AnsiConsole.MarkupLine($"[green3_1]        Edition[/] {installation?.DisplayName}");
            AnsiConsole.MarkupLine($"[green3_1]        Version[/] {installation?.InstallationVersion}");
            AnsiConsole.MarkupLine($"[green3_1]Display Version[/] {installation?.Catalog.ProductDisplayVersion}");
            AnsiConsole.MarkupLine($"[green3_1]           Path[/] {installation?.InstallationPath}");
            AnsiConsole.MarkupLine($"[green3_1]   Install Date[/] {installation?.InstallDate} ");
            AnsiConsole.MarkupLine($"[green3_1]    Update Date[/] {installation?.UpdateDate} ");
            AnsiConsole.MarkupLine($"[green3_1]  Release Notes[/] {installation?.ReleaseNotes} ");
            Console.WriteLine();
        }

    }
}
