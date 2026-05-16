using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FluentWebApplication.Classes;

#pragma warning disable CS8618
/// <summary>
/// Provides extension methods for enhancing functionality related to validation results and model state management.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Adds the errors from a <see cref="FluentValidation.Results.ValidationResult"/> to the specified <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
    /// </summary>
    /// <param name="result">The <see cref="FluentValidation.Results.ValidationResult"/> containing validation errors.</param>
    /// <param name="modelState">The <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> to which the errors will be added.</param>
    /// <param name="prefix">
    /// An optional prefix to prepend to the property names of the validation errors. 
    /// If the prefix is not empty, it will be combined with the property names.
    /// </param>
    /// <remarks>
    /// This method iterates through the validation errors in the <paramref name="result"/> and adds them to the <paramref name="modelState"/>.
    /// If <paramref name="result"/> is valid, no errors will be added.
    /// </remarks>
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState, string prefix)
    {

        if (result.IsValid) return;

        foreach (var error in result.Errors)
        {
            var key = string.IsNullOrEmpty(prefix)
                ? error.PropertyName
                : string.IsNullOrEmpty(error.PropertyName)
                    ? prefix
                    : $"{prefix}.{error.PropertyName}";

            modelState.AddModelError(key, error.ErrorMessage);
        }
    }
}