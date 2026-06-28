using HasConversionDictionary.Data;
using HasConversionDictionary.Models;

namespace HasConversionDictionary;

internal partial class Program
{
    static void Main(string[] args)
    {
        using var context = new DictionaryContext();
        //AddItems(context);
        Show(context);
        Dictionary item = context.Dictionary.FirstOrDefault(x => x.Data.Key == "Karen")!;


        Console.ReadLine();
        
    }

    private static void Show(DictionaryContext context)
    {
        var items = context.Dictionary.ToList();
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Id,-4}{item.Data.Key,-10}{item.Data.Value}");
        }
    }

    private static void AddItems(DictionaryContext context)
    {
        
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Add(new Dictionary() { Data = new DataEntity() { Key = "Karen", Value = "C#" } });
        context.Add(new Dictionary() { Data = new DataEntity() { Key = "Anne", Value = "TypeScript" } });
        context.Add(new Dictionary() { Data = new DataEntity() { Key = "Mike", Value = "VB.NET" } });
        context.SaveChanges();
    }
}