using FluentValidation;
using Microsoft.Extensions.Configuration;
using UsingIncludeInValidation.Interfaces;
using UsingIncludeInValidation.Models;

namespace UsingIncludeInValidation.Classes.Validators;

/// <summary>
/// Provides validation rules for the <see cref="IPerson.BirthDate"/> property.
/// </summary>
/// <remarks>
/// This validator ensures that the birthdate of an <see cref="IPerson"/> instance:
/// <list type="bullet">
/// <item><description>Is greater than the minimum year specified in the <see cref="ValidationSettings"/> configuration.</description></item>
/// <item><description>Is less than or equal to the current year.</description></item>
/// </list>
/// If the validation fails, an appropriate error message is generated.
/// </remarks>
public class BirthDateValidator : AbstractValidator<IPerson>
{

    public BirthDateValidator()
    {

        var value = JsonRoot().GetSection(nameof(ValidationSettings)).Get<ValidationSettings>().MinYear;
        var minYear = DateTime.Now.AddYears(value).Year;

        RuleFor(x => x.BirthDate)
            .Must(x => x.Year > minYear && x.Year <= DateTime.Now.Year)
            .WithMessage($"Birth date must be greater than {minYear} " +
                         $"year and less than or equal to {DateTime.Now.Year} ");
    }
}