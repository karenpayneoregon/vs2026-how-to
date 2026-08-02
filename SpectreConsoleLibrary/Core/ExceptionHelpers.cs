using Spectre.Console;

namespace SpectreConsoleLibrary.Core;

/// <summary>
/// Custom setting for presenting runtime exceptions using AnsiConsole.WriteException.
///
/// The idea here is to present different types of exceptions with different colors while
/// one would be for all exceptions and the other(s) for specific exception types.
/// </summary>
public class ExceptionHelpers
{
    /// <summary>
    /// Renders the details of the specified exception using a custom color scheme for enhanced readability.
    /// </summary>
    /// <param name="exception">
    /// The exception to display, provided as an instance of <see cref="Exception"/>. 
    /// This includes the message, stack trace, and other relevant details.
    /// </param>
    /// <remarks>
    /// This method leverages <see cref="AnsiConsole.WriteException(Exception, ExceptionSettings)"/> to output the exception details 
    /// with a visually distinct color scheme. The formatting emphasizes key components:
    /// - **Cyan** is used for parameter types.
    /// - **Fuchsia** is used for method names.
    /// - Additional styles are applied to other elements, such as paths and line numbers, 
    /// to improve the clarity of the exception output.
    /// 
    /// This approach is particularly useful for debugging scenarios where quick identification 
    /// of exception details is critical.
    /// </remarks>
    public static void ColorWithCyanFuchsia(Exception exception)
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
    /// <summary>
    /// Renders the details of the specified exception using a standard color scheme for readability.
    /// </summary>
    /// <param name="exception">
    /// The exception to display, provided as an instance of <see cref="Exception"/>. 
    /// This includes the message, stack trace, and other relevant details.
    /// </param>
    /// <remarks>
    /// This method utilizes <see cref="AnsiConsole.WriteException(Exception, ExceptionSettings)"/> to output the exception details 
    /// with a consistent and visually clear color scheme. The formatting applies the following styles:
    /// - **White** is used for exception messages.
    /// - **DarkOrange** is used for method names.
    /// - **Aqua** is used for parameter types.
    /// - **GreenYellow** is used for parentheses.
    /// - Additional styles are applied to paths, line numbers, and other elements to ensure clarity.
    /// 
    /// This approach is suitable for general debugging scenarios, providing a clean and standard visual representation of exception details.
    /// </remarks>
    public static void ColorStandard(Exception exception)
    {
        AnsiConsole.WriteException(exception, new ExceptionSettings
        {
            Format = ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks,
            Style = new ExceptionStyle
            {
                Exception = new Style().Foreground(Color.Grey),
                Message = new Style().Foreground(Color.White),
                NonEmphasized = new Style().Foreground(Color.Cornsilk1),
                Parenthesis = new Style().Foreground(Color.GreenYellow),
                Method = new Style().Foreground(Color.DarkOrange),
                ParameterName = new Style().Foreground(Color.Cornsilk1),
                ParameterType = new Style().Foreground(Color.Aqua),
                Path = new Style().Foreground(Color.White),
                LineNumber = new Style().Foreground(Color.Cornsilk1),
            }
        });

    }
}