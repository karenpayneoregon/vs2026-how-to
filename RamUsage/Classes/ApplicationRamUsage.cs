namespace RamUsage.Classes;

/// <summary>
/// Represents the RAM usage details of an application, including its process ID, name, 
/// window title, and memory usage information.
/// </summary>
/// <remarks>
/// This class provides properties to access information about a specific application, 
/// such as its process ID, name, window title (if available), and memory usage in both 
/// bytes and megabytes. It also includes a method to format the application's details 
/// as a string.
/// </remarks>
public sealed class ApplicationRamUsage
{
    public int ProcessId { get; set; }

    public string ProcessName { get; set; } = string.Empty;

    public string? WindowTitle { get; set; }

    public long WorkingSetBytes { get; set; }

    public double WorkingSetMegabytes { get; set; }

    public override string ToString() => $"{ProcessName} (PID {ProcessId}) - {WorkingSetMegabytes:N2} MB";
}
