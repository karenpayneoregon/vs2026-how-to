using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Nodes;
using BootstrapComponentsApp.Classes;
using BootstrapComponentsApp.Models;
using static System.IO.File;

namespace BootstrapComponentsApp.Pages
{
    public class BadgeDemoModel : PageModel
    {
        public int BadgeCount { get; set; }

        public string BadgeDisplay => BadgeCount > 99 ? "99+" : BadgeCount.ToString();

        public void OnGet()
        {
            BadgeCount = BadgeOperations.ReadBadgeCountFromAppSettings();
        }

        public IActionResult OnPostIncrement()
        {
            int currentBadgeCount = BadgeOperations.ReadBadgeCountFromAppSettings();

            currentBadgeCount++;

            BadgeOperations.SaveBadgeCountToAppSettings(currentBadgeCount);

            return RedirectToPage();
        }
    }
}