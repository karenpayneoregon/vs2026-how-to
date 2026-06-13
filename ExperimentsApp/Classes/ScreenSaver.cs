using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ExperimentsApp.Classes;

/// <summary>
/// Represents a utility class for managing screen saver and power settings.
/// </summary>
/// <remarks>
/// This class provides methods to disable and enable the screensaver and monitor sleep functionality 
/// by interacting with the Windows API.
/// </remarks>
internal class ScreenSaver
{
    // Import SetThreadExecutionState from kernel32.dll
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    // Define the necessary flags for execution states
    [Flags]
    private enum EXECUTION_STATE : uint
    {
        ES_SYSTEM_REQUIRED = 0x00000001,  // Keeps the system from sleeping
        // Forces the display to remain on (prevents screensaver and display sleep)
        ES_DISPLAY_REQUIRED = 0x00000002,
        // Informs Windows that the setting should remain in effect until reset
        ES_CONTINUOUS = 0x80000000
    }

    /// <summary>
    /// Prevents the screensaver from activating and stops the monitor from sleeping.
    /// </summary>
    public static void DisableScreensaver()
    {
        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
        Console.WriteLine("Disabled");
    }

    /// <summary>
    /// Restores default Windows power and screensaver behavior.
    /// </summary>
    public static void EnableScreensaver()
    {
        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
    }
}
