namespace ExcelMapperApp1.Classes;

/// <summary>
/// Custom setting for presenting runtime exceptions using AnsiConsole.WriteException.
///
/// The idea here is to present different types of exceptions with different colors while
/// one would be for all exceptions and the other(s) for specific exception types.
/// </summary>
public static class ExceptionHelpers
{
    /// <summary>
    /// Configures and displays the specified exception using custom styles and formatting.
    /// </summary>
    /// <param name="exception">
    /// The exception to be displayed. This parameter cannot be <c>null</c>.
    /// </param>
    /// <remarks>
    /// This method utilizes <c>AnsiConsole.WriteException</c> to present the exception details
    /// with customized colors and styles for various exception components, such as the message,
    /// method, and line number. It is intended to enhance the readability of exception outputs
    /// in the console.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="exception"/> parameter is <c>null</c>.
    /// </exception>
    public static void ColorWithCyanFuchsia(this Exception exception)
    {
        AnsiConsole.WriteException(exception, new ExceptionSettings
        {
            Format = ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks,
            Style = new ExceptionStyle
            {
                Exception = new Style().Foreground(Color.Grey),
                Message = new Style().Foreground(Color.DarkSeaGreen),
                NonEmphasized = new Style().Foreground(Color.Cornsilk1),
                Parenthesis = new Style().Foreground(Color.Cornsilk1),
                Method = new Style().Foreground(Color.Fuchsia),
                ParameterName = new Style().Foreground(Color.Cornsilk1),
                ParameterType = new Style().Foreground(Color.Aqua),
                Path = new Style().Foreground(Color.Red),
                LineNumber = new Style().Foreground(Color.Cornsilk1),
            }
        });

    }

}