using System.Diagnostics;

namespace RamUsage.Classes;

/// <summary>
/// Provides functionality to retrieve and analyze RAM usage information for applications.
/// </summary>
/// <remarks>
/// This class is designed to identify and return details about the top memory-consuming applications 
/// running on the system. It leverages process information and window titles to provide a comprehensive 
/// view of application memory usage.
/// </remarks>
public sealed class RamUsageService
{
    /// <summary>
    /// Retrieves the top five applications consuming the most RAM on the system.
    /// </summary>
    /// <returns>
    /// A read-only list of <see cref="ApplicationRamUsage"/> objects, each representing an application 
    /// with its process ID, name, window title (if available), and memory usage details.
    /// </returns>
    /// <remarks>
    /// This method analyzes the currently running processes, associates them with their main window titles, 
    /// and sorts them by their memory usage in descending order. Only the top five memory-consuming applications 
    /// are included in the result.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if there is an issue accessing process information or memory usage data.
    /// </exception>
    /// <example>
    /// The following example demonstrates how to use this method to display the top five applications by RAM usage:
    /// <code>
    /// var service = new RamUsageService();
    /// var topApplications = service.GetTopFiveApplicationsByRam();
    /// foreach (var app in topApplications)
    /// {
    ///     Console.WriteLine($"{app.ProcessName} (PID: {app.ProcessId}) - {app.WorkingSetMegabytes:N2} MB");
    /// }
    /// </code>
    /// </example>
    public IReadOnlyList<ApplicationRamUsage> GetTopFiveApplicationsByRam()
    {
        var windowTitles = WindowTitleService.GetMainWindowTitlesByProcessId();

        return Process.GetProcesses()
            .Select(process => TryGetProcessInfo(process, windowTitles))
            .Where(p => p != null)
            .OrderByDescending(p => p!.WorkingSetBytes)
            .Take(5)
            .Cast<ApplicationRamUsage>()
            .ToList();
    }

    /// <summary>
    /// Attempts to retrieve detailed information about a specific process, including its memory usage
    /// and associated main window title.
    /// </summary>
    /// <param name="process">
    /// The <see cref="System.Diagnostics.Process"/> object representing the process to retrieve information for.
    /// </param>
    /// <param name="windowTitles">
    /// A read-only dictionary mapping process IDs to their main window titles.
    /// </param>
    /// <returns>
    /// An <see cref="ApplicationRamUsage"/> object containing details about the process, such as its ID, name,
    /// memory usage, and window title, or <c>null</c> if the process information could not be retrieved.
    /// </returns>
    /// <remarks>
    /// This method safely handles exceptions that may occur while accessing process information and ensures
    /// proper disposal of the <paramref name="process"/> object.
    /// </remarks>
    private static ApplicationRamUsage? TryGetProcessInfo(
        Process process,
        IReadOnlyDictionary<int, string> windowTitles)
    {
        try
        {
            windowTitles.TryGetValue(process.Id, out string? windowTitle);

            return new ApplicationRamUsage
            {
                ProcessId = process.Id,
                ProcessName = process.ProcessName,
                WindowTitle = windowTitle,
                WorkingSetBytes = process.WorkingSet64,
                WorkingSetMegabytes = Math.Round(process.WorkingSet64 / 1024d / 1024d, 2)
            };
        }
        catch
        {
            return null;
        }
        finally
        {
            process.Dispose();
        }
    }
}