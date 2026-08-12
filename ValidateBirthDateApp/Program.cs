using SpectreConsoleLibrary.Core;
using ValidateBirthDateApp.Models;
using ValidateBirthDateApp.Validators;

namespace ValidateBirthDateApp;

internal partial class Program
{
    static void Main()
    {
        InvalidPerson();
        Console.WriteLine();
        ValidPerson();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static void InvalidPerson()
    {

        SpectreConsoleHelpers.PrintPink();

        Person person = new()
        {

            BirthDate = new DateOnly(1845, 1, 1)
        };

        Console.WriteLine(ObjectDumper.Dump(person));

        PersonValidator validator = new();
        var validate = validator.Validate(person);
        if (validate.IsValid)
        {
            AnsiConsole.MarkupLine("[green]Valid[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Not valid[/]");
            foreach (var failure in validate.Errors)
            {
                AnsiConsole.MarkupLine($"   WithName: [white]{failure.ErrorMessage}[/]");
            }
        }
    }

    private static void ValidPerson()
    {

        SpectreConsoleHelpers.PrintPink();
        
        Person person = new()
        {
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateOnly(1980, 1, 1)
        };

        Console.WriteLine(ObjectDumper.Dump(person));


        PersonValidator validator = new();
        var validate = validator.Validate(person);
        if (validate.IsValid)
        {
            AnsiConsole.MarkupLine("[green]Valid[/]");
            
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Not valid[/]");
            foreach (var failure in validate.Errors)
            {
                AnsiConsole.MarkupLine($"   WithName: [white]{failure.ErrorMessage}[/]");
            }
        }
    }
}