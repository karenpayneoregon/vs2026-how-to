using System.Text.Json;
using System.Xml.Linq;
using ViewExternalTools.Models;

namespace ViewExternalTools.Classes;

internal class Operations
{
    /// <summary>
    /// Gets or sets the file path to the current Visual Studio settings file. ✔️
    /// </summary>
    /// <remarks>
    /// The default value points to the current settings file for Visual Studio, 
    /// located in the user's local application data directory.
    /// </remarks>
    public static string FileName { get; set; } = 
        @$"C:\Users\{Environment.UserName}\AppData\Local\Microsoft\VisualStudio\18.0_4385fe6c\Settings\CurrentSettings.vssettings";
        
    public static bool FileExists => File.Exists(FileName);

    public static string JsonFileName => "tools.json";

  

    /// <summary>
    /// Writes a collection of external tools to a JSON file at the specified output path.
    /// </summary>
    /// <param name="tools">
    /// A collection of <see cref="ExternalTool"/> objects representing the external tools to be serialized and written to the file.
    /// </param>
    /// <remarks>
    /// This method serializes the provided collection of external tools into a human-readable JSON format
    /// and writes it to the specified file. If the file already exists, it will be overwritten.
    /// </remarks>
    public static void WriteToolsJson(IEnumerable<ExternalTool> tools)
    {
        // If you’re on .NET 7+ and want UTF-8, use File.WriteAllBytes with JsonSerializer.SerializeToUtf8Bytes
        File.WriteAllText(JsonFileName, JsonSerializer.Serialize(tools, Indented));
    }
    /// <summary>
    /// Reads the external tools defined in the settings file specified by <see cref="FileName"/>.
    /// </summary>
    /// <returns>
    /// An enumerable collection of <see cref="ExternalTool"/> objects representing the external tools
    /// defined in the settings file.
    /// </returns>
    /// <remarks>
    /// This method parses the XML structure of the settings file to extract information about user-created
    /// external tools. Each tool is represented as an <see cref="ExternalTool"/> object.
    /// If the "Environment_ExternalTools" category or its tools are not found, the method returns an empty collection.
    /// </remarks>
    /// <seealso cref="ReadCurrentSettingsFile"/>
    /// <seealso cref="WriteToolsJson(string, IEnumerable{ExternalTool})"/>
    public static IEnumerable<ExternalTool> ReadExternalTools()
    {
        var doc = XDocument.Load(FileName);

        // Find the Environment_ExternalTools category.
        var toolsRoot = doc
            .Descendants("Category")
            .FirstOrDefault(c => (string)c.Attribute("name") == "Environment_ExternalTools")
            ?.Element("ExternalTools");

        if (toolsRoot == null)
            yield break;   // Nothing there – bail out.

        foreach (var tool in toolsRoot.Elements("UserCreatedTool"))
        {
            yield return new ExternalTool
            {
                Index = (int?)tool.Element("Index") ?? -1,
                Title = (string?)tool.Element("Title") ?? "",
                Command = (string?)tool.Element("Command") ?? "",
                Arguments = (string?)tool.Element("Arguments") ?? "",
                InitialDirectory = (string?)tool.Element("InitialDirectory") ?? "",
                IsGuiApp = (bool?)tool.Element("IsGUIapp") ?? false,
                CloseOnExit = (bool?)tool.Element("CloseOnExit") ?? false
            };
        }
    }

    public static JsonSerializerOptions Indented => new() { WriteIndented = true };

}
