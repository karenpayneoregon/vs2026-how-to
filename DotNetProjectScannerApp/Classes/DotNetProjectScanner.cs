using DotNetProjectScannerApp.Models;
using System.Xml.Linq;

namespace DotNetProjectScannerApp.Classes;

/// <summary>
/// Provides functionality to scan and retrieve information about .NET projects within a specified directory.
/// </summary>
/// <remarks>
/// This class is designed to search for .csproj files recursively in a given directory 
/// and extract relevant project details, such as project name, file path, target frameworks, 
/// and NuGet package dependencies.
/// </remarks>
public sealed class DotNetProjectScanner
{
    /// <summary>
    /// Retrieves a list of .NET projects from the specified root folder.
    /// </summary>
    /// <param name="rootFolder">The root folder to search for .csproj files.</param>
    /// <returns>
    /// A read-only list of <see cref="Models.ProjectInfo"/> objects, 
    /// each representing a .NET project found in the specified folder.
    /// </returns>
    /// <exception cref="System.ArgumentException">
    /// Thrown when <paramref name="rootFolder"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="System.IO.DirectoryNotFoundException">
    /// Thrown when the specified <paramref name="rootFolder"/> does not exist.
    /// </exception>
    /// <remarks>
    /// This method searches recursively for .csproj files in the specified folder and its subdirectories.
    /// The projects are returned in alphabetical order based on their names.
    /// </remarks>
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

    /// <summary>
    /// Reads and parses the specified .NET project file.
    /// </summary>
    /// <param name="projectFilePath">The full path to the .csproj file to be read.</param>
    /// <returns>
    /// A <see cref="Models.ProjectInfo"/> object containing details about the project, 
    /// including its name, target frameworks, and NuGet package references.
    /// </returns>
    /// <exception cref="System.IO.FileNotFoundException">
    /// Thrown when the specified <paramref name="projectFilePath"/> does not exist.
    /// </exception>
    /// <exception cref="System.Xml.XmlException">
    /// Thrown when the .csproj file is not a valid XML document.
    /// </exception>
    /// <remarks>
    /// This method extracts project metadata such as the project name, target frameworks, 
    /// and NuGet package references by parsing the .csproj file.
    /// </remarks>
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

    /// <summary>
    /// Extracts the target frameworks specified in the provided .NET project file.
    /// </summary>
    /// <param name="document">
    /// An <see cref="XDocument"/> representing the loaded .csproj file.
    /// </param>
    /// <returns>
    /// A read-only list of strings, where each string represents a target framework defined in the project file.
    /// </returns>
    /// <remarks>
    /// This method checks for both single and multiple target framework declarations in the .csproj file.
    /// If the <c>TargetFrameworks</c> element is present, its values are split and returned as a list.
    /// If only the <c>TargetFramework</c> element is present, its value is returned as a single-item list.
    /// If neither element is found, an empty list is returned.
    /// </remarks>
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

    /// <summary>
    /// Extracts a list of NuGet package references from the specified XML document.
    /// </summary>
    /// <param name="document">
    /// The <see cref="System.Xml.Linq.XDocument"/> representing the .csproj file to analyze.
    /// </param>
    /// <returns>
    /// A read-only list of <see cref="DotNetProjectScannerApp.Models.NuGetPackageInfo"/> objects, 
    /// each representing a NuGet package reference found in the project file.
    /// </returns>
    /// <remarks>
    /// This method identifies all <c>PackageReference</c> elements in the provided XML document.
    /// It extracts the package name and version from the attributes or child elements of each <c>PackageReference</c>.
    /// The resulting list is sorted alphabetically by package name.
    /// </remarks>
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

    /// <summary>
    /// Retrieves the name of a NuGet package from the specified <see cref="System.Xml.Linq.XElement"/>.
    /// </summary>
    /// <param name="packageReference">
    /// The <see cref="System.Xml.Linq.XElement"/> representing a <c>PackageReference</c> element in a .csproj file.
    /// </param>
    /// <returns>
    /// A <see cref="System.String"/> containing the name of the NuGet package, or an empty string if the name cannot be determined.
    /// </returns>
    /// <remarks>
    /// This method checks the <c>Include</c> and <c>Update</c> attributes of the <paramref name="packageReference"/> 
    /// to extract the package name. If neither attribute is present, it returns an empty string.
    /// </remarks>
    private static string GetPackageName(XElement packageReference)
    {
        return packageReference.Attribute("Include")?.Value
            ?? packageReference.Attribute("Update")?.Value
            ?? string.Empty;
    }

    /// <summary>
    /// Retrieves the version of a NuGet package from the specified <c>PackageReference</c> element.
    /// </summary>
    /// <param name="packageReference">
    /// The <see cref="System.Xml.Linq.XElement"/> representing the <c>PackageReference</c> element 
    /// from which to extract the package version.
    /// </param>
    /// <returns>
    /// A <see cref="System.String"/> containing the version of the NuGet package, or <c>null</c> 
    /// if the version is not specified.
    /// </returns>
    /// <remarks>
    /// This method first attempts to retrieve the version from the <c>Version</c> attribute of the 
    /// <c>PackageReference</c> element. If the attribute is not present or is empty, it then looks 
    /// for a child element named <c>Version</c> and extracts its value.
    /// </remarks>
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
