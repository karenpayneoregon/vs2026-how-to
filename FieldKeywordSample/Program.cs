using FieldKeywordSample.Classes;
using FieldKeywordSample.Models;
using FieldKeywordSample.Validators;
using SpectreConsoleLibrary.Core;

namespace FieldKeywordSample;

internal partial class Program
{
    static void Main(string[] args)
    {
        var people = MockedData.People();

        foreach (var person in people)
        {
            {

                if (person is Customer customer)
                {
                    AnsiConsole.MarkupLine($"[cyan]{customer.Id} {customer.FirstName} {customer.LastName} {customer.BirthDate}[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[green]{person.Id} {person.FirstName} {person.LastName} {person.BirthDate}[/]");
                }
            }

            Console.WriteLine();

            {
                if (person is not Customer customer) continue;

                AnsiConsole.MarkupLine("[cyan]Customer[/]");
                Console.WriteLine($"    Customer ID: {customer.CustomerId}");
                Console.WriteLine();

                if (customer.Addresses is null) continue;

                AnsiConsole.MarkupLine("[cyan]    Address[/]");
                foreach (var address in customer.Addresses)
                {
                    Console.WriteLine($"        {address.FullAddress}");
                }

            }

            Console.WriteLine();
        }


        ValidatePerson();


        AnsiConsole.MarkupLine(":right_arrow:  [yellow]Exit[/]");
        Console.ReadLine();
    }

    private static void ValidatePerson()
    {
        SpectreConsoleHelpers.PrintPink();
        
        var person = new Validators.Person
        {
            Id = 1,
            FirstName = "john",
            LastName = "doe",
            BirthDate = new DateOnly(1998, 1, 7)
        };
        
        var validator = new PersonValidator();
        var validationResult = validator.Validate(person);

        if (!validationResult.IsValid)
        {
            AnsiConsole.MarkupLine("[red]Validation Failed:[/]");
            foreach (var error in validationResult.Errors)
            {
                AnsiConsole.MarkupLine($"  - [yellow]{error.PropertyName}[/]: {error.ErrorMessage}");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[green]Validation Successful![/]");
        }

        Console.WriteLine();
    }
}