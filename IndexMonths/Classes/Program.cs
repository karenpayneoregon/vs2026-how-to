using SpectreConsoleLibrary.Core;
using System.Runtime.CompilerServices;


// ReSharper disable once CheckNamespace
namespace IndexMonths;

internal partial class Program
{
    
    [ModuleInitializer]
    public static void Init()
    {
        SpectreConsoleHelpers.SetEncoding();
        Console.Title = "Month indexing";
        //WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
        
    /// <summary>
    /// Creates and configures a new table for displaying month indexing details.
    /// </summary>
    /// <returns>
    /// A <see cref="Table"/> instance with predefined columns and styling for displaying month names, indices, and ranges.
    /// </returns>
    private static Table CreateTable()
    {
        return new Table()
            .RoundedBorder().BorderColor(Color.LightSlateGrey)
            .AddColumn("[b]Name[/]")
            .AddColumn("[b]Index[/]")
            .AddColumn("[b]Start Index[/]")
            .AddColumn("[b]End Index[/]")
            .BorderColor(Color.Blue);
    }
   

}