using FluentValidation;
using ValidateBirthDateApp.Interfaces;

namespace ValidateBirthDateApp.Validators;

public class FirstLastNameValidator : AbstractValidator<IPerson>
{
    public FirstLastNameValidator()
    {

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MinimumLength(3)
            .WithMessage("First name must be at least 3 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MinimumLength(3)
            .WithMessage("Last name must be at least 3 characters long.");

    }
}