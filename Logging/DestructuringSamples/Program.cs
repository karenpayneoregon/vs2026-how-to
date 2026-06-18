using DestructuringSamples.Classes;
using DestructuringSamples.Classes.Polices;
using Serilog;
using SpectreConsoleLibrary.Core;

namespace DestructuringSamples;

internal partial class Program
{
    static void Main(string[] args)
    {
        Samples.LogFirstLastNamesWithPhone();
        Samples.ConfigureLoggerForIdFirstLastNames();
        Samples.ConfigureAndLogWithSocialSecurity();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


}