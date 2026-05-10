using System.Text.RegularExpressions;

namespace ExtensionsLibrary;
public static partial class StringExtensions
{
    /// <param name="sender">The input string.</param>
    extension(string sender)
    {
        
        /// <summary>
        /// Determines whether the specified string has a value.
        /// </summary>
        /// <param name="value">The string to check for a value.</param>
        /// <returns>
        /// <c>true</c> if the string is not null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasValue(string value)
            => !string.IsNullOrEmpty(value);

        public bool IsEmpty => string.IsNullOrEmpty(sender);
        public bool IsWhitespace => string.IsNullOrWhiteSpace(sender);

        public bool IsNumeric => !sender.IsEmpty && sender.All(char.IsDigit);

        /// <summary>
        /// Splits a camel case string into separate words.
        /// </summary>
        /// <returns>
        /// A new string where camel case words are separated by spaces.
        /// </returns>
        public string SplitCamelCase() =>
            string.Join(" ", CamelCaseRegex.Matches(sender)
                .Select(m => m.Value));

        /// <summary>
        /// Converts the first character of the given string to uppercase.
        /// </summary>
        /// <returns>
        /// A new string with the first character converted to uppercase 
        /// </returns>
        public string CapitalizeFirstLetter()
            => string.IsNullOrEmpty(sender) ? sender : $"{char.ToUpper(sender[0])}{sender[1..].ToLower()}";

        /// <summary>
        /// Replaces the last occurrence of a specified string within the given string.
        /// </summary>
        /// <param name="find">The string to find case-sensitive.</param>
        /// <param name="replace">The string to replace the found string with.</param>
        /// <returns>A new string with the last occurrence of the specified string replaced.</returns>
        public string ReplaceLast(string find, string replace)
        {
            int index = sender.LastIndexOf(find, StringComparison.OrdinalIgnoreCase);
            return index == -1 ?
                sender :
                sender.Remove(index, find.Length).Insert(index, replace);
        }

        /// <summary>
        /// Trims the last character from the given string.
        /// </summary>
        /// <returns>A new string with the last character removed, or the original string if it is null or whitespace.</returns>
        public string TrimLastCharacter()
            => string.IsNullOrWhiteSpace(sender) ?
                sender :
                sender[..^1];

        /// <summary>
        /// Removes extra spaces from the given string, optionally trimming the end.
        /// </summary>
        /// <param name="trimEnd">If set to <c>true</c>, trims the end of the resulting string.</param>
        /// <returns>A new string with extra spaces removed.</returns>
        public string RemoveExtraSpaces(bool trimEnd = false)
        {
            var result = ExtraSpacesRegex().Replace(sender, " ");
            return trimEnd ? result.TrimEnd() : result;
        }
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

