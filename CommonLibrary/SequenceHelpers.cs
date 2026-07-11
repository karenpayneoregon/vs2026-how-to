using System.Text.RegularExpressions;

namespace CommonLibrary;

/// <summary>
/// Provides helper methods for working with sequences, such as generating
/// incremented alphanumeric strings or manipulating numeric suffixes in strings.
/// </summary>
public static partial class SequenceHelpers
{
    /// <summary>
    /// Increments the numeric suffix of the given string by 1.
    /// </summary>
    /// <param name="sender">The input string that ends with a numeric value.</param>
    /// <returns>A new string with the numeric suffix incremented by 1, preserving the original length of the numeric part.</returns>
    /// <exception cref="FormatException">Thrown when the numeric suffix cannot be parsed as a valid number.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the input string <paramref name="sender"/> is null.</exception>
    public static string NextValue(string sender)
    {
        var value = NumericSuffixRegEx().Match(sender).Value;
        return sender[..^value.Length] + (long.Parse(value) + 1)
            .ToString().PadLeft(value.Length, '0');
    }

    /// <summary>
    /// Generates the next invoice number by incrementing the numeric suffix of the given string by 1.
    /// </summary>
    /// <param name="sender">The input string that ends with a numeric value.</param>
    /// <returns>A new string with the numeric suffix incremented by 1, preserving the original length of the numeric part.</returns>
    /// <exception cref="FormatException">Thrown when the numeric suffix cannot be parsed as a valid number.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the input string <paramref name="sender"/> is null.</exception>
    public static string NextInvoiceNumber(string sender) => NextValue(sender);

    /// <summary>
    /// Increments the numeric suffix of the given string by the specified value.
    /// </summary>
    /// <param name="sender">The input string that ends with a numeric value.</param>
    /// <param name="incrementBy">The value by which to increment the numeric suffix.</param>
    /// <returns>A new string with the numeric suffix incremented by the specified value, preserving the original length of the numeric part.</returns>
    /// <exception cref="FormatException">Thrown when the numeric suffix cannot be parsed as a valid number.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the input string <paramref name="sender"/> is null.</exception>
    public static string NextValue(string sender, int incrementBy)
    {
        string value = EndingNumberRegex().Match(sender).Value;
        return sender[..^value.Length] + (long.Parse(value) + incrementBy)
            .ToString().PadLeft(value.Length, '0');
    }

    /// <summary>
    /// Generates the next invoice number by incrementing the numeric suffix of the given string by the specified value.
    /// </summary>
    /// <param name="sender">The input string that ends with a numeric value.</param>
    /// <param name="incrementBy">The value by which to increment the numeric suffix.</param>
    /// <returns>A new string with the numeric suffix incremented by the specified value, preserving the original length of the numeric part.</returns>
    /// <exception cref="FormatException">Thrown when the numeric suffix cannot be parsed as a valid number.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the input string <paramref name="sender"/> is null.</exception>
    public static string NextInvoiceNumber(string sender, int incrementBy) => NextValue(sender, incrementBy);

    /// <summary>
    /// Generates an auto-incrementing alphanumeric string based on a given prefix character and identifier.
    /// </summary>
    /// <param name="letter">The prefix character used as the starting point for the alphanumeric string.</param>
    /// <param name="identifier">The numeric identifier used to generate the alphanumeric sequence.</param>
    /// <returns>
    /// A string that combines the prefix character and an incremented alphanumeric sequence.
    /// Returns an empty string if the prefix character is not a valid letter.
    /// </returns>
    public static string NextValueFromIdentifier(char letter, int identifier)
    {
        if (!Characters.Contains(char.ToUpper(letter)))
        {
            return "";
        }

        int lead = char.ConvertToUtf32(char.ToString(letter).ToUpper(), 0);

        var parts = new List<string>();
        int numberPart = identifier % 10000;

        parts.Add(numberPart.ToString("0000"));
        identifier /= 10000;

        for (int index = 0; index < 3 || identifier > 0; ++index)
        {
            parts.Add(((char)(lead + (identifier % 26))).ToString());
            identifier /= 26;
        }

        return string.Join(string.Empty, parts.AsEnumerable().Reverse().ToArray());
    }

    public static char[] Characters => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    [GeneratedRegex("[0-9]+$")]
    private static partial Regex NumericSuffixRegEx();
    [GeneratedRegex("[0-9]+$")]
    private static partial Regex EndingNumberRegex();
}