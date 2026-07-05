using Microsoft.Extensions.Hosting;

namespace ParameterizedCollectionModeSample.Classes;

public static class HostEnvironmentExtensions
{
    public static bool IsDevelopmentEnvironment(this IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);

        return string.Equals(
            environment.EnvironmentName,
            Environments.Development,
            StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsProductionEnvironment(this IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);

        return string.Equals(
            environment.EnvironmentName,
            Environments.Production,
            StringComparison.OrdinalIgnoreCase);
    }
}