using FluentValidation;
using UsingIncludeInValidation.Interfaces;

namespace UsingIncludeInValidation.Classes.Validators;

/// <summary>
/// Represents a validator for <see cref="IPerson"/> instances.
/// </summary>
/// <remarks>
/// This validator combines multiple validators, including:
/// <list type="bullet">
/// <item><see cref="FirstNameValidator"/></item>
/// <item><see cref="LastNameValidator"/></item>
/// <item><see cref="BirthDateValidator"/></item>
/// <item><see cref="AddressValidator"/></item>
/// </list>
/// Each included validator ensures specific validation rules for the corresponding properties of an <see cref="IPerson"/>.
/// </remarks>
public class PersonValidator : AbstractValidator<IPerson>
{
    public PersonValidator()
    {
        Include(new FirstNameValidator());
        Include(new LastNameValidator());
        Include(new BirthDateValidator());
        Include(new AddressValidator());
    }
}