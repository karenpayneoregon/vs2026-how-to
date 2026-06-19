using CommonLibrary;
using ExtensionsLibrary.Classes;
using Spectre.Console;
using SpectreConsoleLibrary.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionsApp.Classes;

internal class DateOnlySamples
{
    public static void PriorWeek()
    {

        SpectreConsoleHelpers.PrintPink();
        var list = DateOnlyHelpers.PriorWeeksDates();

        var table = new Table();
        table.Title("[cyan]Prior Week Dates[/]");

        table.AddColumn("Day of Week");
        table.AddColumn("Date");

        foreach (var date in list)
        {
            table.AddRow(date.DayOfWeek.ToString(), date.ToString("yyyy-MM-dd"));
        }

        AnsiConsole.Write(table);

        Console.WriteLine();
    }

    public static void NextWeek()
    {

        SpectreConsoleHelpers.PrintPink();
        var list = DateOnlyHelpers.NextWeeksDates();

        var table = new Table();
        table.Title("[cyan]Next Week Dates[/]");

        table.AddColumn("Day of Week");
        table.AddColumn("Date");

        foreach (var date in list)
        {
            table.AddRow(date.DayOfWeek.ToString(), date.ToString("yyyy-MM-dd"));
        }

        AnsiConsole.Write(table);

        Console.WriteLine();
    }
    
    public static void ThisWeek()
    {
        
        SpectreConsoleHelpers.PrintPink();
        var list = DateOnlyHelpers.GetCurrentWeekDates(DayOfWeek.Sunday);
        var table = new Table();
        table.Title("[cyan]This Week Dates[/]");
        table.AddColumn("Day of Week");
        table.AddColumn("Date");
        foreach (var date in list)
        {
            table.AddRow(date.DayOfWeek.ToString(), date.ToString("yyyy-MM-dd"));
        }

        AnsiConsole.Write(table);
        Console.WriteLine();
    }

    public static void GetMonthDays(int month)
    {
        SpectreConsoleHelpers.PrintPink();
        var table = CalendarTable.CreateMonthTable(6);
        AnsiConsole.Write(table);
    }
}
