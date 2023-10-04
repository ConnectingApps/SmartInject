using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ConnectingApps.SmartInjectTry
{
    public class ExampleHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var healthCheckData = new Dictionary<string, object>
            {
                { "exampleDataKey", "exampleDataValue" }
            };

            return Task.FromResult(
                HealthCheckResult.Healthy("Example health check is healthy", healthCheckData));
        }
    }
}
