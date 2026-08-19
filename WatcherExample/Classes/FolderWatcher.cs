using Spectre.Console;
// ReSharper disable MemberCanBeMadeStatic.Local
#pragma warning disable CA1822
#pragma warning disable CA1816

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

    /// <summary>
    /// Handles the event triggered when a new file is created in the monitored folder.
    /// </summary>
    /// <param name="sender">The source of the event, typically the <see cref="System.IO.FileSystemWatcher"/> instance.</param>
    /// <param name="e">A <see cref="System.IO.FileSystemEventArgs"/> object that contains the event data, including the name and full path of the created file.</param>
    /// <remarks>
    /// This method logs the creation of a new file and processes it using the <c>GroundFile</c> method.
    /// </remarks>
    /// <example>
    /// Example of handling a file creation event:
    /// <code>
    /// private void OnFileCreated(object sender, FileSystemEventArgs e)
    /// {
    ///     Console.WriteLine($"NEW FILE: {e.FullPath}");
    ///     GroundFile(e.FullPath);
    /// }
    /// </code>
    /// </example>
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

    /// <summary>
    /// Handles the event triggered when a file is renamed in the monitored folder.
    /// </summary>
    /// <param name="sender">The source of the event, typically the <see cref="System.IO.FileSystemWatcher"/> instance.</param>
    /// <param name="e">A <see cref="System.IO.RenamedEventArgs"/> object that contains the event data, including the old and new names and full paths of the renamed file.</param>
    /// <remarks>
    /// This method logs the renaming of a file.
    /// </remarks>
    /// <example>
    /// Example of handling a file renaming event:
    /// <code>
    /// private void OnFileRenamed(object sender, RenamedEventArgs e)
    /// {
    ///     Console.WriteLine($"RENAMED FILE: {e.OldFullPath} -> {e.FullPath}");
    /// }
    /// </code>
    /// </example>
    private void OnFileRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"[blue bold]RENAMED FILE: {e.OldFullPath} -> {e.FullPath}[/]");
    }

    /// <summary>
    /// Handles the event triggered when an error occurs in the <see cref="System.IO.FileSystemWatcher"/>.
    /// </summary>
    /// <param name="sender">The source of the event, typically the <see cref="System.IO.FileSystemWatcher"/> instance.</param>
    /// <param name="e">A <see cref="System.IO.ErrorEventArgs"/> object that contains the event data, including the exception that occurred.</param>
    /// <remarks>
    /// This method logs the error that occurred in the <see cref="System.IO.FileSystemWatcher"/>.
    /// </remarks>
    /// <example>
    /// Example of handling a file system watcher error event:
    /// <code>
    /// private void OnError(object sender, ErrorEventArgs e)
    /// {
    ///     Console.WriteLine($"FileSystemWatcher error: {e.GetException()}");
    /// }
    /// </code>
    /// </example>
    private void OnError(object sender, ErrorEventArgs e)
    {
        Console.WriteLine($"[red]FileSystemWatcher error: {e.GetException()}[/]");
    }

    /// <summary>
    /// Processes the specified file by grounding it using the <see cref="Grounder"/> class.
    /// </summary>
    /// <param name="filePath">The full path of the file to ground.</param>
    /// <remarks>
    /// This method logs the grounding action and delegates the actual grounding to the <see cref="Grounder.GroundFile"/> method.
    /// </remarks>
    /// <example>
    /// Example of grounding a file:
    /// <code>
    /// private void GroundFile(string filePath)
    /// {
    ///     AnsiConsole.MarkupLine($"[cyan]Grounding:[/][yellow] {filePath}[/]");
    ///     Grounder.GroundFile(filePath);
    /// }
    /// </code>
    /// </example>
    private void GroundFile(string filePath)
    {
        AnsiConsole.MarkupLine($"[cyan]Grounding:[/][yellow] {filePath}[/]");
        Grounder.GroundFile(filePath);
    }

    /// <summary>
    /// Releases all resources used by the <see cref="FolderWatcher"/> instance.
    /// </summary>
    /// <remarks>
    /// This method unsubscribes from all events and disposes of the underlying <see cref="System.IO.FileSystemWatcher"/>.
    /// </remarks>
    public void Dispose()
    {
        _watcher.Created -= OnFileCreated;
        _watcher.Changed -= OnFileChanged;
        _watcher.Renamed -= OnFileRenamed;
        _watcher.Error -= OnError;

        _watcher.Dispose();
    }
}