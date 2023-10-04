using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Refit;
using System.Net;

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

        [Fact]
        public async Task HealthCheck_ReturnsHealthyStatus()
        {
            // Act
            var response = await _api.GetHealthStatus();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK); // Assert the status code
            response.Content.Should().NotBeNull(); // Assert the content is not null

            // Further assertions based on your expected JSON structure.
            // You might want to deserialize the response.Content and check individual properties.
        }

        public void Dispose()
        {
            _client.Dispose();
           _factory.Dispose();
        }
    }
}
