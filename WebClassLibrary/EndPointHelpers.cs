using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using WebClassLibrary.Models;

namespace WebClassLibrary;

/// <summary>
/// Provides helper methods for working with endpoint data sources and route endpoints.
/// </summary>
public class EndPointHelpers
{

    /// <summary>
    /// Retrieves a collection of <see cref="RouteEndpoint"/> objects from the provided endpoint data sources.
    /// </summary>
    /// <param name="endpointSources">
    /// A collection of <see cref="EndpointDataSource"/> objects from which to extract route endpoints.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="RouteEndpoint"/> objects that have non-empty route patterns.
    /// </returns>
    public static IEnumerable<RouteEndpoint> GetEndpoints(IEnumerable<EndpointDataSource> endpointSources)
    {
        return endpointSources
            .SelectMany(x => x.Endpoints)
            .OfType<RouteEndpoint>()
            .Where(x => !string.IsNullOrWhiteSpace(x.RoutePattern.RawText));
    }



    /// <summary>
    /// Retrieves a list of Razor Page names from the provided action descriptor collection provider.
    /// </summary>
    /// <param name="provider">
    /// An instance of <see cref="IActionDescriptorCollectionProvider"/> used to access action descriptors.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> representing the names of Razor Pages, 
    /// ordered alphabetically by their view engine paths.
    /// </returns>
    public static List<string> GetPageNames(IActionDescriptorCollectionProvider provider)
    {
        var pages = provider
            .ActionDescriptors
            .Items
            .OfType<PageActionDescriptor>()
            .Select(page => page.ViewEnginePath)
            .OrderBy(page => page)
            .ToList();
        return pages;
    }

    /// <summary>
    /// Retrieves a collection of <see cref="PageInfo"/> objects representing Razor Pages
    /// from the provided action descriptor collection provider.
    /// </summary>
    /// <param name="provider">
    /// An instance of <see cref="IActionDescriptorCollectionProvider"/> used to access action descriptors.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="PageInfo"/> objects, each containing the name and path of a Razor Page.
    /// </returns>
    public static List<PageInfo> GetPages(IActionDescriptorCollectionProvider provider)
    {
        
        var pages = provider
            .ActionDescriptors
            .Items
            .OfType<PageActionDescriptor>()
            .Select(page => new PageInfo
            {
                Name = page.ViewEnginePath,
                Path = page.RelativePath
            })
            .OrderBy(page => page.Name)
            .ToList();
        
        return pages;
    }


}