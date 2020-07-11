namespace Usain.Samples.Advanced.AzureQueue.EventListener
{
    using Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services)
        {
            services
                .AddSingleton<IAzureQueueConnectionStringValidator,
                    AzureQueueConnectionStringValidator>();
            services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IValidateOptions
                    <AzureQueueOptions>, AzureQueueOptions>());
            services
                .AddSingleton<IConfigureOptions<AzureQueueOptions>,
                    AzureQueueOptions>();
            services.AddUsainEventListener()
                .AddEventQueue<AzureQueueWrapper>();
            services.AddOptions();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseUsainEventListener();
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapGet(
                        "/",
                        async context =>
                        {
                            await context.Response.WriteAsync(
                                "Usain Samples - Advanced AzureQueue");
                        });
                });
        }
    }
}
