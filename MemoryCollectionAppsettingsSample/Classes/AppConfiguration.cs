using MemoryCollectionAppsettingsSample.Models;
using Microsoft.Extensions.Configuration;
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace MemoryCollectionAppsettingsSample.Classes;

public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> Lazy = new(() => new AppConfiguration());
    public static AppConfiguration Instance => Lazy.Value;

    public string MainConnection { get; set; }
    public HelpDesk HelpDesk { get; set; }

    private static IConfigurationRoot Configuration { get; set; }


    private AppConfiguration()
    {
        Configuration = LoadConfiguration();

        MainConnection = Configuration.GetConnectionString(nameof(DataConnections.MainConnection))!;
        HelpDesk = Configuration.GetSection(nameof(HelpDesk)).Get<HelpDesk>()!;
    }

    private static IConfigurationRoot LoadConfiguration() =>
        new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "HelpDesk:Phone", "555-555-1234" },
                { "HelpDesk:Email", "ServiceDesk@SomeCompany.net" }
            })
            .Build();
}
