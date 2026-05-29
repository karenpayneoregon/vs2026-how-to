using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace UpdateFrameworkApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "NET9 to NET10";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
