using FieldKeywordSample.Interfaces;
using static FieldKeywordSample.Classes.Helpers;

namespace FieldKeywordSample.Models;

/// <summary>
/// Represents an address associated with a customer, including details such as street, city, state, zip code, country, and phone number.
/// </summary>
/// <remarks>
/// This class implements the <see cref="IAddress"/> interface and provides validation for state abbreviations.
/// </remarks>
public class Address : IAddress
{
    public int Id { get; set; }
    public required int CustomerId { get; set; }
    public required string Street { get; set => field = value.Trim(); }
    public required string City { get; set => field = value.Trim(); }
    /// <summary>
    /// Gets or sets the state abbreviation for the address.
    /// </summary>
    /// <remarks>
    /// The value is automatically converted to uppercase and trimmed. 
    /// An exception is thrown if the value is not a valid state abbreviation.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided value is not a valid state abbreviation.
    /// </exception>
    public required string State
    {
        get;
        set
        {
            field = value.ToUpper().Trim();
            if (!GetStateAbbreviations().Contains(field))
            {
                throw new ArgumentException("Invalid state abbreviation.");
            }
        }
    }

    public required string ZipCode { get; set => field = value.Trim(); }
    public required string Country { get; set => field = value.Trim(); }
    public required string Phone { get; set => field = value.Trim(); }

    /// <summary>
    /// Gets the full address as a formatted string, combining the street, city, state, zip code, and country.
    /// </summary>
    public string FullAddress => $"{Street}, {City}, {State}, {ZipCode}, {Country}";

}