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
        BadgeCount = BadgeOperations.BadgeCount();
    }

    public IActionResult OnPostDecrement()
    {
        int currentBadgeCount = BadgeOperations.BadgeCount();

        currentBadgeCount--;

        if (currentBadgeCount < 0)
            currentBadgeCount = 0;
        
        BadgeOperations.Save(currentBadgeCount);

        return RedirectToPage();
    }
    public IActionResult OnPostIncrement()
    {
        int currentBadgeCount = BadgeOperations.BadgeCount();

        currentBadgeCount++;

        BadgeOperations.Save(currentBadgeCount);

        return RedirectToPage();
        
    }
}