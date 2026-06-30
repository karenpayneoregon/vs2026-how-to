using SpectreConsoleLibrary.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using ConsoleConfigurationLibrary.Classes;
using ExcelMapperApp1.Classes;
using Microsoft.Extensions.DependencyInjection;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace ExcelMapperApp1;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Excel/EF Core Code sample";
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
