namespace RamUsage.Classes;

public sealed class ApplicationRamUsage
{
    public int ProcessId { get; set; }

    public string ProcessName { get; set; } = string.Empty;

    public string? WindowTitle { get; set; }

    public long WorkingSetBytes { get; set; }

    public double WorkingSetMegabytes { get; set; }

    public override string ToString() => $"{ProcessName} (PID {ProcessId}) - {WorkingSetMegabytes:N2} MB";
}
