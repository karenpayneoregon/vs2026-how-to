using System.Runtime.InteropServices;
using System.Text;

namespace RamUsage.Classes;

/// <summary>
/// Provides functionality to retrieve the titles of main windows associated with processes.
/// </summary>
/// <remarks>
/// This class interacts with the Windows API to enumerate visible windows and extract their titles.
/// It is designed to map process IDs to their corresponding main window titles.
/// </remarks>
public static class WindowTitleService
{
    /// <summary>
    /// Retrieves the titles of the main windows associated with running processes.
    /// </summary>
    /// <returns>
    /// A read-only dictionary where the keys are process IDs and the values are the titles of their main windows.
    /// </returns>
    /// <remarks>
    /// This method enumerates all visible windows on the system, extracts their titles, and maps them to the corresponding process IDs.
    /// If a process has multiple visible windows, only the first one encountered is included in the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var windowTitles = WindowTitleService.GetMainWindowTitlesByProcessId();
    /// foreach (var entry in windowTitles)
    /// {
    ///     Console.WriteLine($"Process ID: {entry.Key}, Window Title: {entry.Value}");
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="System.Diagnostics.Process"/>
    public static IReadOnlyDictionary<int, string> GetMainWindowTitlesByProcessId()
    {
        var results = new Dictionary<int, string>();

        EnumWindows((hWnd, lParam) =>
        {
            if (!IsWindowVisible(hWnd))
                return true;

            int length = GetWindowTextLength(hWnd);
            if (length == 0)
                return true;

            var builder = new StringBuilder(length + 1);

            if (GetWindowText(hWnd, builder, builder.Capacity) == 0)
                return true;

            string title = builder.ToString();

            if (string.IsNullOrWhiteSpace(title))
                return true;

            GetWindowThreadProcessId(hWnd, out uint processId);

            if (processId == 0)
                return true;

            // Keep the first visible titled window for that process.
            // Some apps have multiple windows.
            results.TryAdd((int)processId, title);

            return true;
        }, IntPtr.Zero);

        return results;
        
    }

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
}