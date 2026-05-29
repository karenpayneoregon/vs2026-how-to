using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace DeleteDuplicateRowsSqlServerTable;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }

    /// <summary>
    /// Creates and configures a new instance of a <see cref="Table"/> with predefined columns, 
    /// border styles, and a title for displaying data.
    /// </summary>
    /// <returns>
    /// A <see cref="Table"/> instance with columns for "Id", "First", "Last", and "Birth", 
    /// styled with a rounded border, centered alignment, and a light slate grey border color.
    /// </returns>
    /// <remarks>
    /// This method is used to initialize a table for displaying data with a consistent format 
    /// and styling. The table is configured using Spectre.Console features.
    /// </remarks>
    public static Table CreateTable()
        => new Table()
            .RoundedBorder()
            .Centered()
            .AddColumn("[cyan]Id[/]")
            .AddColumn("[cyan]First[/]")
            .AddColumn("[cyan]Last[/]")
            .AddColumn("[cyan]Birth[/]")
            .BorderColor(Color.LightSlateGrey)
            .Border(TableBorder.Square)
            .Title("[LightGreen]Before/After[/]");
}

