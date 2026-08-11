using MatchPasswordsApp.Classes.Core;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using SpectreConsoleLibrary.Core;
using ValidationLibrary.Models;
using ValidationLibrary.Validators;

namespace MatchPasswordsApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        PasswordsDoNotMatch();
        Console.WriteLine();
        PasswordsMatch();
        Console.WriteLine();
        PasswordsAndUserNameIssues();
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Validates a <see cref="ValidationLibrary.Models.Person"/> object and displays the results.
    /// </summary>
    /// <remarks>
    /// This method creates a <see cref="ValidationLibrary.Models.Person"/> instance with predefined data,
    /// validates it using the <see cref="ValidationLibrary.Validators.PersonValidator"/> class,
    /// and displays success or error messages using <see cref="SpectreConsoleLibrary.Core.SpectreConsoleHelpers"/>.
    /// </remarks>
    private static void PasswordsDoNotMatch()
    {

        SpectreConsoleHelpers.PrintPink();

        Person person = new Person()
        {
            UserName = "JohnDoe",
            PhoneNumber = "123-456-7890",
            EmailAddress = "john.doe@example.com",
            Password = "Password123",
            PasswordConfirmation = "Password123!"
        };

        PersonValidator validator = new();
        
        var validate = validator.Validate(person);
        if (validate.IsValid)
        {
            SpectreConsoleHelpers.SuccessPill(Justify.Left, "User validation successful!");
        }
        else
        {
            validate.Errors.ForEach(e => 
                SpectreConsoleHelpers.ErrorPill(Justify.Left, e.ErrorMessage));
        }
    }

    /// <summary>
    /// Validates a <see cref="ValidationLibrary.Models.Person"/> object to ensure that the passwords match
    /// and displays the validation results using Spectre.Console helpers.
    /// </summary>
    /// <remarks>
    /// This method creates a <see cref="ValidationLibrary.Models.Person"/> instance, validates it using
    /// <see cref="ValidationLibrary.Validators.PersonValidator"/>, and provides feedback on whether the
    /// validation was successful or not.
    /// </remarks>
    private static void PasswordsMatch()
    {

        SpectreConsoleHelpers.PrintPink();

        Person person = new Person()
        {
            UserName = "JohnDoe",
            PhoneNumber = "123-456-7890",
            EmailAddress = "john.doe@example.com",
            Password = "Password123!",
            PasswordConfirmation = "Password123!"
        };

        PersonValidator validator = new();

        var validate = validator.Validate(person);
        if (validate.IsValid)
        {
            SpectreConsoleHelpers.SuccessPill(Justify.Left, "User validation successful!");
        }
        else
        {
            validate.Errors.ForEach(e => SpectreConsoleHelpers.ErrorPill(Justify.Left, e.ErrorMessage));
        }
    }

    /// <summary>
    /// Validates a <see cref="ValidationLibrary.Models.Person"/> object for potential issues with the username and passwords,
    /// and displays the validation results using Spectre.Console helpers.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Initializes a <see cref="ValidationLibrary.Models.Person"/> object with sample data.
    /// - Uses the <see cref="ValidationLibrary.Validators.PersonValidator"/> to validate the object.
    /// - Displays a success message if validation passes, or error messages for each validation failure.
    /// </remarks>
    private static void PasswordsAndUserNameIssues()
    {

        SpectreConsoleHelpers.PrintPink();

        Person person = new Person()
        {
            UserName = "",
            PhoneNumber = "123-456-7890",
            EmailAddress = "john.doe@example.com",
            Password = "Pasword123!",
            PasswordConfirmation = "Password123!"
        };

        PersonValidator validator = new();

        var validate = validator.Validate(person);
        if (validate.IsValid)
        {
            SpectreConsoleHelpers.SuccessPill(Justify.Left, "User validation successful!");
        }
        else
        {
            validate.Errors.ForEach(e => SpectreConsoleHelpers.ErrorPill(Justify.Left, e.ErrorMessage));
        }
    }
}
