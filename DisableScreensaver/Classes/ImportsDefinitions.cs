using System;
using System.Runtime.InteropServices;

namespace DisableScreensaver.Classes
{
    public static class ImportsDefinitions
    {
        // 1. Import the SetThreadExecutionState function from kernel32.dll
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        // 2. Define the necessary flags for the execution state
        [Flags]
        public enum EXECUTION_STATE : uint
        {
            // Forces the system to be in the working state by resetting the system idle timer.
            ES_SYSTEM_REQUIRED = 0x00000001,

            // Forces the display to be on by resetting the display idle timer (Prevents Screen Saver).
            ES_DISPLAY_REQUIRED = 0x00000002,

            // Informs the system that the state being set should remain in effect until the next call.
            ES_CONTINUOUS = 0x80000000
        }

    }
}
