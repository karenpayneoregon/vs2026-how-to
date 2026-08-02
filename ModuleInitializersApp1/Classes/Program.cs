using ConsoleConfigurationLibrary.Classes;
using ConsoleHelperLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using ModuleInitializersApp1.Classes.Configuration;
using ModuleInitializersApp1.Classes.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using CommonLibrary;
using CommonLibrary.CustonExceptions;
using Spectre.Console;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace ModuleInitializersApp1;

internal partial class Program
{
    [ModuleInitializer]
    public static void MainSetup()
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

    [ModuleInitializer]
    public static void AppsettingsCheck()
    {
        if (!JsonHelpers.MainConnectionExists())
        {
            throw new MissingMainConnectionException();
        }
    }
}
