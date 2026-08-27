using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClassLibrary;

namespace WebApplication1.Areas.Readme.Pages
{
    public class readmoreRightModel : PageModel
    {
        public void OnGet()
        {
           var currentPageName = PageHelpers.GetCurrentPageName(Request); 
        }
    }
}
