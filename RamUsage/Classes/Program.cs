using RamUsage.Classes.Core;
using System.Runtime.CompilerServices;

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
