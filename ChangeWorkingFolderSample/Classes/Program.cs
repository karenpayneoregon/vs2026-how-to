using ChangeWorkingFolderSample.Models;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace ChangeWorkingFolderSample;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        var workingFolder = Config
            .Configuration
            .JsonRoot()
            .GetSection(nameof(AppSettings))[nameof(AppSettings.WorkingFolder)];


        if (Directory.Exists(workingFolder))
        {
            Directory.SetCurrentDirectory(workingFolder);
        }

        Console.Title = $"Code sample -> Work folder";

        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

    }
}
