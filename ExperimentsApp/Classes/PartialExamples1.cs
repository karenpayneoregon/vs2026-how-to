namespace ExperimentsApp.Classes;

public partial class PartialExamples
{
    private List<string> _items = ["one", "two", "three", "four", "five"];

    // Implementing declaration

    /// <summary>
    /// Gets or sets the number of elements that the List can contain.
    /// </summary>
    /// <remarks>
    /// If the value is less than the current capacity, the list will shrink to the
    /// new value. If the value is negative, the list isn't modified.
    /// </remarks>
    public partial int Capacity
    {
        get => _items.Count;
        set
        {
            if ((value != _items.Count) && (value >= 0))
            {
                _items.Capacity = value;
            }
        }
    }

    public partial string this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    /// <summary>
    /// Gets the element at the specified index.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The string stored at that index, or null if out of bounds</returns>
    public partial string? TryGetAt(int index)
    {
        if (index < _items.Count)
        {
            return _items[index];
        }
        return null;
    }
}