using System.Diagnostics;

namespace RamUsage.Classes;

public sealed class RamUsageService
{
    public IReadOnlyList<ApplicationRamUsage> GetTopFiveApplicationsByRam()
    {
        return Process.GetProcesses()
            .Select(TryGetProcessInfo)
            .Where(p => p != null)
            .OrderByDescending(p => p!.WorkingSetBytes)
            .Take(5)
            .Cast<ApplicationRamUsage>()
            .ToList();
    }

    private static ApplicationRamUsage? TryGetProcessInfo(Process process)
    {
        try
        {
            return new ApplicationRamUsage
            {
                ProcessId = process.Id,
                ProcessName = process.ProcessName,
                WindowTitle = string.IsNullOrWhiteSpace(process.MainWindowTitle)
                    ? null
                    : process.MainWindowTitle,
                WorkingSetBytes = process.WorkingSet64,
                WorkingSetMegabytes = Math.Round(process.WorkingSet64 / 1024d / 1024d, 2)
            };
        }
        catch
        {
            // Some system processes may deny access or exit while being queried.
            return null;
        }
        finally
        {
            process.Dispose();
        }
    }
}
