using Microsoft.Extensions.Configuration;

namespace WebClassLibrary;

public static class Extensions
{
    /// <param name="configuration">
    /// The <see cref="ConfigurationManager"/> instance from which to retrieve the connection string.
    /// </param>
    extension(ConfigurationManager configuration)
    {
        /// <summary>
        /// Retrieves the default connection string from the specified <see cref="ConfigurationManager"/> instance.
        /// </summary>
        /// <returns>
        /// The default connection string associated with the key "DefaultConnection".
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the connection string with the key "DefaultConnection" is not found.
        /// </exception>
        public string DefaultConnectionString() => configuration.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Retrieves the main connection string from the specified <see cref="ConfigurationManager"/> instance.
        /// </summary>
        /// <returns>
        /// The main connection string associated with the key "MainConnection".
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the connection string with the key "MainConnection" is not found.
        /// </exception>
        public string MainConnectionString() => configuration.GetConnectionString("MainConnection")!;
    }
}
