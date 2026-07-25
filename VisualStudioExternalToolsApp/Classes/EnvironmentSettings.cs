namespace VisualStudioExternalToolsApp.Classes;

public sealed class EnvironmentSettings
{
    private static readonly Lazy<EnvironmentSettings> Lazy = new(() => new EnvironmentSettings());
    public static EnvironmentSettings Instance => Lazy.Value;

    public string FilePath { get; set; }
    public bool DirectoryExists { get; set; }
    public string FileName { get; set; }
    private EnvironmentSettings()
    {
        var userName = Environment.UserName;
        FilePath = $"C:\\Users\\{userName}\\AppData\\Local\\Microsoft\\VisualStudio\\18.0_869d41d2\\Settings";
        //c:\Users\paynek\AppData\Local\Microsoft\VisualStudio\18.0_869d41d2\Settings\
        DirectoryExists = Directory.Exists(FilePath);
        FileName = Path.Combine(FilePath, "CurrentSettings.vssettings");
    }
}