using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BootstrapComponentsApp.Pages;

public class BadgeDemoModel : PageModel
{
    [BindProperty]
    public int BadgeCount { get; set; } 

    public string BadgeDisplay => BadgeCount > 99 ? "99+" : BadgeCount.ToString();

    /// <summary>
    /// Initializes the <see cref="BadgeCount"/> property with a default value.
    /// In a real application, this could be replaced with logic to retrieve the count from a database or other data source.
    /// </summary>
    public void OnGet()
    {
        BadgeCount = 1;
    }

    /// <summary>
    /// Handles the POST request to increment the <see cref="BadgeCount"/> property.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> that renders the current page with the updated <see cref="BadgeCount"/> value.
    /// </returns>
    /// <remarks>
    /// This method increments the <see cref="BadgeCount"/> by 1 and clears the model state to ensure the updated value is displayed correctly.
    /// </remarks>
    public IActionResult OnPostIncrement()
    {
        
        BadgeCount++;
        ModelState.Clear();

        return Page();
        
    }
}