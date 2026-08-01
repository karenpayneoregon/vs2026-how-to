using Serilog;
using System;
using static DisableScreensaver.Classes.ImportsDefinitions;

namespace DisableScreensaver.Classes
{
    /// <summary>
    /// Represents a configuration for managing the system's screen lock behavior.
    /// </summary>
    /// <remarks>
    /// This class provides functionality to disable or enable the screen lock by manipulating the system's thread execution state.
    /// It is implemented as a singleton to ensure a single instance is used throughout the application.
    /// </remarks>
    /// <threadsafety>
    /// This class is thread-safe due to the use of the <see cref="System.Lazy{T}"/> type for its singleton implementation.
    /// </threadsafety>
    public sealed class LockConfiguration
    {

        private static readonly Lazy<LockConfiguration> Lazy = new Lazy<LockConfiguration>(() => new LockConfiguration());
        public static LockConfiguration Instance => Lazy.Value;

        public bool IsDisabled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LockConfiguration"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is private to enforce the singleton pattern, ensuring that only one instance of the 
        /// <see cref="LockConfiguration"/> class is created and used throughout the application.
        /// It is invoked during the lazy initialization of the <see cref="Instance"/> property.
        /// </remarks>
        /// <seealso cref="LockConfiguration.Instance"/>
        private LockConfiguration()
        {
            // disable screen lock on initialization in Program.cs
        }

        /// <summary>
        /// Disables the screen lock by preventing the system from entering sleep mode or activating the screensaver.
        /// </summary>
        /// <remarks>
        /// This method sets the thread execution state to a combination of 
        /// <see cref="ImportsDefinitions.EXECUTION_STATE.ES_CONTINUOUS"/> and 
        /// <see cref="ImportsDefinitions.EXECUTION_STATE.ES_DISPLAY_REQUIRED"/>, 
        /// ensuring that the display remains active and the system does not lock.
        /// </remarks>
        public void DisableScreenLock()
        {
            try
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
#if SERI_LOGGING
                Log.Information("Screen lock disabled.");
#endif
                IsDisabled = true;
            }
            catch (Exception e)
            {
#if SERI_LOGGING
                Log.Error(e, "An error occurred while disabling screen lock.");
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
        public void EnableScreenLock()
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
                Log.Error(e, "An error occurred while enabling screen lock.");
#endif
                IsDisabled = true;
            }
        }
    }
}
