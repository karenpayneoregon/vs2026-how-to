using Spectre.Console;

namespace ConversionsApp.Classes.Core;

internal class WindowHelpers
{
    public static void CenterLines(params string[] lines)
    {

        int verticalStart = (Console.WindowHeight - lines.Length) / 2;
        int verticalPosition = verticalStart;

        foreach (var line in lines)
        {
            int horizontalStart = (Console.WindowWidth - line.Length) / 2;
            Console.SetCursorPosition(horizontalStart, verticalPosition);
            AnsiConsole.MarkupLine(line);
            ++verticalPosition;
        }
    }
}