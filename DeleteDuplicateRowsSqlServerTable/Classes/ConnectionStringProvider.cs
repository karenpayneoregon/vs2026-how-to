using DeleteDuplicateRowsSqlServerTable.Models;
using Microsoft.Extensions.Configuration;

namespace DeleteDuplicateRowsSqlServerTable.Classes;

/// <summary>
/// Provides functionality for retrieving connection strings from the application's configuration.
/// </summary>
/// <remarks>
/// This class is responsible for accessing the <c>ConnectionStrings</c> section in the 
/// application's configuration file (e.g., appsettings.json) and retrieving specific 
/// connection string values. It is primarily used to facilitate database connectivity.
/// </remarks>
public class ConnectionStringProvider
{
    /// <summary>
    /// Retrieves the main connection string from the application's configuration file.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> representing the main connection string defined in the 
    /// <c>ConnectionStrings</c> section of the appsettings.json file.
    /// </returns>
    /// <remarks>
    /// This method reads the appsettings.json file, extracts the <c>ConnectionStrings</c> section, 
    /// and retrieves the value of the <c>MainConnection</c> property. It is used to establish 
    /// database connections.
    /// </remarks>
    public static string GetMainConnection()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        ConnectionStrings connectionStrings = configuration
            .GetSection("ConnectionStrings")
            .Get<ConnectionStrings>()!;

        return connectionStrings.MainConnection;
    }
}
