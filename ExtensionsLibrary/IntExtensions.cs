namespace ExtensionsLibrary;
/// <summary>
/// Provides a set of extension methods for <see cref="int"/> data type.
/// </summary>
public static class IntExtensions
{
    extension(int number)
    {
        public void Increment()
            => number++;
        public void Decrement() => number--;
    }

    /// <summary>
    /// Provides an extension method container for modifying an <see cref="int"/> value by reference.
    /// </summary>
    /// <param name="number">The integer value to be modified by reference.</param>
    extension(ref int number)
    {
        public void RefIncrement()
            => number++;
        public void RefDecrement() 
            => number--;
    }
}
