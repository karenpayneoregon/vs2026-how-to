using FormattableStringApp.Classes;
using FormattableStringApp.Classes.Core;
using FormattableStringApp.Models;
using Spectre.Console;
using System.Runtime.CompilerServices;
// ReSharper disable PossibleMultipleEnumeration

namespace FormattableStringApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        List<Person> list = SimulateFromDatabase();

        var items = FormattableStrings1(list);

        items.WriteMarkupLines();

        Console.WriteLine();

        var items1 = FormattableStrings2(list);

        items1.ForEach(AnsiConsole.MarkupLineInterpolated);
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Generates a collection of <see cref="FormattableString"/> objects from a list of <see cref="Person"/>.
    /// </summary>
    /// <param name="list">The list of <see cref="Person"/> objects to process.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="FormattableString"/> objects, 
    /// where each string represents a formatted representation of a person's details.
    /// </returns>
    /// <remarks>
    /// Each <see cref="FormattableString"/> is formatted using a specific markup style 
    /// for rendering in the console with Spectre.Console.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="list"/> is <c>null</c>.
    /// </exception>
    private static IEnumerable<FormattableString> FormattableStrings1(List<Person> list)
    {
        IEnumerable<FormattableString> items = list.Select(p =>
            FormattableStringFactory.Create("[DeepPink1]{0,-4}{1,-8}{2,-8}{3}[/]",
                p.Id, p.FirstName, p.LastName, p.BirthDate));
        return items;
    }
    
    /// <summary>
    /// Generates a collection of <see cref="FormattableString"/> objects from a list of <see cref="Person"/>.
    /// </summary>
    /// <param name="list">The list of <see cref="Person"/> objects to process.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="FormattableString"/> objects, 
    /// where each string represents a formatted representation of a person's details.
    /// </returns>
    /// <remarks>
    /// Each <see cref="FormattableString"/> is formatted using a specific markup style 
    /// for rendering in the console with Spectre.Console.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="list"/> is <c>null</c>.
    /// </exception>
    private static IEnumerable<FormattableString> FormattableStrings2(List<Person> list)
    {
        IEnumerable<FormattableString> items1 = list.Select(person =>
            FormattableStringFactory.Create(
                "[lightslateblue]{0,-4}{1,-12}{2,-12}{3}[/]",
                person.IdFormatted,
                person.FirstNameFormatted,
                person.LastNameFormatted,
                person.BirthDateFormatted));
        return items1;
    }

    /// <summary>
    /// Simulates retrieving a list of <see cref="Person"/> objects from a database.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="Person"/> objects representing the simulated database records.
    /// </returns>
    /// <remarks>
    /// This method generates a predefined collection of <see cref="Person"/> objects 
    /// with sample data for demonstration purposes.
    /// </remarks>
    private static List<Person> SimulateFromDatabase()
        =>
        [
            new() { Id = 1, FirstName = "Karen", LastName = "Payne", BirthDate = new(1962, 12, 7) },
            new() { Id = 2, FirstName = "Sam", LastName = "Smith", BirthDate = new(1972, 8, 15) },
            new() { Id = 3, FirstName = "Lucy", LastName = "Adams", BirthDate = new(1982, 2, 25) }
        ];

}