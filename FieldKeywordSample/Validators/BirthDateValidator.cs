using FieldKeywordSample.Interfaces;
using FluentValidation;

namespace FieldKeywordSample.Validators;

public class BirthDateValidator : AbstractValidator<IPerson>
{

    public BirthDateValidator()
    {

        var minYear = 1977;

        RuleFor(x => x.BirthDate)
            .Must(x => x.Year > minYear && x.Year <= DateTime.Now.Year)
            .WithMessage($"Birth date must be greater than {minYear} " +
                         $"year and less than or equal to {DateTime.Now.Year} ");
    }
}