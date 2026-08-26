using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using WebApplication1.Classes;
using WebApplication1.Models;
using static WebApplication1.Classes.EndPointHelpers;

namespace WebApplication1.Pages;

public class IndexModel(IEnumerable<EndpointDataSource> endpointSources, IActionDescriptorCollectionProvider provider) : PageModel
{
    private readonly IEnumerable<EndpointDataSource> _endpointSources = endpointSources;
    public required IEnumerable<RouteEndpoint> EndpointSources { get; set; }

    private readonly IActionDescriptorCollectionProvider _provider = provider;

    public List<PageInfo> Pages { get; private set; } = [];

    /// <summary>
    /// This method initializes the <see cref="EndpointSources"/> property by retrieving route endpoints
    /// from the provided endpoint data sources. It also logs the route pattern and display name of each endpoint.
    /// </summary>
    public void OnGet()
    {
        EndpointSources = EndPointHelpers.GetEndpoints(_endpointSources);
        foreach (var rep in EndpointSources)
        {
            Log.Information("{P1,-50} {P2}", rep.RoutePattern.RawText, rep.DisplayName);
        }

        Pages = GetPages(_provider);
        
        foreach (var page in Pages)
        {
            Log.Information("Page:  {Path}",  page.Path);
        }
    }
}
