using DisableScreensaver.Classes;
using System;
using System.Windows.Forms;
using Serilog;
using static DisableScreensaver.Classes.ImportsDefinitions;

namespace DisableScreensaver
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles() ;
            Application.SetCompatibleTextRenderingDefault(false);
            
            SetupLogging.Development();
            LockConfiguration.Instance.DisableScreenLock();


            Application.Run(new DisableScreenSaverForm());
        }

        /// <summary>
        /// Disables the screen lock by setting the thread execution state to prevent the display
        /// from turning off or the system from entering sleep mode.
        /// </summary>
        /// <remarks>
        /// This method uses the <see cref="DisableScreensaver.Classes.ImportsDefinitions.SetThreadExecutionState"/> 
        /// function to ensure the screen remains active. It logs the operation status using Serilog.
        /// </remarks>
        /// <exception cref="System.Exception">
        /// Thrown if an error occurs while attempting to disable the screen lock.
        /// </exception>
        private static void DisableScreenLock()
        {

            try
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
                Log.Information("Screen lock disabled.");
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while disabling screen lock.");
            }
        }
    }
    
}
