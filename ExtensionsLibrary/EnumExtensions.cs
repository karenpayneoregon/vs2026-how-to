using System;

namespace ExtensionsLibrary;

public static class EnumExtensions
{
    private static readonly Random _random = new();

    public static T Random<T>() where T : struct, Enum
    {
        var values = Enum.GetValues<T>();
        return values[_random.Next(values.Length)];
    }

    public static T Random<T>(this T _) where T : struct, Enum => Random<T>();
}
