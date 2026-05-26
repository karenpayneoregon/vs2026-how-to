using DotNetProjectScannerApp.Models;
using System.Xml.Linq;

namespace DotNetProjectScannerApp.Classes;

public sealed class DotNetProjectScanner
{
    public IReadOnlyList<ProjectInfo> GetProjects(string rootFolder)
    {
        if (string.IsNullOrWhiteSpace(rootFolder))
            throw new ArgumentException("Root folder is required.", nameof(rootFolder));

        if (!Directory.Exists(rootFolder))
            throw new DirectoryNotFoundException($"Folder not found: {rootFolder}");

        var projectFiles = Directory.GetFiles(rootFolder, "*.csproj", SearchOption.AllDirectories);

        return projectFiles
            .Select(ReadProject)
            .OrderBy(p => p.ProjectName)
            .ToList();
    }

    private static ProjectInfo ReadProject(string projectFilePath)
    {
        var document = XDocument.Load(projectFilePath);

        var projectName = Path.GetFileNameWithoutExtension(projectFilePath);

        var targetFrameworks = GetTargetFrameworks(document);
        var packages = GetNuGetPackages(document);

        return new ProjectInfo
        {
            ProjectName = projectName,
            ProjectFilePath = projectFilePath,
            TargetFrameworks = targetFrameworks,
            NuGetPackages = packages
        };
    }

    private static IReadOnlyList<string> GetTargetFrameworks(XDocument document)
    {
        var targetFramework = document
            .Descendants()
            .FirstOrDefault(e => e.Name.LocalName == "TargetFramework")
            ?.Value;

        var targetFrameworks = document
            .Descendants()
            .FirstOrDefault(e => e.Name.LocalName == "TargetFrameworks")
            ?.Value;

        if (!string.IsNullOrWhiteSpace(targetFrameworks))
        {
            return targetFrameworks
                .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(targetFramework))
        {
            return new List<string> { targetFramework.Trim() };
        }

        return [];
    }

    private static IReadOnlyList<NuGetPackageInfo> GetNuGetPackages(XDocument document)
    {
        return document
            .Descendants()
            .Where(e => e.Name.LocalName == "PackageReference")
            .Select(e => new NuGetPackageInfo
            {
                PackageName = GetPackageName(e),
                Version = GetPackageVersion(e)
            })
            .Where(p => !string.IsNullOrWhiteSpace(p.PackageName))
            .OrderBy(p => p.PackageName)
            .ToList();
    }

    private static string GetPackageName(XElement packageReference)
    {
        return packageReference.Attribute("Include")?.Value
            ?? packageReference.Attribute("Update")?.Value
            ?? string.Empty;
    }

    private static string? GetPackageVersion(XElement packageReference)
    {
        var versionAttribute = packageReference.Attribute("Version")?.Value;

        if (!string.IsNullOrWhiteSpace(versionAttribute))
            return versionAttribute;

        var versionElement = packageReference
            .Elements()
            .FirstOrDefault(e => e.Name.LocalName == "Version")
            ?.Value;

        return string.IsNullOrWhiteSpace(versionElement)
            ? null
            : versionElement.Trim();
    }
}
