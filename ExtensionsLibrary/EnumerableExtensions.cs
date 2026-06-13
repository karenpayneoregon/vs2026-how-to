using System.Diagnostics;

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
        [DebuggerStepThrough]
        public static IEnumerable<TSource> Combine(IEnumerable<TSource> first, IEnumerable<TSource> second)
            => first.Concat(second);

        /// <summary>
        /// Combines three sequences into a single sequence by concatenating them.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input sequences.</typeparam>
        /// <param name="first">The first sequence to combine.</param>
        /// <param name="second">The second sequence to combine.</param>
        /// <param name="third">The third sequence to combine.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the elements of the first sequence followed by the elements of the second sequence followed by the elements of the third sequence.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<TSource> Combine(
            IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEnumerable<TSource> third)
            => first.Concat(second).Concat(third);

        /// <summary>
        /// Combines four sequences into a single sequence by concatenating them.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input sequences.</typeparam>
        /// <param name="first">The first sequence to combine.</param>
        /// <param name="second">The second sequence to combine.</param>
        /// <param name="third">The third sequence to combine.</param>
        /// <param name="fourth">The fourth sequence to combine.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the elements of the first sequence followed by the elements of the second sequence followed by the elements of the third sequence followed by the elements of the fourth sequence.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<TSource> Combine(
            IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEnumerable<TSource> third,
            IEnumerable<TSource> fourth)
            => first.Concat(second).Concat(third).Concat(fourth);

        /// <summary>
        /// Combines five sequences into a single sequence by concatenating them.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input sequences.</typeparam>
        /// <param name="first">The first sequence to combine.</param>
        /// <param name="second">The second sequence to combine.</param>
        /// <param name="third">The third sequence to combine.</param>
        /// <param name="fourth">The fourth sequence to combine.</param>
        /// <param name="fifth">The fifth sequence to combine.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the elements of the first sequence followed by the elements of the second sequence followed by the elements of the third sequence followed by the fourth sequence followed by the fifth sequence.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<TSource> Combine(
            IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEnumerable<TSource> third,
            IEnumerable<TSource> fourth,
            IEnumerable<TSource> fifth)
        {
            return first
                .Concat(second)
                .Concat(third)
                .Concat(fourth)
                .Concat(fifth);
        }


        /// <summary>
    /// Gets an empty sequence of the specified type.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
    /// <returns>An empty <see cref="IEnumerable{T}"/> of the specified type.</returns>
    public static IEnumerable<TSource> EmptySequence
            => [];

    }
}