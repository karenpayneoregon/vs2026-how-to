namespace MemoryCollectionAppsettingsSample.Models;

/// <summary>
/// Represents the HelpDesk model containing contact information such as phone and email.
/// </summary>
/// <remarks>
/// This class is used to store and manage HelpDesk-related data, loaded from configuration settings.
/// </remarks>
public class HelpDesk
{
    public string? Phone { get; set; }
    public string? Email { get; set; }
}