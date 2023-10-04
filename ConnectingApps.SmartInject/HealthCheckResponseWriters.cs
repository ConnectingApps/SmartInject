using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConnectingApps.SmartInject
{
    internal class HealthCheckResponseWriters
    {
        /// <summary>
        /// method that writes the health check response as JSON baded on
        /// https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-6.0#customize-output
        /// </summary>
        /// <param name="context"></param>
        /// <param name="healthReport"></param>
        /// <returns></returns>
        internal static Task WriteJsonResponse(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions { Indented = true };

            using var memoryStream = new MemoryStream();
            using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("status", healthReport.Status.ToString());
                jsonWriter.WriteStartObject("results");

                foreach (var healthReportEntry in healthReport.Entries)
                {
                    jsonWriter.WriteStartObject(healthReportEntry.Key);
                    jsonWriter.WriteString("status",
                        healthReportEntry.Value.Status.ToString());
                    jsonWriter.WriteString("description",
                        healthReportEntry.Value.Description);
                    jsonWriter.WriteStartObject("data");

                    foreach (var item in healthReportEntry.Value.Data)
                    {
                        jsonWriter.WritePropertyName(item.Key);

                        JsonSerializer.Serialize(jsonWriter, item.Value,
                            item.Value?.GetType() ?? typeof(object));
                    }

                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            return context.Response.WriteAsync(
                Encoding.UTF8.GetString(memoryStream.ToArray()));
        }
    }
}
