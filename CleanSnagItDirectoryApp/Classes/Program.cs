using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace CleanSnagItDirectoryApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Clean SnagIt Directory";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
