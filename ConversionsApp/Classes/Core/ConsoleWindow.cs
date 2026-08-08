using System.Runtime.InteropServices;

namespace ConversionsApp.Classes.Core;

public static class ConsoleWindow
{
    private const uint MONITOR_DEFAULTTONEAREST = 2;

    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOZORDER = 0x0004;
    private const uint SWP_SHOWWINDOW = 0x0040;

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(
        IntPtr hWnd,
        out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(
        IntPtr hwnd,
        uint dwFlags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool GetMonitorInfo(
        IntPtr hMonitor,
        ref MONITORINFO lpmi);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int X,
        int Y,
        int cx,
        int cy,
        uint uFlags);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MONITORINFO
    {
        public uint cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
    }

    public static bool Center()
    {
        IntPtr hwnd = GetConsoleWindow();

        if (hwnd == IntPtr.Zero)
            return false;

        if (!GetWindowRect(hwnd, out RECT windowRect))
            return false;

        IntPtr monitor = MonitorFromWindow(
            hwnd,
            MONITOR_DEFAULTTONEAREST);

        if (monitor == IntPtr.Zero)
            return false;

        var monitorInfo = new MONITORINFO
        {
            cbSize = (uint)Marshal.SizeOf<MONITORINFO>()
        };

        if (!GetMonitorInfo(monitor, ref monitorInfo))
            return false;

        int width =
            windowRect.Right - windowRect.Left;

        int height =
            windowRect.Bottom - windowRect.Top;

        int workWidth =
            monitorInfo.rcWork.Right -
            monitorInfo.rcWork.Left;

        int workHeight =
            monitorInfo.rcWork.Bottom -
            monitorInfo.rcWork.Top;

        int x =
            monitorInfo.rcWork.Left +
            ((workWidth - width) / 2);

        int y =
            monitorInfo.rcWork.Top +
            ((workHeight - height) / 2);

        return SetWindowPos(
            hwnd,
            IntPtr.Zero,
            x,
            y,
            0,
            0,
            SWP_NOSIZE |
            SWP_NOZORDER |
            SWP_SHOWWINDOW);
    }
}