using ConsoleConfigurationLibrary.Models;

namespace PropertyExistsApp.Classes;
public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> _lazyInstance = new(() => new AppConfiguration());
    public static AppConfiguration Instance { get; } = _lazyInstance.Value;

    public HelpDesk HelpDesk { get; }
    public ConnectionStrings ConnectionStrings { get;}

    private AppConfiguration()
    {

        HelpDesk = JsonConfiguration.ReadSection<HelpDesk>(nameof(HelpDesk));
        ConnectionStrings = JsonConfiguration.ReadSection<ConnectionStrings>(nameof(ConnectionStrings));

    }
}

