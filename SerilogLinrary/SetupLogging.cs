using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using static System.DateTime;

namespace SerilogLibrary;

public static class SetupLogging
{
    extension(WebApplicationBuilder builder)
    {
        /// <summary>
        /// Configures the Serilog logger for development environments.
        /// </summary>
        /// <remarks>
        /// This method sets up a logger that writes log entries to a file located in the "LogFiles" directory
        /// within the application's base directory. The log file is named using the current date in the format
        /// "YYYY-MM-DD/Log.txt". The log entries include a timestamp, log level, message, and exception details.
        /// </remarks>
        public void SerilogDevelopmentLoggingSetup()
        {
            ConfigurationManager test = builder.Configuration;

            if (builder.Environment.IsDevelopment())
            {
                builder.Host.UseSerilog((context, configuration) =>
                {

                    configuration.WriteTo.File(
                        Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles"),
                            $"{Now.Year}-{Now.Month:D2}-{Now.Day:D2}", "Log.txt"),
                        rollingInterval: RollingInterval.Infinite,
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}");

                    configuration.MinimumLevel.Information();
                    configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    configuration.MinimumLevel.Override("System", LogEventLevel.Warning);

                });

            }
        }

        public void SerilogProductionLoggingSetup()
        {
            // Implement production logging setup here
        }
    }
}
