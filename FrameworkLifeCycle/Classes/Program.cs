using FrameworkLifeCycle.Classes.Configuration;
using System.Runtime.CompilerServices;
using SpectreConsoleLibrary.Core;

// ReSharper disable once CheckNamespace
namespace FrameworkLifeCycle;

internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {

        SetupLogging.Development();
        SpectreConsoleHelpers.SetEncoding();

    }

}
