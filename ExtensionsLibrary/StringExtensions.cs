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
    /// Regular expression pattern for matching camel case words.
    /// </summary>
    private static readonly Regex CamelCaseRegex = CasingRegex();
    [GeneratedRegex(@"([A-Z][a-z]+)")]
    private static partial Regex CasingRegex();
}
