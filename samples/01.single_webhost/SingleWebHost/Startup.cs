namespace SingleWebHost
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using global::Slack.NetStandard;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Usain.Core.Infrastructure;
    using Usain.EventProcessor.EventReactions;
    using Usain.Slack.Models;
    using UsainReactions;

    public class Startup
    {
        private readonly IEventQueue<EventWrapper> _eventQueue =
            new InMemoryEventQueue();

        private readonly IConfiguration _configuration;

        public Startup(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddOptions()
                .Configure<SlackOptions>(
                    _configuration.GetSection(SlackOptions.OptionsKeyName));
            services.AddHttpClient(
                "slack",
                (
                    sp,
                    client) =>
                {
                    var options =
                        sp.GetRequiredService<IOptions<SlackOptions>>();
                    client
                            .DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(
                            "Bearer",
                            options.Value.BotToken);
                });
            services.AddUsainEventListener()
                .AddEventQueue(sp => _eventQueue);
            services.AddUsainEventProcessor()
                .AddEventQueue(sp => _eventQueue);
            services.AddTransient<ISlackApiClient>(
                sp =>
                {
                    var httpClientFactory =
                        sp.GetRequiredService<IHttpClientFactory>();
                    return new SlackWebApiClient(
                        httpClientFactory.CreateClient("slack"));
                });
            services
                .AddTransient(
                    typeof(IEventReactionFactory<>),
                    typeof(
                        CustomReactionFactory<>));
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseUsainEventListener();
        }
    }
}
