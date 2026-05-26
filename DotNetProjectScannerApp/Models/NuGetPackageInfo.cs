namespace DotNetProjectScannerApp.Models;

public sealed class NuGetPackageInfo
{
    public string PackageName { get; init; } = string.Empty;
    public string? Version { get; init; }
}