#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace PropertyExistsApp.Classes;

public class HelpDesk
{
    public string Phone { get; set; }
    public string Email { get; set; }
}

public class Logging
{
    public required LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    public required string Default { get; set; }
    [JsonPropertyName("Microsoft.AspNetCore")]
    public required string MicrosoftAspNetCore { get; set; }
}