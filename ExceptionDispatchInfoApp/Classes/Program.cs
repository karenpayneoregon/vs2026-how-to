using ExceptionDispatchInfoApp.Classes;
using SpectreConsoleLibrary.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace ExceptionDispatchInfoApp;
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

        SpectreConsoleHelpers.SetEncoding();
    }
}
