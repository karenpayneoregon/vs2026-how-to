using System.Globalization;

namespace CommonLibrary;

/// <summary>
/// Provides a mechanism for comparing strings that contain numeric values in a culture-sensitive manner.
/// </summary>
/// <remarks>
/// This class implements <see cref="IComparer{T}"/> for strings and uses the specified <see cref="CultureInfo"/>
/// to perform comparisons that respect numeric ordering. For example, "file10" will be considered greater than "file2".
/// </remarks>
public sealed class NumericStringComparer : IComparer<string>
{
    private readonly CompareInfo _compareInfo;
    private readonly CompareOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="NumericStringComparer"/> class
    /// with the specified culture information.
    /// </summary>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> to use for culture-specific string comparisons.
    /// </param>
    /// <remarks>
    /// This constructor sets up the comparer to perform numeric-aware string comparisons
    /// based on the rules of the provided culture.
    /// </remarks>
    public NumericStringComparer(CultureInfo culture)
    {
        _compareInfo = culture.CompareInfo;
        _options = CompareOptions.NumericOrdering;
    }

    /// <summary>
    /// Compares two strings and returns a value indicating whether one is less than, equal to, or greater than the other.
    /// </summary>
    /// <param name="x">The first string to compare. Can be <c>null</c>.</param>
    /// <param name="y">The second string to compare. Can be <c>null</c>.</param>
    /// <returns>
    /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>:
    /// <list type="bullet">
    /// <item>
    /// <description>Less than zero: <paramref name="x"/> is less than <paramref name="y"/>.</description>
    /// </item>
    /// <item>
    /// <description>Zero: <paramref name="x"/> equals <paramref name="y"/>.</description>
    /// </item>
    /// <item>
    /// <description>Greater than zero: <paramref name="x"/> is greater than <paramref name="y"/>.</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method performs a culture-sensitive comparison that respects numeric ordering.
    /// For example, "file10" will be considered greater than "file2".
    /// </remarks>
    public int Compare(string? x, string? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (x is null) return -1;
        return y is null ? 1 : _compareInfo.Compare(x, y, _options);
    }
}