namespace ChangeWorkingFolderSample;

internal partial class Program
{
    static void Main(string[] args)
    {
        
        AnsiConsole.MarkupLine($"[white]Current working folder:[/] [cyan]{Directory.GetCurrentDirectory()}[/]");
        AnsiConsole.MarkupLine($"     [white]Executable folder:[/] [cyan]{AppDomain.CurrentDomain.BaseDirectory}[/]");
        Console.ReadLine();
    }
}