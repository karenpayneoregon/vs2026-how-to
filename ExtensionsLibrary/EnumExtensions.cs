using System;
using System.Diagnostics;


namespace ExtensionsLibrary;

public static class EnumExtensions
{
    /// <summary>
    /// Returns a randomly selected value from the specified enumeration type.
    /// </summary>
    [DebuggerStepThrough]
    public static T Randomize<T>() where T : struct, Enum
    {
        var values = Enum.GetValues<T>();
        return values[Random.Shared.Next(values.Length)];
    }


    /// <summary>
    /// Returns a specified number of randomly selected unique values from the specified enumeration type.
    /// </summary>
    /// <typeparam name="T">The enumeration type from which values are selected.</typeparam>
    /// <param name="count">The number of unique values to select.</param>
    /// <returns>A <see cref="HashSet{T}"/> containing the randomly selected unique values.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="count"/> is less than 0 or greater than the total number of values in the enumeration.
    /// </exception>
    [DebuggerStepThrough]
    public static HashSet<T> Randomize<T>(int count) where T : struct, Enum
    {
        var values = Enum.GetValues<T>();

        if (count < 0 || count > values.Length)
        {
            throw new ArgumentOutOfRangeException(
                nameof(count),
                count,
                $"Count must be between 0 and {values.Length}.");
        }

        for (var index = 0; index < count; index++)
        {
            var randomIndex = Random.Shared.Next(index, values.Length);
            (values[index], values[randomIndex]) = (values[randomIndex], values[index]);
        }

        var selectedValues = new HashSet<T>();

        for (var index = 0; index < count; index++)
        {
            selectedValues.Add(values[index]);
        }

        return selectedValues;
    }

    extension<T>(T _) where T : struct, Enum
    {
        /// <summary>
        /// Returns a randomly selected value of type T from the available set.
        /// </summary>
        public T Randomize() => Randomize<T>();

        /// <summary>
        /// Returns the requested number of randomly selected unique values of type T from the available set.
        /// </summary>
        public HashSet<T> Randomize(int count) => Randomize<T>(count);
    }
}