using ConsoleConfigurationLibrary.Classes;
using ConsoleHelperLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using RamUsage.Classes.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace RamUsage;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        SpectreConsoleHelpers.SetEncoding();
    }

}
