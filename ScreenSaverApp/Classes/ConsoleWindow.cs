using System.Runtime.InteropServices;

namespace ScreenSaverApp.Classes;

internal static class ConsoleWindow
{
    private const int SwMinimize = 6;

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    /// <summary>
    /// Minimizes the console window of the current application.
    /// </summary>
    /// <remarks>
    /// This method retrieves the handle of the console window and uses the Windows API
    /// to minimize it. If the console window handle is not found, no action is performed.
    /// </remarks>
    public static void Minimize()
    {
        var handle = GetConsoleWindow();

        if (handle != IntPtr.Zero)
        {
            ShowWindow(handle, SwMinimize);
        }
    }
}