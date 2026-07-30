using ModelFormatCustomSample.Classes;

namespace ModelFormatCustomSample.Models;
public record Customer(int Id, string FirstName, string LastName, DateOnly BirthDay) : IFormattable
{
    public int Id { get; set; } = Id;
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public DateOnly BirthDay { get; set; } = BirthDay;

    /// <summary>
    /// Returns a string representation of the customer based on the specified format and format provider.
    /// </summary>
    /// <param name="format">
    /// A format string that determines the representation of the customer. Supported formats include:
    /// <list type="bullet">
    /// <item>
    /// <term>"A" or "Age"</term>
    /// <description>Returns the customer's age.</description>
    /// </item>
    /// <item>
    /// <term>"IFl"</term>
    /// <description>Returns the customer's ID, first name, and last name.</description>
    /// </item>
    /// <item>
    /// <term>"FL"</term>
    /// <description>Returns the customer's first name and last name.</description>
    /// </item>
    /// <item>
    /// <term>"B" or "Birth"</term>
    /// <description>Returns the customer's birthdate.</description>
    /// </item>
    /// <item>
    /// <term>"I"</term>
    /// <description>Returns the customer's ID.</description>
    /// </item>
    /// <item>
    /// <term>Default</term>
    /// <description>Returns a combination of the customer's ID, birth date, and last name.</description>
    /// </item>
    /// </list>
    /// </param>
    /// <param name="_">
    /// An <see cref="IFormatProvider"/> object that provides culture-specific formatting information. This parameter is not used.
    /// </param>
    /// <returns>A <see cref="string"/> representation of the customer based on the specified format.</returns>
    public string ToString(string? format, IFormatProvider? _) => format switch
        {
            "A" or "Age" => $"{BirthDay.GetAge()}",
            "IFl" => $"{Id,-5}{FirstName} {LastName}",
            "FL" => $"{FirstName} {LastName}",
            "B" or "Birth" => $"{BirthDay}",
            "I" => $"{Id}",
            _ => $"{Id,-3}{BirthDay} {LastName}, {BirthDay}"
        };
    /// <summary>
    /// Returns a string representation of the customer, including their full name and age.
    /// </summary>
    /// <returns>A <see cref="string"/> containing the customer's full name and age.</returns>
    public override string ToString()
        => $"{FirstName} {LastName}, age {BirthDay.GetAge()}";

    public void Deconstruct(out string firstName, out string lastName, out DateOnly birth)
    {
        firstName = FirstName;
        lastName = LastName;
        birth = BirthDay;
    }
}