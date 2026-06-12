using System.Runtime.InteropServices;

namespace RamUsage.Classes;

/// <summary>
/// Provides methods for retrieving system memory usage information.
/// </summary>
/// <remarks>
/// This static class interacts with the operating system to obtain detailed memory statistics,
/// such as total physical memory and available physical memory. It uses platform invocation services
/// to call native Windows API functions.
/// </remarks>
public static class SystemMemory
{
    public static MemoryUsageInfo GetMemoryUsage()
    {
        var status = new MEMORYSTATUSEX();
        status.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));

        if (!GlobalMemoryStatusEx(ref status))
        {
            throw new InvalidOperationException("Unable to retrieve system memory information.");
        }

        return new MemoryUsageInfo
        {
            TotalBytes = status.ullTotalPhys,
            AvailableBytes = status.ullAvailPhys
        };
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);
}