using System.Text.RegularExpressions;

namespace ExtensionsLibrary;
public static partial class StringExtensions
{
    public static string SplitCamelCase(this string sender) =>
        string.Join(" ", CamelCaseRegex.Matches(sender)
            .Select(m => m.Value));


    /// <summary>
    /// Converts the first character of the given string to uppercase.
    /// </summary>
    /// <param name="sender">The input string.</param>
    /// <returns>
    /// A new string with the first character converted to uppercase 
    /// </returns>
    public static string CapitalizeFirstLetter(this string sender)
        => string.IsNullOrEmpty(sender) ? sender : $"{char.ToUpper(sender[0])}{sender[1..].ToLower()}";

    /// <summary>
    /// Replaces the last occurrence of a specified string within the given string.
    /// </summary>
    /// <param name="source">The string to search within.</param>
    /// <param name="find">The string to find case-sensitive.</param>
    /// <param name="replace">The string to replace the found string with.</param>
    /// <returns>A new string with the last occurrence of the specified string replaced.</returns>
    public static string ReplaceLast(this string source, string find, string replace)
    {
        int index = source.LastIndexOf(find, StringComparison.OrdinalIgnoreCase);
        return index == -1 ?
            source :
            source.Remove(index, find.Length).Insert(index, replace);
    }

    /// <summary>
    /// Trims the last character from the given string.
    /// </summary>
    /// <param name="sender">The string from which to trim the last character.</param>
    /// <returns>A new string with the last character removed, or the original string if it is null or whitespace.</returns>
    public static string TrimLastCharacter(this string sender)
        => string.IsNullOrWhiteSpace(sender) ?
            sender :
            sender[..^1];

    /// <summary>
    /// Removes extra spaces from the given string, optionally trimming the end.
    /// </summary>
    /// <param name="source">The string from which to remove extra spaces.</param>
    /// <param name="trimEnd">If set to <c>true</c>, trims the end of the resulting string.</param>
    /// <returns>A new string with extra spaces removed.</returns>
    public static string RemoveExtraSpaces(this string source, bool trimEnd = false)
    {
        var result = ExtraSpacesRegex().Replace(source, " ");
        return trimEnd ? result.TrimEnd() : result;
    }

    /// <summary>
    /// Regular expression pattern for matching camel case words.
    /// </summary>
    private static readonly Regex CamelCaseRegex = CasingRegex();
    [GeneratedRegex(@"([A-Z][a-z]+)")]
    private static partial Regex CasingRegex();

    /// <summary>
    /// Provides a regular expression to match sequences of two or more whitespace characters.
    /// </summary>
    /// <returns>A <see cref="Regex"/> object that matches sequences of two or more whitespace characters.</returns>
    /// <remarks>
    /// Pattern:<br/>
    /// <code>\s{2,}</code><br/>
    /// Explanation:<br/>
    /// <code>
    /// ○ Match a whitespace character atomically at least twice.<br/>
    /// </code>
    /// </remarks>
    [GeneratedRegex(@"\s{2,}", RegexOptions.None)]
    private static partial Regex ExtraSpacesRegex();
}

