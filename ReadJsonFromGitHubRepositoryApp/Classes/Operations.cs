using System.Text.Json;
using Serilog;

namespace ReadJsonFromGitHubRepositoryApp.Classes;

public class Operations
{
    /// <summary>`
    /// Asynchronously loads a list of states from a JSON resource located at the specified URL.
    /// </summary>
    /// <param name="url">The URL of the JSON resource to load.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a list of <see cref="State"/> objects deserialized from the JSON resource.</returns>
    /// <exception cref="HttpRequestException">Thrown if there is an error while sending the HTTP request or receiving the response.</exception>
    /// <exception cref="JsonException">Thrown if there is an error during JSON deserialization.</exception>
    public static async Task<(bool, List<State>)> LoadStatesFromUrlAsync(string url)
    {
        try
        {
            var json = await Client.GetStringAsync(url);

            var states = JsonSerializer.Deserialize<List<State>>(json, CachedJsonSerializerOptions);

            return (true, states ?? new List<State>());
        }
        catch (Exception e)
        {
            Log.Error(e, "An error occurred while loading states from URL: {Url}", url);
            return (false,new List<State>());
        }
    }

    private static readonly JsonSerializerOptions CachedJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly HttpClient Client = new();
    
}