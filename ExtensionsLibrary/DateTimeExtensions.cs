using System;
using System.Globalization;

namespace ExtensionsLibrary
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns a composite date and time format string that combines the short date pattern with the long time
        /// pattern from the specified format information.
        /// </summary>
        /// <param name="formatInfo">The <see cref="DateTimeFormatInfo"/> instance that provides the date and time patterns. Cannot be null.</param>
        extension(DateTimeFormatInfo formatInfo)
        {
            /// <summary>
            /// Returns a composite date and time format string that combines the short date pattern with the long time
            /// pattern from the current format information.
            /// </summary>
            /// <returns>A string representing the short date pattern followed by the long time pattern, separated by a space.</returns>
            /// <exception cref="ArgumentNullException">Thrown if the format information is null.</exception>
            public string ShortDateLongTimeFormat()
            {
                return formatInfo is null ? 
                    throw new ArgumentNullException(nameof(formatInfo)) : 
                    $"{formatInfo.ShortDatePattern} {formatInfo.ShortTimePattern}";
            }
        }
    }
}
