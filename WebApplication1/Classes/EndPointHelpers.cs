using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApplication1.Models;

namespace WebApplication1.Classes;

/// <summary>
/// Provides helper methods for working with endpoint data sources and route endpoints.
/// </summary>
public class EndPointHelpers
{
    /// <summary>
    /// Retrieves a collection of <see cref="RouteEndpoint"/> instances from the provided endpoint sources.
    /// </summary>
    /// <param name="endpointSources">
    /// A collection of <see cref="EndpointDataSource"/> objects from which the endpoints will be extracted.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> containing the <see cref="RouteEndpoint"/> instances found in the provided sources.
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