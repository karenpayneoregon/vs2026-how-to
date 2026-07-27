using System.Globalization;
using FormattableStringApp.Models;

namespace FormattableStringApp.Classes;

/// <summary>
/// Provides extension methods for operations related to the <c>Person</c> class.
/// </summary>
/// <remarks>
/// This static class contains utility methods that extend the functionality of the <c>Person</c> class.
/// </remarks>
public static class PersonExtensions
{
    /// <summary>
    /// Provides formatted representations of properties for a <see cref="Person"/> instance.
    /// </summary>
    /// <remarks>
    /// This extension encapsulates formatted versions of the <see cref="Person"/> class properties, 
    /// such as <c>IdFormatted</c>, <c>FirstNameFormatted</c>, <c>LastNameFormatted</c>, 
    /// and <c>BirthDateFormatted</c>, for consistent display or processing.
    /// </remarks>
    extension(Person person)
    {
        public string IdFormatted => person.Id.ToString(CultureInfo.InvariantCulture);

        public string FirstNameFormatted => person.FirstName;

        public string LastNameFormatted => person.LastName;

        public string BirthDateFormatted => person.BirthDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
    }
}