using ConsoleConfigurationLibrary.Classes;

namespace ExperimentsApp.Classes;

/// <summary>
/// Represents a singleton class that manages data connection configurations for the application.
/// </summary>
/// <remarks>
/// This class provides a thread-safe, lazy-initialized instance of itself to ensure a single point of access
/// for managing the application's main connection string.
/// </remarks>
public sealed class DataConnections
{
    private static readonly Lazy<DataConnections> Lazy = new(() => new DataConnections());
    public static DataConnections Instance => Lazy.Value;

    public string MainConnection { get; set; }
    public bool CreateNew { get; set; }
    
    private DataConnections()
    {
        MainConnection = AppConnections.Instance.MainConnection;
        CreateNew = EntitySettings.Instance.CreateNew;
    }

}