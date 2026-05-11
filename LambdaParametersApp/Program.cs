using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace LambdaParametersApp;

/// <summary>
/// Demonstrates lambda/local-function parameter capture semantics using a TryParse-style delegate.
/// Shows how capturing an outer variable can cause the delegate to ignore its parameter.
/// </summary>
internal partial class Program
{
    /// <summary>
    /// Represents a TryParse pattern delegate that attempts to parse a string into T.
    /// The method returns true if parsing succeeded and outputs the parsed value via <paramref name="result"/>.
    /// </summary>
    /// <typeparam name="T">The target parse type.</typeparam>
    /// <param name="text">The input text to parse.</param>
    /// <param name="result">When this method returns, contains the parsed value if parsing succeeded; otherwise the default of T.</param>
    /// <returns>True if parsing succeeded; otherwise false.</returns>
    private delegate bool TryParse<T>(string text, out T result);

    /// <summary>
    /// Entry point. Sets up a sample value and demonstrates a local function that mistakenly
    /// captures an outer variable instead of using its <c>text</c> parameter.
    /// </summary>
    /// <param name="args">Command-line arguments (unused).</param>
    static void Main(string[] args)
    {
        // Sample input that will fail when parsed as an integer.
        var value = "90Q";

        // Parse1 captures the outer variable 'value' instead of using its 'text' parameter.
        // This demonstrates how a lambda/local function can close over variables from the enclosing scope,
        // which may lead to surprising behavior if you expect the function to use its parameters.
        bool Parse1(string text, out int result) => int.TryParse(value, out result);

        // Correct implementation that uses the provided parameter.
        bool Parse2(string text, out int result) => int.TryParse(text, out result);

        // Using the delegate with a captured variable (Parse1).
        if (Parse1(value, out var parsedValue1))
        {
            AnsiConsole.MarkupLine($"[green]Parsed using captured variable:[/] {parsedValue1}");
        }
        else
        {
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "Failed to parse using captured variable.");
        }

        Console.WriteLine();
        
        //Optionally demonstrate the correct behavior(uncomment to use).
        if (Parse2(value, out var parsedValue2))
        {
            AnsiConsole.MarkupLine($"[green]Parsed using parameter:[/] {parsedValue2}");
        }
        else
        {
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "Failed to parse using parameter.");
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}
