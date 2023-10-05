using Refit;

namespace ConnectingApps.SmartInjectTry.SelfhostTest
{
    public interface IHealthCheckApi
    {
        [Get("/healthz")]
        Task<ApiResponse<string>> GetHealthStatus();
    }
}
