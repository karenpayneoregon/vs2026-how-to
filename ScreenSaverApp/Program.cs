using Spectre.Console;

// ReSharper disable InconsistentNaming

namespace ScreenSaverApp
{
    partial class Program
    {

        
        /// <summary>
        /// This method prevents the screen saver and monitor sleep mode from activating by setting the thread's execution state.
        /// It waits for user input to restore the screen saver settings to their normal state.
        /// Explicitly resetting the execution state is a best practice to ensure proper cleanup.
        /// The entry point of the ScreenSaverApp application.
        /// </summary>>
        static void Main(string[] args)
        {
            AnsiConsole.MarkupLine("[cyan]Preventing screen saver from starting...[/]");

            // 3. Disable the screen saver and monitor sleep
            // This tells Windows that this thread requires the display to stay active continuously.
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED);

            AnsiConsole.MarkupLine("[cyan]Press ENTER to allow the screen saver to work normally again.[/]");
            Console.ReadLine();

            // 4. Reset the state back to normal
            // Failing to call this is usually safe as Windows clears it when the thread/app exits,
            // but explicit cleanup is best practice.
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);

            AnsiConsole.MarkupLine("[cyan]Screen saver settings restored.[/]");
            Console.ReadLine();
        }
    }
}
