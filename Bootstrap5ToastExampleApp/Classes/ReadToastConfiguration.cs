using Bootstrap5ToastExampleApp.Models;
using Microsoft.Extensions.Options;

namespace Bootstrap5ToastExampleApp.Classes;

/// <summary>
/// Provides functionality to read and retrieve toast notification configuration options.
/// </summary>
/// <remarks>
/// This class is designed to work with the <see cref="ToastOptions"/> model, utilizing dependency injection
/// to access the configured options. It simplifies the process of retrieving toast settings for use in
/// Bootstrap 5 toast notifications.
/// </remarks>
public class ReadToastConfiguration(IOptions<ToastOptions> options)
{
    private readonly ToastOptions _toastOptions = options.Value;

    public ToastOptions GetToastOptions()
    {
        return _toastOptions;
    }
}