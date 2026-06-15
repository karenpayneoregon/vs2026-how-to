using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BootstrapComponentsApp.Classes;

namespace BootstrapComponentsApp.Pages;

public class BadgeDemoModel : PageModel
{
    /// <summary>
    /// Gets or sets the current badge count displayed on the page.
    /// </summary>
    /// <value>
    /// An integer representing the badge count. This value is used to determine the display of the badge,
    /// such as showing "99+" for counts greater than 99.
    /// </value>
    /// <remarks>
    /// The badge count is updated dynamically based on user interactions, such as incrementing or decrementing
    /// the count through the corresponding POST methods.
    /// </remarks>
    public int BadgeCount { get; set; }

    /// <summary>
    /// Gets the display value for the badge, formatting the badge count appropriately.
    /// </summary>
    /// <value>
    /// A string representing the badge count. If the count exceeds 99, it returns "99+"; 
    /// otherwise, it returns the count as a string.
    /// </value>
    /// <remarks>
    /// This property dynamically formats the badge count for display purposes. 
    /// It ensures that counts greater than 99 are represented as "99+" to maintain a concise display.
    /// </remarks>
    public string BadgeDisplay => BadgeCount > 99 ? "99+" : BadgeCount.ToString();

    public void OnGet()
    {
        BadgeCount = BadgeOperations.BadgeCount();
    }

    /// <summary>
    /// Handles the POST request to decrement the badge count.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> that redirects to the current page after updating the badge count.
    /// </returns>
    /// <remarks>
    /// This method retrieves the current badge count using <see cref="BootstrapComponentsApp.Classes.BadgeOperations.BadgeCount"/>.
    /// It decrements the count, ensuring it does not go below zero, and then saves the updated count
    /// using <see cref="BootstrapComponentsApp.Classes.BadgeOperations.Save(int)"/>. Finally, it redirects to the current page.
    /// </remarks>
    public IActionResult OnPostDecrement()
    {
        int currentBadgeCount = BadgeOperations.BadgeCount();

        currentBadgeCount--;

        if (currentBadgeCount < 0)
            currentBadgeCount = 0;
        
        BadgeOperations.Save(currentBadgeCount);

        return RedirectToPage();
    }
    
    
    /// <summary>
    /// Handles the POST request to increment the badge count.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> that redirects to the current page after updating the badge count.
    /// </returns>
    /// <remarks>
    /// This method retrieves the current badge count using <see cref="BadgeOperations.BadgeCount"/>,
    /// increments it by one, and then saves the updated count using <see cref="BadgeOperations.Save(int)"/>.
    /// Finally, it redirects to the current page to reflect the updated badge count.
    /// </remarks>
    public IActionResult OnPostIncrement()
    {
        int currentBadgeCount = BadgeOperations.BadgeCount();

        currentBadgeCount++;

        BadgeOperations.Save(currentBadgeCount);

        return RedirectToPage();
        
    }
}