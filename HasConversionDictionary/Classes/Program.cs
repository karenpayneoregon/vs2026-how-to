using ConsoleConfigurationLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using SpectreConsoleLibrary.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using ValueConversionsEncryptProperty.Classes;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace HasConversionDictionary;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        var assembly = Assembly.GetEntryAssembly();
        var product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

        Console.Title = product!;

        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
        Setup();
    }

    /// <summary>
    /// Configures and initializes the application's services, logging, and console settings.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Sets up development logging using <see cref="ValueConversionsEncryptProperty.Classes.SetupLogging.Development"/>.
    /// - Configures dependency injection services and retrieves required services.
    /// - Initializes connection strings and entity settings using <see cref="SetupServices"/>.
    /// - Configures the console's encoding using <see cref="SpectreConsoleLibrary.Core.SpectreConsoleHelpers.SetEncoding"/>.
    /// </remarks>
    private static void Setup()
    {

        SetupLogging.Development();

        var services = ConfigureServices();
        using var provider = services.BuildServiceProvider();
        var setup = provider.GetService<SetupServices>();
        setup!.GetConnectionStrings();
        setup.GetEntitySettings();

        SpectreConsoleHelpers.SetEncoding();
    }
}
