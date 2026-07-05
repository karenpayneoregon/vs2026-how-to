using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ParameterizedCollectionModeSample.Classes;

/// <summary>
/// Represents a singleton class for managing environment settings specific to the application.
/// </summary>
/// <remarks>
/// This class is designed to handle environment-specific configurations, such as determining 
/// whether the application is running in a development or production environment. 
/// It ensures a single shared instance is used throughout the application, adhering to the singleton pattern.
/// </remarks>
public sealed class EnvironmentSettings
{
    private static readonly Lazy<EnvironmentSettings> Lazy = new(() => new EnvironmentSettings());
    public static EnvironmentSettings Instance => Lazy.Value;

    public bool IsDevelopment { get; set; }
    public bool IsProduction { get; set; }
    public string CurrentEnvironment { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentSettings"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is private to enforce the singleton pattern. It initializes the environment settings
    /// by determining whether the application is running in a development or production environment.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <see cref="IHostEnvironment"/> service is not available or is null.
    /// </exception>
    private EnvironmentSettings()
    {
        using IHost host = Host.CreateDefaultBuilder().Build();

        IHostEnvironment environment = host.Services.GetRequiredService<IHostEnvironment>();

        if (environment.IsDevelopmentEnvironment())
        {
            IsDevelopment = true;
            CurrentEnvironment = "Development";
        }

        if (environment.IsProductionEnvironment())
        {
            IsProduction = true;
            CurrentEnvironment = "Production";
        }
    }
}