using Microsoft.Extensions.Configuration;

namespace WebClassLibrary;

public static class Extensions
{
    /// <summary>
    /// Retrieves the default connection string from the specified <see cref="ConfigurationManager"/> instance.
    /// </summary>
    /// <param name="configuration">
    /// The <see cref="ConfigurationManager"/> instance from which to retrieve the connection string.
    /// </param>
    /// <returns>
    /// The default connection string associated with the key "DefaultConnection".
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the connection string with the key "DefaultConnection" is not found.
    /// </exception>
    public static string DefaultConnectionString(this ConfigurationManager configuration) => configuration.GetConnectionString("DefaultConnection")!;
    
    
    /// <summary>
    /// Retrieves the main connection string from the specified <see cref="ConfigurationManager"/> instance.
    /// </summary>
    /// <param name="configuration">
    /// The <see cref="ConfigurationManager"/> instance from which to retrieve the connection string.
    /// </param>
    /// <returns>
    /// The main connection string associated with the key "MainConnection".
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the connection string with the key "MainConnection" is not found.
    /// </exception>
    public static string MainConnectionString(this ConfigurationManager configuration) => configuration.GetConnectionString("MainConnection")!;
}
