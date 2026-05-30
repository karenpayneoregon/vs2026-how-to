using BootstrapComponentsApp.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.IO.File;

namespace BootstrapComponentsApp.Classes;

/// <summary>
/// Provides operations for managing badge-related settings in the application.
/// </summary>
/// <remarks>
/// This class includes methods for reading and saving badge counts to the application's settings file (appsettings.json).
/// It interacts with the "BadgeSettings" section of the configuration file to persist and retrieve badge-related data.
/// </remarks>
public class BadgeOperations
{
    /// <summary>
    /// Reads the badge count from the application settings file (appsettings.json).
    /// </summary>
    /// <returns>
    /// The badge count as an integer. If the badge count is not found or cannot be parsed, a default value of 1 is returned.
    /// </returns>
    /// <remarks>
    /// This method retrieves the badge count from the "BadgeSettings" section of the appsettings.json file.
    /// If the file or the specific settings are missing, it defaults to a badge count of 1.
    /// </remarks>
    public static int ReadBadgeCountFromAppSettings()
    {
        var appSettingsPath = AppSettingsPath();

        string json = ReadAllText(appSettingsPath);

        JsonNode? root = JsonNode.Parse(json);

        BadgeSettings? badgeSettings = root?[nameof(BadgeSettings)]?.Deserialize<BadgeSettings>();

        return badgeSettings?.BadgeCount ?? 1;

    }

    /// <summary>
    /// Saves the specified badge count to the application settings file (appsettings.json).
    /// </summary>
    /// <param name="badgeCount">
    /// The badge count to be saved. This value will be stored in the "BadgeSettings" section of the appsettings.json file.
    /// </param>
    /// <remarks>
    /// This method updates the "BadgeCount" property in the "BadgeSettings" section of the appsettings.json file.
    /// If the file or the specific settings section does not exist, it creates them.
    /// The updated settings are written back to the file in an indented JSON format.
    /// </remarks>
    public static void SaveBadgeCountToAppSettings(int badgeCount)
    {
        var appSettingsPath = AppSettingsPath();

        string json = ReadAllText(appSettingsPath);

        JsonNode? root = JsonNode.Parse(json) ?? new JsonObject();

        var badgeSettings = new BadgeSettings
        {
            BadgeCount = badgeCount
        };

        root[nameof(BadgeSettings)] = JsonSerializer.SerializeToNode(badgeSettings);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        WriteAllText(appSettingsPath, root.ToJsonString(options));
    }

    /// <summary>
    /// Retrieves the file path of the application settings file (appsettings.json).
    /// </summary>
    /// <returns>
    /// A string representing the absolute path to the appsettings.json file located in the application's base directory.
    /// </returns>
    /// <remarks>
    /// This method constructs the path to the appsettings.json file by combining the application's base directory
    /// with the file name "appsettings.json". It ensures that the application can locate its configuration file
    /// regardless of the current working directory.
    /// </remarks>
    private static string AppSettingsPath()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"); 
    }
}
