using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ConnectingApps.SmartInjectTry.SelfhostTest;

internal class TestWebApplicationFactory<TToResolve> : WebApplicationFactory<Program>
    where TToResolve : class
{
    private IServiceScope? _serviceScope;

    private TToResolve? TypeToSet { get; set; }

    public Lazy<TToResolve?> TypeToResolve { get; set; }

    public TestWebApplicationFactory()
    {
        TypeToResolve = new Lazy<TToResolve?>(() => TypeToSet);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var sp = services.BuildServiceProvider();


            _serviceScope = sp.CreateScope();
            var scopedServices = _serviceScope.ServiceProvider;
            TypeToSet = scopedServices.GetRequiredService<TToResolve>();
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _serviceScope?.Dispose();
    }

}