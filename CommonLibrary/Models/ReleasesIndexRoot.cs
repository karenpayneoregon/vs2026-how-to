using System.Text.Json.Serialization;

namespace CommonLibrary.Models;

public sealed class ReleasesIndexRoot
{
    [JsonPropertyName("releases-index")]
    public List<ReleaseIndexItem>? ReleasesIndex { get; set; }
}