using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace ConnectingApps.SmartInject
{
    public class JsonHealthCheckOptions : HealthCheckOptions
    {
        public JsonHealthCheckOptions()
        {
            ResponseWriter = HealthCheckResponseWriters.WriteResponse;
        }
    }
}
