using FluentValidation;
using UsingIncludeInValidation.Interfaces;

namespace UsingIncludeInValidation.Classes.Validators;

/// <summary>
/// Provides validation rules for the <see cref="IPerson.FirstName"/> property.
/// </summary>
/// <remarks>
/// This validator ensures that the first name of an <see cref="IPerson"/> instance:
/// <list type="bullet">
/// <item><description>Is not empty.</description></item>
/// <item><description>Has a minimum length of 3 characters.</description></item>
/// </list>
/// If the validation fails, an appropriate error message is generated.
/// </remarks>
public class FirstNameValidator : AbstractValidator<IPerson>
{
    public FirstNameValidator()
    {
        RuleFor(person => person.FirstName)
            .NotEmpty()
            .MinimumLength(3);
    }
}