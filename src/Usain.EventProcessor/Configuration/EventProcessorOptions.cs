namespace Usain.EventProcessor.Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class EventProcessorOptions
        : IConfigureOptions<EventProcessorOptions>
    {
        private readonly IConfiguration? _configuration;

        public const string OptionsSectionKeyName = "UsainEventProcessor";
        public int CheckUpdateTimeMs { get; set; } = 1000;

        public EventProcessorOptions() { }

        public EventProcessorOptions(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(
            EventProcessorOptions options)
        {
            _configuration?.GetSection(OptionsSectionKeyName)
                .Bind(options);
        }
    }
}
