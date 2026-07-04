using ConsoleConfigurationLibrary.Classes;
using EntityFrameworkLibrary;
using ExtensionsLibrary;
using HasConversionDictionary.Data;
using HasConversionDictionary.Models;
using SpectreConsoleLibrary.Core;

namespace HasConversionDictionary;

internal partial class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(DatabaseService.DatabaseName(AppConnections.Instance.MainConnection));
        using var context = new DictionaryContext();
        Prepare(context);
        ShowData(context);

        var keyToFind = "Anne";

        DictionaryItem item = context.Dictionary.FirstOrDefault(x => x.Data.Key == keyToFind)!;

        Console.WriteLine();
        if (item != null)
        {
            AnsiConsole.MarkupLine($"[Yellow]Found item with key[/] '{keyToFind}': " +
                $"[Yellow]Id[/] = {item.Id}, [Yellow]Key[/] = {item.Data.Key}, [Yellow]Value[/] = {item.Data.Value}");
        }
        else
        {
            AnsiConsole.MarkupLine($"[Red]Item with key '{keyToFind}' not found.[/]");
        }

        Console.WriteLine();

        keyToFind = "Jill";

        item = context.Dictionary.FirstOrDefault(x => x.Data.Key == keyToFind)!;

        Console.WriteLine();
        if (item != null)
        {
            AnsiConsole.MarkupLine($"[Yellow]Found item with key[/] '{keyToFind}': " +
                $"[Yellow]Id[/] = {item.Id}, [Yellow]Key[/] = {item.Data.Key}, [Yellow]Value[/] = {item.Data.Value}");
        }
        else
        {
            AnsiConsole.MarkupLine($"[Red]Item with key '{keyToFind}' not found.[/]");
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Displays the data from the <see cref="DictionaryContext"/> in a formatted manner.
    /// </summary>
    /// <param name="context">
    /// The <see cref="DictionaryContext"/> instance containing the data to be displayed.
    /// </param>
    /// <remarks>
    /// The method iterates through the items in the <see cref="DictionaryContext.Dictionary"/> DbSet
    /// and displays each item's details using different colors based on the index.
    /// </remarks>
    private static void ShowData(DictionaryContext context)
    {
        var items = context.Dictionary.ToList();
        foreach (var (index, item) in items.Index())
        {
            if (index.IsOdd())
            {
                AnsiConsole.MarkupLine($"[CornflowerBlue]{item.Id,-4}{item.Data.Key,-10}{item.Data.Value}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[Green1]{item.Id,-4}{item.Data.Key,-10}{item.Data.Value}[/]");
            }
        }
    }


    /// <summary>
    /// Prepares the specified <see cref="DictionaryContext"/> by ensuring the database is created and populated with initial data.
    /// </summary>
    /// <param name="context">The <see cref="DictionaryContext"/> instance used to interact with the database.</param>
    /// <remarks>
    /// If the database already exists, this method will not recreate it. Otherwise, it will delete any existing database,
    /// create a new one, and populate it with predefined dictionary entries.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the provided <paramref name="context"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the database creator service is not available or if a relational database creator is required but not present.
    /// </exception>
    private static void Prepare(DictionaryContext context)
    {

        //if (context.DatabaseExists())
        //{
        //    AnsiConsole.MarkupLine("[cyan]Database already exists[/] :check_mark:");
        //    Console.WriteLine("\n");
        //    return;
        //}

        AnsiConsole.MarkupLine("[cyan]Creating database[/]");

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        AnsiConsole.MarkupLine("[cyan]Database created...[/] :check_mark:");

        context.Add(new DictionaryItem() { Data = new DataEntity() { Key = "Karen", Value = "C#" } });
        context.Add(new DictionaryItem() { Data = new DataEntity() { Key = "Anne", Value = "C#" } });
        context.Add(new DictionaryItem() { Data = new DataEntity() { Key = "Mike", Value = "TypeScript" } });
        context.Add(new DictionaryItem() { Data = new DataEntity() { Key = "Sara", Value = "VB.NET" } });

        context.SaveChanges();
        AnsiConsole.MarkupLine("[cyan]Populated table[/] :check_mark:");

        Console.WriteLine("\n");
    }
}