using Spectre.Console;
using WatcherExample.Classes;
using WatcherExample.Classes.Core;

namespace WatcherExample;

internal partial class Program
{
    static void Main(string[] args)
    {
        const string folder = @"C:\OED\WatchMe";
        using var watcher = new FolderWatcher(folder);

        watcher.Start();

        SpectreConsoleHelpers.InfoPill(Justify.Left,$"Watching folder: {folder}");
        Console.WriteLine("Press Enter to quit.");

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

}
