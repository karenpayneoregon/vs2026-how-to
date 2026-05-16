using Microsoft.AspNetCore.Mvc;

namespace Bootstrap5ToastExampleApp.Models;

/// <summary>
/// Represents the configuration options for toast notifications in the Bootstrap 5 Toast example application.
/// </summary>
/// <remarks>
/// This class is used to define and manage toast notification settings, such as error messages or other
/// predefined toast configurations. It is typically configured via dependency injection and appsettings.json.
/// </remarks>
public class ToastOptions
{
    /// <summary>
    /// Gets or sets the options for configuring a toast message that represents a database error.
    /// </summary>
    /// <remarks>
    /// This property provides the configuration for a toast notification that is displayed
    /// when a database error occurs. It includes settings such as the title, message, and delay
    /// for the toast notification.
    /// </remarks>
    public ToastMessageOptions DatabaseError { get; set; } = new();
}