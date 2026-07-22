using System;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace ScreenSaverApp
{
    partial class Program
    {
        // 1. Import the SetThreadExecutionState function from kernel32.dll
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        // 2. Define the necessary flags for the execution state
        [Flags]
        private enum EXECUTION_STATE : uint
        {
            // Forces the system to be in the working state by resetting the system idle timer.
            ES_SYSTEM_REQUIRED = 0x00000001,

            // Forces the display to be on by resetting the display idle timer (Prevents Screen Saver).
            ES_DISPLAY_REQUIRED = 0x00000002,

            // Informs the system that the state being set should remain in effect until the next call.
            ES_CONTINUOUS = 0x80000000
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Preventing screen saver from starting...");

            // 3. Disable the screen saver and monitor sleep
            // This tells Windows that this thread requires the display to stay active continuously.
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED);

            Console.WriteLine("Press ENTER to allow the screen saver to work normally again.");
            Console.ReadLine();

            // 4. Reset the state back to normal
            // Failing to call this is usually safe as Windows clears it when the thread/app exits,
            // but explicit cleanup is best practice.
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);

            Console.WriteLine("Screen saver settings restored.");
        }
    }
}
