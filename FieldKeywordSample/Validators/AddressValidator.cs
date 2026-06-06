using FieldKeywordSample.Models;
using FluentValidation;

namespace FieldKeywordSample.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street).NotNull();
        RuleFor(x => x.City).NotNull();
        RuleFor(x => x.Country).NotNull();
        RuleFor(x => x.ZipCode).NotNull();
    }
}