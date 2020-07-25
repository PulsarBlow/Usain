namespace Usain.EventListener.Integration.Tests.Helpers
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class HostBuilderFactory
    {
        public static IHostBuilder CreateHostBuilder()
        {
            return new HostBuilder()
                .ConfigureWebHost(
                    webHost =>
                    {
                        // Add TestServer
                        webHost.UseTestServer();
                        webHost.ConfigureServices(ConfigureServices);
                        webHost.Configure(ConfigureApp);
                    });
        }

        private static void ConfigureServices(
            IServiceCollection services)
        {
            services.AddUsainEventListener(
                    config => config.IsRequestAuthenticationEnabled = false)
                .AddEventQueue<InMemoryEventQueue>();
        }

        private static void ConfigureApp(
            IApplicationBuilder app)
        {
            app.UseUsainEventListener();
        }
    }
}
