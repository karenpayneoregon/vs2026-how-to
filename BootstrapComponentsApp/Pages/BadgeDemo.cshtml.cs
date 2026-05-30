using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BootstrapComponentsApp.Classes;

namespace BootstrapComponentsApp.Pages;

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