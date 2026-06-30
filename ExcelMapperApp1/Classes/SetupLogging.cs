using Serilog;

namespace ExcelMapperApp1.Classes;

internal class SetupLogging
{
    public static void Development()
    {

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{DateTime.Now.Year}-{DateTime.Now.Month:D2}-{DateTime.Now.Day:D2}", "Log.txt"),
                rollingInterval: RollingInterval.Infinite,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
}
