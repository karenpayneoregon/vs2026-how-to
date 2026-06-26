namespace CommonLibrary.Models;

/// <summary>
/// Represents an item resulting from a file matching operation, containing details about the matched file.
/// </summary>
/// <remarks>
/// This class is used to encapsulate information about files that match specified glob patterns during file system operations.
/// </remarks>
public class FileMatchItem(string sender)
{
    public string? Folder { get; init; } = Path.GetDirectoryName(sender);
    public string FileName { get; init; } = Path.GetFileName(sender);
    public override string ToString() => $"{Folder}\\{FileName}";

}