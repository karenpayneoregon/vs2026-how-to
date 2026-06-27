using ConsoleConfigurationLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using SpectreConsoleLibrary.Core;
using ValueConversionsEncryptProperty.Classes;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace ValueConversionsEncryptProperty;

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