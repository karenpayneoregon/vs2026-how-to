using System.Runtime.CompilerServices;
using UpdateFrameworkApp.Classes;

// ReSharper disable once CheckNamespace
namespace UpdateFrameworkApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "NET9 to NET10";
        SetupLogging.Development();
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
