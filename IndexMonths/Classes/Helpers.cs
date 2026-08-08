using System.Globalization;
using IndexMonths.Models;

namespace IndexMonths.Classes;

public class Helpers
{
    /// <summary>
    /// Retrieves a list of month names from the current culture's <see cref="DateTimeFormatInfo"/>.
    /// </summary>
    /// <returns>
    /// A list of strings representing the names of the months in the current culture,
    /// excluding any empty or placeholder entries.
    /// </returns>
    /// <remarks>
    /// This method utilizes the <see cref="DateTimeFormatInfo.CurrentInfo"/> property to fetch
    /// the month names and excludes the last entry, which is typically an empty string.
    /// </remarks>
    public static List<string> MonthNames() =>
        [.. DateTimeFormatInfo.CurrentInfo!.MonthNames[..^1]];

    /// <summary>
    /// Generates a list of <see cref="ElementContainer{T}"/> objects, each containing details about an element in the provided list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the input list.</typeparam>
    /// <param name="list">The input list of elements to process.</param>
    /// <returns>
    /// A list of <see cref="ElementContainer{T}"/> objects, where each object contains:
    /// - The element value.
    /// - Its zero-based start index.
    /// - Its zero-based end index (from the end of the list).
    /// - Its one-based index.
    /// </returns>
    /// <remarks>
    /// This method uses LINQ to project each element of the input list into an <see cref="ElementContainer{T}"/> object,
    /// providing detailed indexing information.
    /// </remarks>
    public static List<ElementContainer<T>> RangeDetails<T>(List<T> list) =>
    [
        .. list.Select((element, index) => new ElementContainer<T>
        {
            Value = element,
            StartIndex = new Index(index),
            EndIndex = new Index(list.Count - index - 1, true),
            Index = index + 1
        })
    ];
}

