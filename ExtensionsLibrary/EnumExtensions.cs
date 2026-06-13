using System;
using System.Diagnostics;

namespace ExtensionsLibrary;

public static class EnumExtensions
{
    private static readonly Random _random = new();

    /// <summary>
    /// Returns a randomly selected value from the specified enumeration type.
    /// </summary>
    /// <remarks>This method is useful for scenarios where a random enum value is needed, such as in testing
    /// or generating sample data. The method requires that <typeparamref name="T"/> is a non-nullable enum
    /// type.</remarks>
    /// <typeparam name="T">The enumeration type from which to select a random value. Must be a value type that is an enum.</typeparam>
    /// <returns>A randomly chosen value of type <typeparamref name="T"/> from the set of defined enumeration values.</returns>
    [DebuggerStepThrough]
    public static T Random<T>() where T : struct, Enum
    {
        var values = Enum.GetValues<T>();
        return values[_random.Next(values.Length)];
    }


    extension<T>(T _) where T : struct, Enum
    {
        /// <summary>
        /// Returns a randomly selected value of type T from the available set.
        /// </summary>
        /// <returns>A randomly chosen value of type T. The specific value returned depends on the implementation and available
        /// values of T.</returns>
        public T Random() => Random<T>();
    }
}