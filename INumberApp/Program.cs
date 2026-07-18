using ExtensionsLibrary;
using Spectre.Console;
using SpectreConsoleLibrary.Core;
using System.Globalization;
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression

namespace INumberApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        SpectreConsoleHelpers.PinkPill(Justify.Left, "INumber<T> examples");

        AnsiConsole.MarkupLine($"[yellow]         Max between 23 and 22 => [/]{int.MaxMagnitude(23, 22)}");
        AnsiConsole.MarkupLine($"[yellow]         Min between 23 and 22 => [/]{int.MinMagnitude(23, 22)}");
        AnsiConsole.MarkupLine($"[yellow]                  Is -8 positive? [/]{int.IsPositive(-8).ToYesNo()}");
        AnsiConsole.MarkupLine($"[yellow]                   Is 8 positive? [/]{int.IsPositive(8).ToYesNo()}");
        AnsiConsole.MarkupLine($"[yellow]                      Is -7 even? [/]{int.IsEvenInteger(-7).ToYesNo()}");
        AnsiConsole.MarkupLine($"[yellow]                      Is -8 even? [/]{int.IsEvenInteger(8).ToYesNo()}");
        AnsiConsole.MarkupLine($"[yellow]                       Is -7 odd? [/]{int.IsOddInteger(-7).ToYesNo()}");
        AnsiConsole.MarkupLine($"[yellow]                        Is 8 odd? [/]{int.IsOddInteger(8).ToYesNo()}");
        AnsiConsole.MarkupLine($"[yellow]                   Is Hour {DateTime.Now.Hour} odd? [/]{int.IsOddInteger(DateTime.Now.Hour).ToYesNo()}");
        AnsiConsole.MarkupLine($"[yellow]                            Clamp[/] {12.Clamp(1, 10)}");


        Console.WriteLine();

        var months = DateTimeFormatInfo.CurrentInfo.MonthNames[..^1]
            .Select((index, name) => new MonthItem(name + 1, index))
            .ToList();

        var table = new Table().Border(TableBorder.None);
        table.AddColumn("[cyan bold]Index[/]");
        table.AddColumn("[cyan bold]Month[/]");

        foreach (var month in months)
        {
            if (month.index.IsEven())
            {
                table.AddRow(month.index.ToString(), $"{month.name} :check_mark:");

            }
            else
            {
                table.AddRow(month.index.ToString(), $"[Grey]{month.name}[/]");
            }
        }

        AnsiConsole.Write(table);
        Console.WriteLine();
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

}

public record MonthItem(int index, string name);
