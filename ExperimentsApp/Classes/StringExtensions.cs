using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ExperimentsApp.Classes;

internal static partial class StringExtensions
{
    /// <summary>
    /// Splits the input string by inserting a space before each uppercase letter, 
    /// except for the first character, effectively separating words in camel case notation.
    /// </summary>
    /// <param name="input">The input string to be split.</param>
    /// <returns>
    /// A new string with spaces inserted before each uppercase letter, 
    /// or the original string if it is <c>null</c> or empty.
    /// </returns>
    /// <example>
    /// <code>
    /// string result = "KarenPayne".SplitOnUpperCase();
    /// // result: "Karen Payne"
    /// </code>
    /// </example>
    [DebuggerStepThrough]
    public static string SplitOnUpperCase(this string input) 
        => string.IsNullOrEmpty(input) ? input : SplitCamelCaseRegex().Replace(input, " $1");

    [GeneratedRegex("(?<!^)([A-Z])")]
    private static partial Regex SplitCamelCaseRegex();
}
