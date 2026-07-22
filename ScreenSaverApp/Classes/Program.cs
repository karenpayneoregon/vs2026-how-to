using ConsoleConfigurationLibrary.Classes;
using ConsoleHelperLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using ScreenSaverApp.Classes.Configuration;
using SpectreConsoleLibrary.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;
// ReSharper disable InconsistentNaming


// ReSharper disable once CheckNamespace
namespace ScreenSaverApp
{
    partial class Program
    {
        // 1. Import the SetThreadExecutionState function from kernel32.dll
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        // 2. Define the necessary flags for the execution state
        [Flags]
        private enum EXECUTION_STATE : uint
        {
            // Forces the system to be in the working state by resetting the system idle timer.
            ES_SYSTEM_REQUIRED = 0x00000001,

            // Forces the display to be on by resetting the display idle timer (Prevents Screen Saver).
            ES_DISPLAY_REQUIRED = 0x00000002,

            // Informs the system that the state being set should remain in effect until the next call.
            ES_CONTINUOUS = 0x80000000
        }
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





