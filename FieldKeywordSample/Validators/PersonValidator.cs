using FieldKeywordSample.Interfaces;
using FluentValidation;

namespace FieldKeywordSample.Validators;

public class PersonValidator : AbstractValidator<IPerson>
{
    public PersonValidator()
    {
        Include(new FirstNameValidator());
        Include(new LastNameValidator());
        Include(new BirthDateValidator());
    }
}