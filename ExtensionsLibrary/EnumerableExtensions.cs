namespace ExtensionsLibrary;

public static class EnumerableExtensions
{
    extension<TSource>(IEnumerable<TSource>)
    {
        /// <summary>
        /// Combines two sequences into a single sequence by concatenating them.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input sequences.</typeparam>
        /// <param name="first">The first sequence to combine.</param>
        /// <param name="second">The second sequence to combine.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the elements of the first sequence followed by the elements of the second sequence.</returns>
        public static IEnumerable<TSource> Combine(IEnumerable<TSource> first, IEnumerable<TSource> second)
            => first.Concat(second);

        /// <summary>
        /// Gets an empty sequence of the specified type.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
        /// <returns>An empty <see cref="IEnumerable{T}"/> of the specified type.</returns>
        public static IEnumerable<TSource> Identity
            => Enumerable.Empty<TSource>();

    }
}