using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using System.Reflection;

namespace WebClassLibrary;

/// <summary>
/// Provides helper methods for working with pages in an ASP.NET Core application.
/// </summary>
/// <remarks>
/// This class contains utility methods that assist in handling page-related operations, 
/// such as retrieving the current page name from an HTTP request.
/// </remarks>
public class PageHelpers
{
        
    /// <summary>
    /// Retrieves the name of the current page from the specified HTTP request.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequest"/> object representing the current HTTP request.</param>
    /// <returns>
    /// A <see cref="string"/> representing the name of the current page. 
    /// Returns "Index" if the request path is empty or the root ("/").
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="request"/> is <c>null</c>.</exception>
    public static string GetCurrentPageName(HttpRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var path = request.Path.Value;

        if (string.IsNullOrWhiteSpace(path) || path == "/")
        {
            return "Index";
        }

        return path.TrimStart('/');
    }

    public static List<string> GetPageNames()
    {
        var assembly = Assembly.GetEntryAssembly();

        if (assembly is null)
        {
            return [];
        }

        return assembly
            .GetTypes()
            .Where(type =>
                type.Name.EndsWith("Model", StringComparison.Ordinal) &&
                type.Namespace?.Contains(".Pages", StringComparison.Ordinal) == true)
            .Select(type => type.Name[..^"Model".Length])
            .Distinct()
            .OrderBy(name => name)
            .ToList();
    }
}

