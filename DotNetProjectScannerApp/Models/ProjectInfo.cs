namespace DotNetProjectScannerApp.Models;

public sealed class ProjectInfo
{
    public string ProjectName { get; init; } = string.Empty;
    public string ProjectFilePath { get; init; } = string.Empty;
    public IReadOnlyList<string> TargetFrameworks { get; init; } = [];
    public IReadOnlyList<NuGetPackageInfo> NuGetPackages { get; init; } = [];
}