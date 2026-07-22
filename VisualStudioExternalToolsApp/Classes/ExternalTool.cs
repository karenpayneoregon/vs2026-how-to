namespace VisualStudioExternalToolsApp.Classes;

/// <summary>
/// Represents an external tool configuration in Visual Studio.
/// </summary>
/// <remarks>
/// This record encapsulates the properties of an external tool, including its index, title, 
/// command, arguments, initial directory, and additional flags indicating whether it is a GUI application 
/// and whether it should close on exit.
/// </remarks>
public sealed record ExternalTool
{
    public int Index { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Command { get; init; } = string.Empty;
    public string Arguments { get; init; } = string.Empty;
    public string InitialDirectory { get; init; } = string.Empty;
    public bool IsGuiApp { get; init; }
    public bool CloseOnExit { get; init; }
    public string Name => $"External Command {Index +1}";
    // write ToString() method to return a string representation of the object
    public override string ToString()
    {
        return $"Index: {Index}, Title: \"{Title}\", Command: \"{Command}\", " +
               $"Arguments: \"{Arguments}\", InitialDirectory: \"{InitialDirectory}\", " +
               $"Name: \"{Name}\", " +
               $"IsGuiApp: {IsGuiApp}, CloseOnExit: {CloseOnExit}";
    }
}

