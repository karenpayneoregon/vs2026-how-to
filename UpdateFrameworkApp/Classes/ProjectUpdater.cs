using System.Diagnostics;
using System.Xml.Linq;

namespace UpdateFrameworkApp.Classes;

/// <summary>
/// Provides functionality to update the target framework of a .csproj file.
/// </summary>
/// <remarks>
/// Intent
/// - Use as an external tool for Visual Studio.
/// - Refactor as a dotnet tool
/// </remarks>
public class ProjectUpdater
{
    /// <summary>
    /// Updates the target framework of a specified .csproj file.
    /// </summary>
    /// <param name="csprojPath">The path to the .csproj file to be updated.</param>
    /// <param name="oldFramework">
    /// The current target framework to be replaced. Defaults to <c>net9.0</c>.
    /// </param>
    /// <param name="newFramework">
    /// The new target framework to set. Defaults to <c>net10.0</c>.
    /// </param>
    /// <returns>
    /// A message indicating the result of the update operation. This could be a success message,
    /// an error message if the file is not found, or a message indicating no changes were made.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the specified .csproj file does not exist.
    /// </exception>
    /// <exception cref="System.Xml.XmlException">
    /// Thrown if the .csproj file is not a valid XML document.
    /// </exception>
    /// <exception cref="Exception">
    /// Thrown if an unexpected error occurs during the update process.
    /// </exception>
    public static string UpdateTargetFramework(string csprojPath, string oldFramework = "net9.0", string newFramework = "net10.0")
    {
        
        var path = Path.GetDirectoryName(csprojPath);
        var proj = Path.GetFileName(csprojPath);
        
        if (!File.Exists(proj))
        {
            return $"[red]File not found:[/] {proj}";
        }

        try
        {
            var doc = XDocument.Load(proj);
            var targetFrameworkElement = doc.Root?.Element("PropertyGroup") ?.Element("TargetFramework");

            if (targetFrameworkElement == null)
            {
                return "[red]No <TargetFramework> element found.[/]";
            }

            if (targetFrameworkElement.Value.Trim() != oldFramework)
            {
                return $"[red]TargetFramework is not[/] [cyan]'{oldFramework}'[/],[red] found[/] [cyan]'" +
                       $"{targetFrameworkElement.Value}'[/]. [red]No changes made.[/]";
            }

            targetFrameworkElement.Value = newFramework;
            doc.Save(proj);
            
            var restoreResult = Restore(path);
            AnsiConsole.MarkupLine($"[green bold]Retore: {restoreResult}[/]");

            return $"[cyan]Updated TargetFramework to[/] '{newFramework}' [cyan]in[/] '{Path.GetFileName(proj)}'.";
            
        }
        catch (Exception ex)
        {
            return $"Error updating file: {ex.Message}";
        }
    }
    
    /// <summary>
    /// Executes the `dotnet restore` command in the specified project directory.
    /// </summary>
    /// <param name="path">The path to the project directory where the restore operation will be executed.</param>
    /// <returns>
    /// A message indicating the result of the restore operation. This could be a success message,
    /// an error message if the restore process fails, or a message indicating that the project directory
    /// could not be determined.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the restore process fails to start.
    /// </exception>
    /// <remarks>
    /// This method uses the `dotnet` CLI to restore dependencies for the project.
    /// Ensure that the .NET SDK is installed and accessible in the system's PATH.
    /// </remarks>
    private static string Restore(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return "[red]Could not determine project directory.[/]";
        }

        var restoreInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "restore",
            WorkingDirectory = path,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using Process restoreProcess = Process.Start(restoreInfo);

        if (restoreProcess == null)
        {
            return "[red]Failed to start dotnet restore.[/]";
        }

        var output = restoreProcess.StandardOutput.ReadToEnd();
        var error = restoreProcess.StandardError.ReadToEnd();

        restoreProcess.WaitForExit();

        if (restoreProcess.ExitCode != 0)
        {
            return $"[red]TargetFramework updated, but dotnet restore failed:[/] {error}";
        }
        
        return "Restore successful";
    }
}