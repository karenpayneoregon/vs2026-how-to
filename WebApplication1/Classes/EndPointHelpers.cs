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
}
