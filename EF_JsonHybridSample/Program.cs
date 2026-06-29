using EF_JsonHybridSample.Classes.Core;
using EF_JsonHybridSample.Data;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace EF_JsonHybridSample;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        await using var context = new Context();

        var aced = await context.Applications
            .AsNoTracking()
            .FirstOrDefaultAsync(app => app.ApplicationName == "ACED");

        if (aced is not null)
        {
            AnsiConsole.MarkupLine($"[cyan]FromAddress[/]             {aced.MailSettings.FromAddress}");
            AnsiConsole.MarkupLine($"[cyan]Host[/]                    {aced.MailSettings.Host}");
            AnsiConsole.MarkupLine($"[cyan]Pickup folder[/]           {aced.MailSettings.PickupFolder}");
            AnsiConsole.MarkupLine($"[cyan]Port[/]                    {aced.MailSettings.Port}");
            AnsiConsole.MarkupLine($"[cyan]TimeOut[/]                 {aced.MailSettings.TimeOut}");
            AnsiConsole.MarkupLine($"[cyan]MainDatabaseConnection[/]  {aced.GeneralSettings.MainDatabaseConnection}");
            AnsiConsole.MarkupLine($"[cyan]ServicePath[/]             {aced.GeneralSettings.ServicePath}");
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
        
    }

}
