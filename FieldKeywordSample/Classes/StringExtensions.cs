using System.Text.RegularExpressions;

namespace FieldKeywordSample.Classes;
public static partial class StringExtensions
{

    /// <param name="input">The string to capitalize.</param>
    extension(string input)
    {

        /// <summary>
        /// Capitalizes the first letter of the given string while converting the rest to lowercase.
        /// </summary>
        /// <returns>
        /// A new string with the first letter capitalized and the remaining characters in lowercase,
        /// or the original string if it is null, empty, or consists only of whitespace.
        /// </returns>
        public string CapitalizeFirstLetter()
            => string.IsNullOrWhiteSpace(input) ?
                input : char.ToUpper(input[0]) + input.AsSpan(1).ToString().ToLower();
        /// <summary>
        /// Trims the last character from the given string.
        /// </summary>
        /// <returns>A new string with the last character removed, or the original string if it is null or whitespace.</returns>
        public string TrimLastCharacter()
            => string.IsNullOrWhiteSpace(input) ?
                input :
                input[..^1];

        /// <summary>
        /// Removes extra spaces from the given string, optionally trimming the end.
        /// </summary>
        /// <param name="trimEnd">If set to <c>true</c>, trims the end of the resulting string.</param>
        /// <returns>A new string with extra spaces removed.</returns>
        public string RemoveExtraSpaces(bool trimEnd = false)
        {
            var result = ExtraSpacesRegex().Replace(input, " ");
            return trimEnd ? result.TrimEnd() : result;
        }

        /// <summary>
        /// Replaces the last occurrence of a specified string within the given string.
        /// </summary>
        /// <param name="find">The string to find case-sensitive.</param>
        /// <param name="replace">The string to replace the found string with.</param>
        /// <returns>A new string with the last occurrence of the specified string replaced.</returns>
        public string ReplaceLast(string find, string replace)
        {
            var index = input.LastIndexOf(find, StringComparison.OrdinalIgnoreCase);
            return index == -1 ?
                input :
                input.Remove(index, find.Length).Insert(index, replace);
        }
    }


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
