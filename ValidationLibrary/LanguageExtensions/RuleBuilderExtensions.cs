using FluentValidation;


namespace ValidationLibrary.LanguageExtensions;
public static class RuleBuilderExtensions
{
    /// <param name="ruleBuilder"></param>
    /// <typeparam name="T"></typeparam>
    extension<T>(IRuleBuilder<T, string> ruleBuilder)
    {
        /// <summary>
        /// Matches a specific phone number pattern nnn-nnnn
        /// </summary>
        /// <returns></returns>
        public IRuleBuilderOptions<T, string> MatchPhoneNumber()
            => ruleBuilder
                .Matches(@"^(1-)?\d{3}-\d{4}$")
                .WithMessage("Invalid phone number");

        public IRuleBuilderOptions<T, string> NotStartWithWhiteSpace() 
            => ruleBuilder
                .Must(m => m != null && !m.StartsWith(" "))
                .WithMessage("'{PropertyName}' should not start with whitespace");

        public IRuleBuilderOptions<T, string> NotEndWithWhiteSpace() 
            => ruleBuilder
                .Must(m => m != null && !m.EndsWith(" "))
                .WithMessage("'{PropertyName}' should not end with whitespace");
    }

    extension<T>(IRuleBuilder<T, DateOnly> ruleBuilder)
    {
        public IRuleBuilderOptions<T, DateOnly> BirthDateRule()
        {
            int minYear = DateTime.Now.AddYears(-100).Year;
            return ruleBuilder
                .Must(x => x.Year > minYear && x.Year <= DateTime.Now.Year)
                .WithMessage($"Birth date must be greater than {minYear} " +
                             $"year and less than or equal to {DateTime.Now.Year} ");
        }
    }

}
