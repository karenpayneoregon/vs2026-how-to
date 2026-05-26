using DotNetProjectScannerApp.Classes;
using DotNetProjectScannerApp.Classes.Core;
using Spectre.Console;

namespace DotNetProjectScannerApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        var solutionFolder = "C:\\OED\\DotnetLand\\VS2026\\HowToVS2026";

        if (!Directory.Exists(solutionFolder))
        {
            SpectreConsoleHelpers.WarningPill(Justify.Left, $"'{solutionFolder}' does not exists");
            SpectreConsoleHelpers.ExitPrompt(Justify.Left);
            return;
        }
        
        SpectreConsoleHelpers.PinkPill(Justify.Left, $"Projects in: {solutionFolder} ");
        Console.WriteLine("\n");

        var scanner = new DotNetProjectScanner();

        var projects = scanner.GetProjects(solutionFolder);

        foreach (var project in projects)
        {
            AnsiConsole.MarkupLine($"[cyan]{project.ProjectName}[/]");
            Console.WriteLine($"Path: {project.ProjectFilePath}");


            Console.WriteLine(project.TargetFrameworks.Count == 1
                ? $"Framework: {project.TargetFrameworks[0]}"
                : $"Frameworks: {string.Join(", ", project.TargetFrameworks)}");

            foreach (var package in project.NuGetPackages)
            {
                Console.WriteLine($"  {package.PackageName} - {package.Version ?? "No version specified"}");
            }

            Console.WriteLine();
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }




}
