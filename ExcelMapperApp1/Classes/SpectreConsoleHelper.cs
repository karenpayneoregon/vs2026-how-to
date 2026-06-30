namespace ExcelMapperApp1.Classes;

public static class SpectreConsoleHelper
{
    extension(string text)
    {
        public string Colorize()
            => text
                .Replace("{Person}", "[yellow]{Person}[/]")
                .Replace("{Address}", "[yellow]{Address}[/]");
    }
}
