namespace RamUsage.Classes;

/// <summary>
/// Represents detailed information about memory usage, including total, available, and used memory in bytes and gigabytes.
/// </summary>
/// <remarks>
/// This class provides properties to calculate memory usage statistics, such as the percentage of memory used,
/// and methods to convert memory sizes from bytes to gigabytes.
/// </remarks>
public sealed class MemoryUsageInfo
{
    public ulong TotalBytes { get; init; }
    public ulong AvailableBytes { get; init; }
    public ulong UsedBytes => TotalBytes - AvailableBytes;
    public double PercentUsed => TotalBytes == 0 ? 0 : UsedBytes * 100.0 / TotalBytes;

    public double TotalGB => BytesToGB(TotalBytes);
    public double AvailableGB => BytesToGB(AvailableBytes);
    public double UsedGB => BytesToGB(UsedBytes);

    private static double BytesToGB(ulong bytes)
    {
        return bytes / 1024.0 / 1024.0 / 1024.0;
    }
}