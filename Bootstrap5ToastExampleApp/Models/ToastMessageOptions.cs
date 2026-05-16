namespace Bootstrap5ToastExampleApp.Models;

/// <summary>
/// Represents the options for configuring a toast message in the Bootstrap 5 Toast example application.
/// </summary>
public class ToastMessageOptions
{
    public string ToastMessage { get; set; } = string.Empty;
    public string ToastTitle { get; set; } = string.Empty;
    public int ToastDelay { get; set; }
}