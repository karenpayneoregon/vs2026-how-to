using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ParameterizedCollectionModeSample.Classes;

public static class GenericExtensions
{
    extension<T>(T sender) where T : INumber<T>
    {
        /// <summary>Determines if a value represents an even integral value.</summary>
        public bool IsEven() => T.IsEvenInteger(sender);

        /// <summary>Determines if a value represents an odd integral value.</summary>
        public bool IsOdd() => T.IsOddInteger(sender);
    }
}
