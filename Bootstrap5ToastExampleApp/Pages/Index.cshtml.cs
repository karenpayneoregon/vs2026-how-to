using Bootstrap5ToastExampleApp.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bootstrap5ToastExampleApp.Pages;
/// <summary>
/// This class is responsible for handling the logic related to displaying Bootstrap 5 toasts
/// on the Index page. It uses dependency injection to retrieve toast options and manages
/// TempData properties for toast message, title, and delay.
/// </summary>
public class IndexModel(ReadToastConfiguration readToast) : PageModel
{
    [TempData]
    public string? ToastMessage { get; set; }

    [TempData]
    public string? ToastTitle { get; set; }

    [TempData]
    public int ToastDelay { get; set; }

    public void OnGet()
    {
    }

    /// <summary>
    /// Handles the POST request to display a Bootstrap 5 toast notification.
    /// </summary>
    /// <remarks>
    /// This method retrieves toast options, including the title, message, and delay, 
    /// from the <see cref="ReadToastConfiguration"/> service. It assigns these values 
    /// to the corresponding TempData properties and redirects the user back to the 
    /// current page to trigger the display of the toast notification.
    /// </remarks>
    /// <returns>
    /// An <see cref="IActionResult"/> that redirects to the current page.
    /// </returns>
    public IActionResult OnPostShowToast()
    {
        var options = readToast.GetToastOptions();
        
        ToastTitle = options.DatabaseError.ToastTitle;
        ToastMessage = options.DatabaseError.ToastMessage;
        ToastDelay = options.DatabaseError.ToastDelay;

        return RedirectToPage();
        
    }
}