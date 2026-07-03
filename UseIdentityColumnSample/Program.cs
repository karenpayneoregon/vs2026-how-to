using Microsoft.EntityFrameworkCore;
using Serilog;
using Spectre.Console;
using UseIdentityColumnSample.Classes;
using UseIdentityColumnSample.Data;
using UseIdentityColumnSample.Models;
using static SpectreConsoleLibrary.Core.SpectreConsoleHelpers;

namespace UseIdentityColumnSample;

internal partial class Program
{
    static async Task Main(string[] args)
    {

        // add first records
        await DataOperations.AddCustomers();

        // add another record to show identity column is working
        await DataOperations.AddCustomer();
        
        await DataOperations.PrintCustomers();

        ExitPrompt(Justify.Left);
    }

}
