namespace WebApplication1.Models;

/// <summary>
/// Represents information about a Razor Page, including its name and path.
/// </summary>
public class PageInfo
{
    /// <summary>
    /// Gets or sets the name of the Razor Page.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the name of the Razor Page.
    /// </value>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the path of the Razor Page.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the path of the Razor Page.
    /// </value>
    public required string Path { get; set; }
}