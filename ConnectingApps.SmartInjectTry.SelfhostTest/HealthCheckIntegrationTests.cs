using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Refit;
using System.Net;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;

namespace ConnectingApps.SmartInjectTry.SelfhostTest
{
    public class HealthCheckIntegrationTests : IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory = new();
        private readonly IHealthCheckApi _api;
        private readonly HttpClient _client;

        public HealthCheckIntegrationTests()
        {
            _client = _factory.CreateClient();
            _api = RestService.For<IHealthCheckApi>(_client);
        }

        [Theory]
        [InlineData(
            """
            {
              "status": "Healthy",
              "results": {
                "ExampleHealthCheck": {
                  "status": "Healthy",
                  "description": "Example health check is healthy",
                  "data": {
                    "exampleDataKey": "exampleDataValue"
                  }
                }
              }
            }
            """
            )]
        public async Task HealthCheck_ReturnsHealthyStatus(string expectedResponseBody)
        {
            // Act
            var response = await _api.GetHealthStatus();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().NotBeNullOrEmpty();

            var actual = JToken.Parse(response.Content!);
            var expected = JToken.Parse(expectedResponseBody);
            actual.Should().BeEquivalentTo(expected);
        }

        public void Dispose()
        {
            _client.Dispose();
           _factory.Dispose();
        }
    }
}
