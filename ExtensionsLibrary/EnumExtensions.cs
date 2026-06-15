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

    extension<T>(T _) where T : struct, Enum
    {
        /// <summary>
        /// Returns a randomly selected value of type T from the available set.
        /// </summary>
        public T Randomize() => Randomize<T>();
    }
}