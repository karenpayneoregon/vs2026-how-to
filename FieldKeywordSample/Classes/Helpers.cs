namespace FieldKeywordSample.Classes;
internal class Helpers
{
    /// <summary>
    /// Retrieves a list of valid U.S. state abbreviations.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of strings containing two-letter state abbreviations.
    /// </returns>
    public static List<string> GetStateAbbreviations() =>
    [
        "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
        "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
        "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
        "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
        "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
    ];

    /// <summary>
    /// Retrieves a random valid U.S. state abbreviation.
    /// </summary>
    /// <returns>
    /// A two-letter U.S. state abbreviation.
    /// </returns>
    public static string GetRandomStateAbbreviation()
    {
        var states = GetStateAbbreviations();

        return states[Random.Shared.Next(states.Count)];
    }
}
