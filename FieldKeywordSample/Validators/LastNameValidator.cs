using FieldKeywordSample.Interfaces;
using FluentValidation;

namespace FieldKeywordSample.Validators;

public class LastNameValidator : AbstractValidator<IPerson>
{
    public LastNameValidator()
    {
        RuleFor(person => person.LastName)
            .NotEmpty()
            .MinimumLength(3);
    }
}