using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FluenyValidationLibrary;

#pragma warning disable CS8618
public static class FluentValidationExtensions
{
    extension(ValidationResult result)
    {
        /// <summary>
        /// Adds the errors from a <see cref="FluentValidation.Results.ValidationResult"/> to the specified <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
        /// </summary>
        /// <param name="modelState">The <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> to which the validation errors will be added.</param>
        /// <param name="prefix">
        /// An optional prefix to prepend to the property names of the validation errors. 
        /// If the prefix is not null or empty, it will be combined with the property names of the errors.
        /// </param>
        /// <remarks>
        /// This method iterates through the validation errors in the <see cref="FluentValidation.Results.ValidationResult"/> 
        /// and adds them to the <paramref name="modelState"/> with their respective property names.
        /// </remarks>
        public void AddToModelState(ModelStateDictionary modelState, string prefix)
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
}