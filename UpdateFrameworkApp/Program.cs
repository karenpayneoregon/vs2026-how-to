using Serilog;
using UpdateFrameworkApp.Classes;

namespace UpdateFrameworkApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        Log.Information("'{A}'", args[0]);
        var msg = ProjectUpdater.UpdateTargetFramework(args[0]);
        AnsiConsole.MarkupLine(msg);
        AnsiConsole.MarkupLine("[cyan]Press a key to close[/]");
        
        Console.ReadLine();
    }
}