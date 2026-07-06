using Microsoft.Extensions.Hosting;

namespace CommonLibrary.Web;

/// <summary>
/// Provides extension methods for the <see cref="Microsoft.Extensions.Hosting.IHostEnvironment"/> interface 
/// to determine the current hosting environment.
/// </summary>
public static class HostEnvironmentExtensions
{
    extension(IHostEnvironment environment)
    {
        public bool IsDevelopmentEnvironment()
        {
            ArgumentNullException.ThrowIfNull(environment);

            return string.Equals(
                environment.EnvironmentName,
                Environments.Development,
                StringComparison.OrdinalIgnoreCase);
        }

        public bool IsProductionEnvironment()
        {
            ArgumentNullException.ThrowIfNull(environment);

            return string.Equals(
                environment.EnvironmentName,
                Environments.Production,
                StringComparison.OrdinalIgnoreCase);
        }
    }
}