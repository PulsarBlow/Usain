namespace Usain.EventListener.Configuration
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class EventListenerOptions : IConfigureOptions<EventListenerOptions>
    {
        private const string OptionsSectionKeyName = "UsainEventListener";
        private readonly IConfiguration? _configuration;

        public bool IsRequestAuthenticationEnabled { get; set; } = true;
        public string SigningKey { get; set; } = string.Empty;

        [Range(
            1,
            int.MaxValue,
            ErrorMessageResourceName = nameof(Resources
                .OptionsValidation_DeltaTimeTolerance_Range_NotValid),
            ErrorMessageResourceType = typeof(Resources))]
        public int DeltaTimeToleranceSeconds { get; set; } = 300;

        public EventListenerOptions() { }

        public EventListenerOptions(
            IConfiguration configuration)
            => _configuration = configuration;

        public void Configure(
            EventListenerOptions options)
        {
            _configuration?.GetSection(OptionsSectionKeyName)
                .Bind(options);
        }
    }
}
