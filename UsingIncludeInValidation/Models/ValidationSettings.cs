namespace UsingIncludeInValidation.Models;

/// <summary>
/// Represents the validation settings used for configuring validation rules.
/// </summary>
/// <remarks>
/// This class provides configuration options, such as the minimum year, 
/// which can be utilized by validators like <see cref="Classes.Validators.BirthDateValidator"/> 
/// to enforce specific validation rules.
/// </remarks>
public class ValidationSettings
{
    public int MinYear { get; set; }
}