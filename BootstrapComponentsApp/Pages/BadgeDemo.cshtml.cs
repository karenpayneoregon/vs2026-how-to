using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.IO.File;

namespace BootstrapComponentsApp.Pages
{
    public class BadgeDemoModel(IWebHostEnvironment environment) : PageModel
    {
        public int BadgeCount { get; set; }

        public string BadgeDisplay => BadgeCount > 99 ? "99+" : BadgeCount.ToString();

        public void OnGet()
        {
            BadgeCount = ReadBadgeCountFromAppSettings();
        }

        public IActionResult OnPostIncrement()
        {
            int currentBadgeCount = ReadBadgeCountFromAppSettings();

            currentBadgeCount++;

            SaveBadgeCountToAppSettings(currentBadgeCount);

            return RedirectToPage();
        }

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
        private int ReadBadgeCountFromAppSettings()
        {
            var appSettingsPath = AppSettingsPath();

            string json = ReadAllText(appSettingsPath);

            JsonNode? root = JsonNode.Parse(json);

            if (root == null)
            {
                return 1;
            }

            int? badgeCount = root["BadgeSettings"]?["BadgeCount"]?.GetValue<int>();

            return badgeCount ?? 1;
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
        private void SaveBadgeCountToAppSettings(int badgeCount)
        {
            var appSettingsPath = AppSettingsPath();

            string json = ReadAllText(appSettingsPath);

            JsonNode? root = JsonNode.Parse(json) ?? new JsonObject();

            root["BadgeSettings"] ??= new JsonObject();

            root["BadgeSettings"]!["BadgeCount"] = badgeCount;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            WriteAllText(appSettingsPath, root.ToJsonString(options));
        }

        private string AppSettingsPath()
        {
            string appSettingsPath = Path.Combine(environment.ContentRootPath, "appsettings.json");
            return appSettingsPath;
        }
    }
}







public class BadgeSettings
{
    public int BadgeCount { get; set; }
}
