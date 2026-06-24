namespace ViewExternalTools.Models;
public sealed class ExternalTool
{
    public int Index { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Command { get; set; } = string.Empty;

    public string Arguments { get; set; } = string.Empty;

    public string InitialDirectory { get; set; } = string.Empty;

    public bool IsGuiApp { get; set; }

    public bool CloseOnExit { get; set; }
}