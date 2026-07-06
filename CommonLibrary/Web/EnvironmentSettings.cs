using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommonLibrary.Web;

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

    /// <summary>
    /// Gets a value indicating whether the application is running in a development environment.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the application is running in a development environment; otherwise, <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// This property is initialized during the construction of the <see cref="EnvironmentSettings"/> singleton instance.
    /// It is determined based on the current hosting environment.
    /// </remarks>
    public bool IsDevelopment { get; init ; }
    /// <summary>
    /// Gets a value indicating whether the application is running in a production environment.
    /// </summary>
    /// <value>
    /// <c>true</c> if the application is running in a production environment; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// This property is initialized based on the current hosting environment. It is set to <c>true</c>
    /// if the environment is identified as production using the <see cref="HostEnvironmentExtensions.IsProductionEnvironment"/> method.
    /// </remarks>
    public bool IsProduction { get; init; }
    /// <summary>
    /// Gets the name of the current environment in which the application is running.
    /// </summary>
    /// <value>
    /// A string representing the current environment, such as "Development" or "Production".
    /// </value>
    /// <remarks>
    /// This property is initialized during the creation of the <see cref="EnvironmentSettings"/> instance
    /// and reflects whether the application is running in a development or production environment.
    /// </remarks>
    public string? CurrentEnvironment { get; init; }
    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentSettings"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is private to enforce the singleton pattern. It initializes the environment settings
    /// by determining whether the application is running in a development or production environment.
    ///
    /// Note colors for Spectre.Console: [yellow bold]Development[/] and [green bold]Production[/]
    /// </remarks>
    private EnvironmentSettings()
    {
        using IHost host = Host.CreateDefaultBuilder().Build();

        IHostEnvironment environment = host.Services.GetRequiredService<IHostEnvironment>();

        if (environment.IsDevelopmentEnvironment())
        {
            IsDevelopment = true;
            CurrentEnvironment = "[yellow bold]Development[/]";
        }

        if (environment.IsProductionEnvironment())
        {
            IsProduction = true;
            CurrentEnvironment = "[green bold]Production[/]";
        }
    }
}