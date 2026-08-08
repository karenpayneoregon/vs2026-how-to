namespace IndexMonths.Models;

/// <summary>
/// Represents a container for an element, providing detailed indexing information.
/// </summary>
/// <typeparam name="T">The type of the element contained within the container.</typeparam>
/// <remarks>
/// This class encapsulates an element along with its associated indexing details, including:
/// - The zero-based start index.
/// - The zero-based end index (from the end of the list).
/// - The one-based index.
/// </remarks>
public partial class ElementContainer<T>
{
    public T Value { get; set; }
    public Index StartIndex { get; set; }
    public Index EndIndex { get; set; }
    public int Index { get; set; }

}