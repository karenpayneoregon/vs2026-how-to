using BootstrapComponentsApp.Classes;
using BootstrapComponentsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BootstrapComponentsApp.Pages
{
    public class PopOverDemoModel : PageModel
    {

        public List<PopOverContainer> PopContainer { get; set; } = MockedData.PopOverList();

        public void OnGet()
        {
        }
    }
}
