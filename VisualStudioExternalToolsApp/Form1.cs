using System.ComponentModel;
using System.Text;
using VisualStudioExternalToolsApp.Classes;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace VisualStudioExternalToolsApp;

public partial class Form1 : Form
{
    private BindingList<ExternalTool> _bindingList;
    private readonly BindingSource _bindingSource = new();
    public Form1()
    {
        InitializeComponent();

        Shown += Form1_Shown;
        titleLabel.Click += TitleLabelClickHandler;
    }

    private void TitleLabelClickHandler(object? sender, EventArgs e)
    {
        TitleTextBox.Focus();
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {
        if (!EnvironmentSettings.Instance.DirectoryExists) return;

        List<ExternalTool> tools = ExternalToolsOperations
            .ReadExternalTools(EnvironmentSettings.Instance.FileName)
            .ToList();

        if (tools.Count >0)
        {
            ExportSettings(tools);
        }
        else
        {
            MessageBox.Show(@"No external tools found in the settings file.");
        }

        _bindingList = new BindingList<ExternalTool>(tools);
        _bindingSource.DataSource = _bindingList;

        NameTextBox.DataBindings.Add("Text", _bindingSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
        CommandTextBox.DataBindings.Add("Text", _bindingSource, "Command", true, DataSourceUpdateMode.OnPropertyChanged);
        TitleTextBox.DataBindings.Add("Text", _bindingSource, "Title", true, DataSourceUpdateMode.OnPropertyChanged);
        InitialDirectoryTextBox.DataBindings.Add("Text", _bindingSource, "InitialDirectory", true, DataSourceUpdateMode.OnPropertyChanged);

        BindingNavigator1.BindingSource = _bindingSource;
    }

    /// <summary>
    /// Exports the settings of the provided external tools to a text file.
    /// </summary>
    /// <param name="tools">A list of <see cref="ExternalTool"/> objects representing the external tools to export.</param>
    /// <remarks>
    /// This method writes the details of each external tool to a file located at "H:\ExternalToolsSettings.txt". 
    /// If the directory "H:\" does not exist, the method exits without performing any action. 
    /// After successfully writing the file, a message box is displayed to indicate completion.
    /// </remarks>
    private void ExportSettings(List<ExternalTool> tools)
    {
        if (!Directory.Exists("H:\\")) return;
        
        var fileName = "H:\\ExternalToolsSettings.txt";
        StringBuilder sb = new StringBuilder();
        tools.ForEach(tool =>
        {
            sb.AppendLine(tool.ToString());
            sb.AppendLine();
        });

        File.WriteAllText(fileName, sb.ToString());
        MessageBox.Show(@"Done export");

    }

    private void OpenSourceFileButton_Click(object sender, EventArgs e)
    {

        var fileOperations = new FileOperations();
        fileOperations.OpenSettingsFile(EnvironmentSettings.Instance.FileName);
    }
}