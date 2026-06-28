using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnvironmentApplication.Pages;

public class IndexModel : PageModel
{
    public void OnGet()
    {
        var currentPageName = WebClassLibrary.PageHelpers.GetCurrentPageName(Request);
    }
}
