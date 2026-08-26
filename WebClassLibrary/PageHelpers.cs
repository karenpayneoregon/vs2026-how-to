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
    /// Retrieves the name of the current page based on the provided HTTP request.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequest"/> object representing the current HTTP request. Cannot be <see langword="null"/>.</param>
    /// <returns>
    /// A <see cref="string"/> representing the name of the current page. 
    /// Returns "Index" if the request path is the root ("/"), otherwise returns the file name without its extension.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="request"/> is <see langword="null"/>.</exception>
    public static string GetCurrentPageName(HttpRequest request)
    {
        string path = request.Path;
        return path == "/" ? "Index" : Path.GetFileNameWithoutExtension(path);
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

