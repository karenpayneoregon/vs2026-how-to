using EnumHasConversion.Data;
using EnumHasConversion.Models;
using static EnumHasConversion.Classes.SpectreConsoleHelpers;

namespace EnumHasConversion.Classes;

public class WineOperations
{
    /// <summary>
    /// Retrieves a list of wines filtered by the specified wine type.
    /// </summary>
    /// <param name="wineType">The type of wine to filter by. See <see cref="WineType"/> for available options.</param>
    /// <returns>A list of <see cref="Wine"/> objects that match the specified wine type.</returns>
    /// <remarks>
    /// This method uses the <see cref="WineContext"/> to query the database for wines of the specified type.
    /// </remarks>
    public static List<Wine> GetWinesByType(WineType wineType)
    {
        using var context = new WineContext();
        return context.Wines
            .Where(wine => wine.WineType == wineType)
            .ToList();
    }
    
    
    /// <summary>
    /// Demonstrates various examples of grouping and filtering operations on wine data.
    /// </summary>
    /// <remarks>
    /// This method performs the following operations:
    /// - Groups wines by their type and displays the grouped results.
    /// - Filters wines by specific types (e.g., Rose, Red) and displays the filtered results.
    /// - Displays all wines in the database.
    /// </remarks>
    public static void RunExamples()
    {

        using var context = new WineContext();
            
        LineSeparator("[white]Grouped 1[/]");

        List<WineGroupItem> allWinesGrouped1 = context.Wines
            .GroupBy( wine => wine.WineType)
            .Select(w => new WineGroupItem(w.Key, w.ToList()))
            .ToList();

        Dictionary<WineType, List<Wine>> allWinesGrouped2 = context.Wines.GroupBy(x => x.WineType)
            .ToDictionary(k => k.Key, v => v.ToList());

        foreach (WineGroupItem item in allWinesGrouped1)
        {
            AnsiConsole.MarkupLine($"[cyan]{item.Type}[/]");
            foreach (var wine in item.List)
            {
                Console.WriteLine($"\t{wine.WineId, -5}{wine.Name}");
            }
        }
        
        LineSeparator("[white]Grouped 2[/]");

        foreach (KeyValuePair<WineType, List<Wine>> kvp in allWinesGrouped2)
        {
            AnsiConsole.MarkupLine($"[cyan]{kvp.Key}[/]");
            foreach (var wine in kvp.Value)
            {
                Console.WriteLine($"\t{wine.WineId,-5}{wine.Name}");
            }
        }

        List<Wine> allWines = [.. context.Wines];
        
        LineSeparator("[white]All[/]");

        foreach (var wine in allWines)
        {
            AnsiConsole.MarkupLine($"[cyan]{wine.WineType,-8}[/]{wine.Name}");
        }

        List<Wine> rose = [.. context.Wines.Where(wine => wine.WineType == WineType.Rose)];

        LineSeparator("[white]Rose[/]");

        if (rose.Count == 0)
        {
            Console.WriteLine("\tNone");
        }
        else
        {
            foreach (var wine in rose)
            {
                Console.WriteLine($"{wine.Name,30}");
            }
        }
        
        LineSeparator("[white]Red[/]");


        List<Wine> red = [.. context.Wines.Where(wine => wine.WineType == WineType.Red)];

        foreach (Wine wine in red)
        {
            Console.WriteLine($"{wine.Name,30}");
        }

    }
}