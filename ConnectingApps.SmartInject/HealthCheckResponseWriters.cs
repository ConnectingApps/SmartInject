using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConnectingApps.SmartInject
{
    public static class HealthCheckResponseWriters
    {
        /// <summary>
        /// method that writes the health check response as JSON baded on
        /// https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-6.0#customize-output
        /// </summary>
        /// <param name="context"></param> // This is dynamic to avoid a dependency on Microsoft.AspNetCore.Http.Abstractions which is deprecated or a .NET Core dependency which is not available in .NET Standard
        /// <param name="healthReport"></param>
        /// <returns></returns>
        public static Task WriteJsonResponse(dynamic context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions { Indented = true };
            using (var memoryStream = new MemoryStream())
            {
                using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
                {
                    jsonWriter.WriteStartObject();
                    jsonWriter.WriteString("status", healthReport.Status.ToString());
                    jsonWriter.WriteStartObject("results");

                    foreach (var healthReportEntry in healthReport.Entries)
                    {
                        jsonWriter.WriteStartObject(healthReportEntry.Key);
                        jsonWriter.WriteString("status", healthReportEntry.Value.Status.ToString());
                        jsonWriter.WriteString("description", healthReportEntry.Value.Description);
                        jsonWriter.WriteStartObject("data");

                        foreach (var item in healthReportEntry.Value.Data)
                        {
                            jsonWriter.WritePropertyName(item.Key);
                            JsonSerializer.Serialize(jsonWriter, item.Value, item.Value?.GetType() ?? typeof(object));
                        }

                        jsonWriter.WriteEndObject();
                        jsonWriter.WriteEndObject();
                    }

                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();
                }

                // Avoiding extension method (because of dynamic type) at the end by using HttpResponse.Body.WriteAsync method directly
                var buffer = memoryStream.ToArray();
                return context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
        }
    }
}
