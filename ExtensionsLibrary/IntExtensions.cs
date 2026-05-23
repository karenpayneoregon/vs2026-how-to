namespace ExtensionsLibrary;
/// <summary>
/// Provides a set of extension methods for <see cref="int"/> data type.
/// </summary>
public static class IntExtensions
{
    extension(int number)
    {
        /// <summary>
        /// Increments the value of the current <see cref="int"/> instance by one.
        /// </summary>
        /// <remarks>
        /// This method modifies the value of the integer it is called on.
        /// </remarks>
        public void Increment()
            => number++;
        /// <summary>
        /// Decrements the value of the current <see cref="int"/> instance by one.
        /// </summary>
        /// <remarks>
        /// This method modifies the value of the integer it is called on.
        /// </remarks>
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
