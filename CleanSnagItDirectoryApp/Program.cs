using CleanSnagItDirectoryApp.Classes;


namespace CleanSnagItDirectoryApp;

internal partial class Program
{
    static void Main(string[] args)
    {
 
        var fileOps = new FileOperations();
        fileOps.DeleteFiles(SettingsOperations.GetFolderPath());
        AnsiConsole.MarkupLine("[yellow]Continue[/]");
        Console.ReadLine();
    }
}