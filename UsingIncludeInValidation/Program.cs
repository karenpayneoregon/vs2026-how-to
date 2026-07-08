using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UsingIncludeInValidation.Classes;
using UsingIncludeInValidation.Classes.Validators;

namespace UsingIncludeInValidation;

internal partial class Program
{
    static void Main(string[] args)
    {
        
        var people = MockedData.People();

        PersonValidator validator = new();

        foreach (var person in people)
        {
            AnsiConsole.MarkupLine($"{person} [cyan]{person.GetType().Name}[/]");

            var result = validator.Validate(person);
            if (result.IsValid)
            {
                AnsiConsole.MarkupLine("[yellow]Valid[/]");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    AnsiConsole.MarkupLine($"[red]   {error.ErrorMessage}[/]");
                }
            }

            Console.WriteLine();
            
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);

    }
}