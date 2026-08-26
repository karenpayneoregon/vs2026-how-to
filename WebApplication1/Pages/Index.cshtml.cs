using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using WebClassLibrary;
using WebClassLibrary.Models;

namespace WebApplication1.Pages;

public class IndexModel(IEnumerable<EndpointDataSource> endpointSources, IActionDescriptorCollectionProvider provider) : PageModel
{
    public required IEnumerable<RouteEndpoint> EndpointSources { get; set; }

    public List<PageInfo> Pages { get; private set; } = [];

    /// <summary>
    /// This method initializes the <see cref="EndpointSources"/> property by retrieving route endpoints
    /// from the provided endpoint data sources. It also logs the route pattern and display name of each endpoint.
    /// </summary>
    public void OnGet()
    {
      
        EndpointSources = EndPointHelpers.GetEndpoints(endpointSources);
        
        foreach (var rep in EndpointSources)
        {
            Log.Information("{P1,-50} {P2}", rep.RoutePattern.RawText, rep.DisplayName);
        }

        Pages = EndPointHelpers.GetPages(provider);
        
        foreach (var page in Pages)
        {
            Log.Information("Page:  {Path}",  page.Path);
        }
        
    }
}
