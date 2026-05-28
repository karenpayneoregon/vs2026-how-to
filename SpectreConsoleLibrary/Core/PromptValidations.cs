using Spectre.Console;

namespace SpectreConsoleLibrary.Core;

internal static class PromptValidations
{
    public static ValidationResult ValidateDate(DateOnly dateOnly, int year)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        if (dateOnly > today)
        {
            return ValidationResult.Error("[red]Birth date cannot be in the future[/]");
        }

        if (dateOnly.Year >= year)
        {
            return ValidationResult.Error($"[red]Must be less than {year}[/]");
        }

        return ValidationResult.Success();
    }
}