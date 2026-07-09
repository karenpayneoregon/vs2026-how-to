using Microsoft.Extensions.Configuration;

namespace WebClassLibrary;

public static class ConfigurationHelpers
{
    /// <summary>
    /// Checks if a configuration property with the specified key exists.
    /// </summary>
    /// <param name="key">The key of the configuration property to check.</param>
    /// <returns>
    /// <c>true</c> if the configuration property exists; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="key"/> is null, empty, or consists only of whitespace.
    /// </exception>
    /// <remarks>
    /// This method uses the application's configuration sources to determine the existence of the specified key.
    /// </remarks>
    public static bool PropertyExists(string key)
    {

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        return GetConfiguration().GetSection(key).Exists();
    }

    /// <summary>
    /// Checks if a configuration property with the specified key exists in the provided configuration.
    /// </summary>
    /// <param name="configuration">The configuration instance to check for the property.</param>
    /// <param name="key">The key of the configuration property to check.</param>
    /// <returns>
    /// <c>true</c> if the configuration property exists; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="configuration"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="key"/> is null, empty, or consists only of whitespace.
    /// </exception>
    /// <remarks>
    /// This method checks the existence of the specified key within the provided configuration instance.
    /// </remarks>
    public static bool PropertyExists(IConfiguration configuration, string key)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        return configuration.GetSection(key).Exists();
    }

    /// <summary>
    /// Builds configuration for the current application environment.
    /// </summary>
    /// <remarks>
    /// Loading order:
    /// 1. appsettings.json
    /// 2. appsettings.{Environment}.json
    /// 3. Environment variables
    ///
    /// ASP.NET Core normally uses ASPNETCORE_ENVIRONMENT.
    /// Generic .networkers and console applications normally use DOTNET_ENVIRONMENT.
    /// ASPNETCORE_ENVIRONMENT wins when both are set.
    /// </remarks>
    public static IConfiguration GetConfiguration(string? basePath = null, string? environmentName = null)
    {
        var environment = GetEnvironmentName(environmentName);

        var resolvedBasePath = string.IsNullOrWhiteSpace(basePath)
            ? Directory.GetCurrentDirectory()
            : basePath;

        return new ConfigurationBuilder()
            .SetBasePath(resolvedBasePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }

    /// <summary>
    /// Retrieves the name of the current application environment.
    /// </summary>
    /// <param name="environmentName">
    /// An optional parameter specifying the environment name. If provided and not null or whitespace, 
    /// this value will be returned.
    /// </param>
    /// <returns>
    /// The name of the current application environment. If <paramref name="environmentName"/> is not provided 
    /// or is null/whitespace, the method attempts to retrieve the environment name from the 
    /// "ASPNETCORE_ENVIRONMENT" or "DOTNET_ENVIRONMENT" environment variables. If neither is set, 
    /// "Development" is returned as the default.
    /// </returns>
    /// <remarks>
    /// This method prioritizes the following sources for determining the environment name:
    /// 1. The <paramref name="environmentName"/> parameter, if provided.
    /// 2. The "ASPNETCORE_ENVIRONMENT" environment variable.
    /// 3. The "DOTNET_ENVIRONMENT" environment variable.
    /// 4. Defaults to "Development" if none of the above are set.
    /// </remarks>
    private static string GetEnvironmentName(string? environmentName)
    {
        if (!string.IsNullOrWhiteSpace(environmentName))
        {
            return environmentName;
        }

        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
               ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
               ?? "Development";
    }
}
