using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClassLibrary;

namespace EnvironmentApplication.Pages;

public class IndexModel : PageModel
{
    public void OnGet()
    {
        var currentPageName = PageHelpers.GetCurrentPageName(Request);
    }
}
