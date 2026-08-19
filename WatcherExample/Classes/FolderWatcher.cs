using Spectre.Console;

namespace WatcherExample.Classes;

/// <summary>
/// Represents a folder watcher that monitors changes in a specified directory.
/// </summary>
/// <remarks>
/// This class uses <see cref="System.IO.FileSystemWatcher"/> to observe changes such as file creation,
/// modification, renaming, and errors within a specified folder. It provides methods to start and stop
/// monitoring and implements the <see cref="System.IDisposable"/> interface to release resources.
/// </remarks>
public class FolderWatcher : IDisposable
{
    private readonly FileSystemWatcher _watcher;

    /// <summary>
    /// Initializes a new instance of the <see cref="FolderWatcher"/> class for monitoring changes in the specified folder.
    /// </summary>
    /// <param name="folderPath">The path of the folder to monitor for changes.</param>
    /// <exception cref="DirectoryNotFoundException">
    /// Thrown when the specified <paramref name="folderPath"/> does not exist.
    /// </exception>
    /// <remarks>
    /// This constructor sets up a <see cref="FileSystemWatcher"/> to observe changes such as file creation,
    /// modification, renaming, and errors within the specified folder. The watcher is initially configured to not raise events.
    /// </remarks>
    /// <example>
    /// To create a folder watcher for a specific directory:
    /// <code>
    /// var folderWatcher = new FolderWatcher("C:\\MyFolder");
    /// </code>
    /// </example>
    public FolderWatcher(string folderPath)
    {
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException(folderPath);

        _watcher = new FileSystemWatcher(folderPath)
        {
            NotifyFilter =
                NotifyFilters.FileName |
                NotifyFilters.LastWrite |
                NotifyFilters.Size,

            Filter = "*.*",

            IncludeSubdirectories = false,

            EnableRaisingEvents = false
        };

        _watcher.Created += OnFileCreated;
        _watcher.Changed += OnFileChanged;
        _watcher.Renamed += OnFileRenamed;
        _watcher.Error += OnError;
    }

    /// <summary>
    /// Starts monitoring changes in the specified folder.
    /// </summary>
    /// <remarks>
    /// This method enables the <see cref="System.IO.FileSystemWatcher.EnableRaisingEvents"/> property,
    /// allowing the observation of file system events such as creation, modification, and renaming.
    /// </remarks>
    /// <example>
    /// To start monitoring a folder:
    /// <code>
    /// var folderWatcher = new FolderWatcher("C:\\MyFolder");
    /// folderWatcher.Start();
    /// </code>
    /// </example>
    public void Start()
    {
        _watcher.EnableRaisingEvents = true;
    }

    /// <summary>
    /// Stops monitoring changes in the specified folder.
    /// </summary>
    /// <remarks>
    /// This method disables the <see cref="System.IO.FileSystemWatcher.EnableRaisingEvents"/> property,
    /// effectively halting the observation of file system events such as creation, modification, and renaming.
    /// </remarks>
    /// <example>
    /// To stop monitoring a folder after starting it:
    /// <code>
    /// var folderWatcher = new FolderWatcher("C:\\MyFolder");
    /// folderWatcher.Start();
    /// // Perform operations...
    /// folderWatcher.Stop();
    /// </code>
    /// </example>
    public void Stop()
    {
        _watcher.EnableRaisingEvents = false;
    }

    private void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[green bold]NEW FILE: {e.FullPath}[/]");

        GroundFile(e.FullPath);
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[yellow]UPDATED FILE: {e.FullPath}[/]");

        GroundFile(e.FullPath);
    }

    private void OnFileRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"[blue bold]RENAMED FILE: {e.OldFullPath} -> {e.FullPath}[/]");
    }

    private void OnError(object sender, ErrorEventArgs e)
    {
        Console.WriteLine($"[red]FileSystemWatcher error: {e.GetException()}[/]");
    }

    private void GroundFile(string filePath)
    {
        AnsiConsole.MarkupLine($"[cyan]Grounding:[/][yellow] {filePath}[/]");
        Grounder.GroundFile(filePath);
    }

    public void Dispose()
    {
        _watcher.Created -= OnFileCreated;
        _watcher.Changed -= OnFileChanged;
        _watcher.Renamed -= OnFileRenamed;
        _watcher.Error -= OnError;

        _watcher.Dispose();
    }
}