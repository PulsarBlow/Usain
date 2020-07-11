namespace Usain.Samples.Advanced.AzureQueue.EventProcessor
{
    using System.Threading.Tasks;
    using Common;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    class Program
    {
        static async Task Main(
            string[] args)
        {
            await CreateHostBuilder(args)
                .Build()
                .RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(
            string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureServices(Configure);

        private static void Configure(
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

            services.AddUsainEventProcessor()
                .AddEventQueue<AzureQueueWrapper>();

            services.AddLogging();
            services.AddOptions();
        }
    }
}
