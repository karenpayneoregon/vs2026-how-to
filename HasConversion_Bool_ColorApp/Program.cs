using EntityFrameworkLibrary;
using HasConversion_Bool_ColorApp.Classes;
using HasConversion_Bool_ColorApp.Data;
using SpectreConsoleLibrary.Core;

namespace HasConversion_Bool_ColorApp;

internal partial class Program
{
    private static void Main(string[] args)
    {

        using var context = new Context();

        if (!context.DatabaseExists())
        {
            MockData.StartFresh(context);
        }

        var people = context.People.ToList();

        var table = CreateTable();

        foreach (var person in people)
        {
            table.AddRow(
                person.Id.ToString(),
                person.FirstName,
                person.LastName,
                person.IsFriend.ToYesNo(), 
                person.IsFriend.ToString(),
                $"[{person.Color.Name}]{person.Color.Name}[/]",
                person.BirthDate.ToString());
        }
        
        AnsiConsole.Write(table);
        
        Console.WriteLine();
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}