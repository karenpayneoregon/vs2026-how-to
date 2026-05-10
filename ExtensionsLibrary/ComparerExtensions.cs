namespace ExtensionsLibrary;

public static class ComparerExtensions
{
    /// <param name="value">The value to compare.</param>
    /// <typeparam name="T">The type of the value, which must be a value type that implements <see cref="IComparable{T}"/>.</typeparam>
    extension<T>(T value) where T : struct, IComparable<T>
    {
        /// <summary>
        /// Determines whether the specified value is between the given lower and upper bounds, inclusive.
        /// </summary>
        /// <param name="lowerValue">The lower bound to compare against.</param>
        /// <param name="upperValue">The upper bound to compare against.</param>
        /// <returns>
        /// <c>true</c> if the value is between the lower and upper bounds, inclusive; otherwise, <c>false</c>.
        /// </returns>
        public bool Between(T lowerValue, T upperValue) => Comparer<T>.Default.Compare(value, lowerValue) >= 0 &&
                                                           Comparer<T>.Default.Compare(value, upperValue) <= 0;

        /// <summary>
        /// Determines whether the specified value is between the given lower and upper bounds, inclusive.
        /// </summary>
        /// <param name="lowerValue">The lower bound to compare against.</param>
        /// <param name="upperValue">The upper bound to compare against.</param>
        /// <returns>
        /// <c>true</c> if the value is between the lower and upper bounds, inclusive; otherwise, <c>false</c>.
        /// </returns>
        public bool IsBetween(T lowerValue, T upperValue) => value.Between(lowerValue, upperValue);
    }

    /// <param name="sender">The value to compare.</param>
    /// <typeparam name="T">The type of the value, which must implement <see cref="IComparable{T}"/>.</typeparam>
    extension<T>(IComparable<T> sender)
    {
        /// <summary>
        /// Determines whether the specified value is strictly between the given lower and upper bounds.
        /// </summary>
        /// <param name="minimumValue">The lower bound to compare against.</param>
        /// <param name="maximumValue">The upper bound to compare against.</param>
        /// <returns>
        /// <c>true</c> if the value is strictly between the lower and upper bounds; otherwise, <c>false</c>.
        /// </returns>
        public bool BetweenExclusive(T minimumValue, T maximumValue)
            => sender.CompareTo(minimumValue) > 0 &&
               sender.CompareTo(maximumValue) < 0;

        /// <summary>
        /// Determines whether the specified value is strictly between the given lower and upper bounds.
        /// </summary>
        /// <param name="minimumValue">The lower bound to compare against.</param>
        /// <param name="maximumValue">The upper bound to compare against.</param>
        /// <returns>
        /// <c>true</c> if the value is strictly between the lower and upper bounds; otherwise, <c>false</c>.
        /// </returns>
        public bool IsBetweenExclusive(T minimumValue, T maximumValue)
            => sender.BetweenExclusive(minimumValue, maximumValue);
    }
}


