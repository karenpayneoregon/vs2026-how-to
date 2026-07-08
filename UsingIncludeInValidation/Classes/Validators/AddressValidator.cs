using FluentValidation;
using UsingIncludeInValidation.Interfaces;

namespace UsingIncludeInValidation.Classes.Validators;

public class AddressValidator : AbstractValidator<IPerson>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddressValidator"/> class.
    /// </summary>
    /// <remarks>
    /// This validator ensures that the <see cref="Address"/> properties of an <see cref="IPerson"/> instance
    /// meet the required validation rules. Specifically:
    /// <list type="bullet">
    /// <item><description><c>Line1</c> (Street) must not be <c>null</c>.</description></item>
    /// <item><description><c>Town</c> must not be <c>null</c>.</description></item>
    /// <item><description><c>Country</c> must not be <c>null</c>.</description></item>
    /// <item><description><c>Postcode</c> must not be <c>null</c>.</description></item>
    /// </list>
    /// </remarks>
    public AddressValidator()
    {
        RuleFor(item => item.Address.Line1).NotNull()
            .WithName("Street")
            .WithMessage("Please ensure you have entered your {PropertyName}");
        RuleFor(item => item.Address.Town).NotNull();
        RuleFor(item => item.Address.Country).NotNull();
        RuleFor(item => item.Address.Postcode).NotNull();
    }
}