using System.Numerics;

namespace WebApplication1.Classes;

public static class Extensions
{
    extension<T>(T sender) where T : INumber<T>
    {
        /// <summary>
        /// Determines whether the number is an even integer.
        /// </summary>
        /// <returns></returns>
        public bool IsEven() => T.IsEvenInteger(sender);
    }
}
