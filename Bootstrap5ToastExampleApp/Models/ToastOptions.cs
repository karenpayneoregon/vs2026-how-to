using Microsoft.AspNetCore.Mvc;

namespace Bootstrap5ToastExampleApp.Models;

/// <summary>
/// Represents the configuration options for a Bootstrap 5 toast notification.
/// </summary>
/// <remarks>
/// This class is used to define the properties required to configure and display a toast notification,
/// such as the message, title, and delay duration.
/// </remarks>
public class ToastOptions
{
    public string? ToastMessage { get; set; }

    public string? ToastTitle { get; set; }

    public int ToastDelay { get; set; }
}
