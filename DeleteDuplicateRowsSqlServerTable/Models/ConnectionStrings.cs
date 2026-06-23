namespace DeleteDuplicateRowsSqlServerTable.Models;

/// <summary>
/// Encapsulates the connection strings utilized by the application.
/// </summary>
/// <remarks>
/// This class serves as a container for connection string values specified in the application's
/// configuration file (e.g., appsettings.json). It provides a centralized and structured way
/// to manage and access database connection details efficiently.
/// </remarks>
public class ConnectionStrings
{
    /// <summary>
    /// Gets or sets the main connection string used for database connectivity.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the connection string for the primary database.
    /// </value>
    /// <remarks>
    /// This property is typically populated from the <c>ConnectionStrings</c> section of the 
    /// application's configuration file (e.g., appsettings.json). It is used to establish 
    /// connections to the main database.
    /// </remarks>
    public string MainConnection { get; set; } = string.Empty;
}
