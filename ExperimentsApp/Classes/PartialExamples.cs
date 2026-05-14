namespace ExperimentsApp.Classes;

public partial class PartialExamples
{
    /// <summary>
    /// Gets or sets the number of elements that the List can contain.
    /// </summary>
    public partial int Capacity { get; set; }

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The string stored at that index</returns>
    public partial string this[int index] { get; set; }

    public partial string? TryGetAt(int index);
}