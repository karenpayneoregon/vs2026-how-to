using Spectre.Console;

namespace FormattableStringApp.Classes;

public static class SpectreConsoleExtensions
{
    /// <summary>
    /// Provides an extension method for rendering a sequence of <see cref="FormattableString"/> objects 
    /// as markup lines in the console using Spectre.Console.
    /// </summary>
    /// <remarks>
    /// This extension method processes each <see cref="FormattableString"/> in the provided collection 
    /// and renders it with Spectre.Console's markup capabilities, enabling rich text formatting.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="source"/> collection is <c>null</c>.
    /// </exception>
    extension(IEnumerable<FormattableString> source)
    {
     
        /// <summary>
        /// Renders a sequence of <see cref="FormattableString"/> objects as markup lines in the console.
        /// </summary>
        /// <remarks>
        /// Each <see cref="FormattableString"/> in the <paramref name="source"/> collection is processed 
        /// and rendered using Spectre.Console's markup capabilities, allowing for rich text formatting.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="source"/> collection is <c>null</c>.
        /// </exception>
        public void WriteMarkupLines()
        {
            ArgumentNullException.ThrowIfNull(source);

            foreach (var item in source)
            {
                AnsiConsole.MarkupLineInterpolated(item);
            }
        }
    }
}