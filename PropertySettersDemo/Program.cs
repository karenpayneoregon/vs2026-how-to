using ExperimentsApp.Models;
using PropertySettersDemo.Classes.Core;
using Spectre.Console;

namespace PropertySettersDemo;

internal partial class Program
{
    /*
     * Set a breakpoint on the first line of PropertySetters.SetProperty method im CommonLibrary to see how it works.
     */
    static void Main(string[] args)
    {

        var human = new Human
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateOnly(1990, 1, 1)
        };

        human.FirstName = "John"; // This will not trigger PropertyChanged event because the value is the same
        human.FirstName = "Mary"; // This will trigger PropertyChanged event because the value is different

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

}
