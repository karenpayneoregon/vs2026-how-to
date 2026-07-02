using ConsoleConfigurationLibrary.Classes;
using HasConversion_Bool_ColorApp.Classes;
using Microsoft.Extensions.DependencyInjection;
using SpectreConsoleLibrary.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;
// ReSharper disable once CheckNamespace
namespace HasConversion_Bool_ColorApp;
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

    private static Table CreateTable()
    {
        return new Table()
            .RoundedBorder().BorderColor(Color.LightSlateGrey)
            .AddColumn("[b]Id[/]")
            .AddColumn("[b]First[/]")
            .AddColumn("[b]Last[/]")
            .AddColumn("[b]Is-string[/]")
            .AddColumn("[b]Is-bool[/]")
            .AddColumn("[b]Color[/]")
            .AddColumn("[b]Born[/]")
            .Title("[white on blue]Friends[/]");
    }
}
