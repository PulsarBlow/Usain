namespace Usain.EventProcessor.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class EventProcessorOptions
        : IConfigureOptions<EventProcessorOptions>
    {
        private const string OptionsSectionKeyName = "UsainEventProcessor";
        private readonly IConfiguration? _configuration;

        public int CheckUpdateTimeMs { get; set; } = 1000;

        public EventProcessorOptions() { }

        public EventProcessorOptions(
            IConfiguration configuration)
            => _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));

        public void Configure(
            EventProcessorOptions options)
        {
            _configuration?.GetSection(OptionsSectionKeyName)
                .Bind(options);
        }
    }
}
