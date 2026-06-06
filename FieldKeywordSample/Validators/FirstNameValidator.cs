using FieldKeywordSample.Interfaces;
using FluentValidation;

namespace FieldKeywordSample.Validators;

public class FirstNameValidator : AbstractValidator<IPerson>
{
    public FirstNameValidator()
    {
        RuleFor(person => person.FirstName)
            .NotEmpty()
            .MinimumLength(3);
    }
}