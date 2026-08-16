using static ActivityLogFinder.Classes.ImportsDefinitions;

namespace ActivityLogFinder.Classes
{
 
    public sealed class AppConfiguration
    {

        private static readonly Lazy<AppConfiguration> Lazy = new Lazy<AppConfiguration>(() => new AppConfiguration());
        public static AppConfiguration Instance => Lazy.Value;

        public bool IsDisabled { get; set; }

        private AppConfiguration()
        {
        }


        public void Disable()
        {
            try
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
#if SERI_LOGGING
                Log.Information("Disabled.");
#endif
                IsDisabled = true;
            }
            catch (Exception e)
            {
#if SERI_LOGGING
                Log.Error(e, "An error occurred while disabling.");
#endif
                IsDisabled = false;
            }
        }

        /// <summary>
        /// Enables the screen lock by resetting the thread execution state to allow the system to enter sleep mode or activate the screensaver.
        /// </summary>
        /// <remarks>
        /// This method sets the thread execution state to <see cref="ImportsDefinitions.EXECUTION_STATE.ES_CONTINUOUS"/>, 
        /// which allows the system to resume its default behavior for screen locking and power-saving features.
        /// </remarks>
        /// <exception cref="System.Exception">
        /// Thrown when an error occurs while enabling the screen lock. The error is logged using Serilog.
        /// </exception>
        public void Enable()
        {
            try
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
#if SERI_LOGGING
                Log.Information("Screen lock enabled.");
#endif
                IsDisabled = false;
            }
            catch (Exception e)
            {
#if SERI_LOGGING
                Log.Error(e, "An error occurred while enabling.");
#endif
                IsDisabled = true;
            }
        }
    }
}
