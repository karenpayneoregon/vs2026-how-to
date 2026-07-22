using ConsoleHelperLibrary.Classes;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;
using System.Reflection;
using System.Runtime.CompilerServices;
using ConsoleConfigurationLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using ScreenSaverApp.Classes.Configuration;
using SpectreConsoleLibrary.Core;


// ReSharper disable once CheckNamespace
namespace ScreenSaverApp
{
    partial class Program
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
}





