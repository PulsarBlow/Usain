namespace Usain.Samples.Advanced.AzureQueue.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class AzureQueueOptions : IConfigureOptions<AzureQueueOptions>,
        IValidateOptions<AzureQueueOptions>
    {
        private const string OptionsSectionKeyName = "AzureQueue";
        private readonly IConfiguration _configuration;
        private readonly IAzureQueueConnectionStringValidator
            _connectionStringValidator;

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string QueueName { get; set; }

        public AzureQueueOptions() { }

        public AzureQueueOptions(
            IConfiguration configuration,
            IAzureQueueConnectionStringValidator connectionStringValidator)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
            _connectionStringValidator = connectionStringValidator
                ?? throw new ArgumentNullException(
                    nameof(connectionStringValidator));
        }

        public void Configure(
            AzureQueueOptions options)
        {
            _configuration.GetSection(OptionsSectionKeyName)
                .Bind(options);
        }

        public ValidateOptionsResult Validate(
            string name,
            AzureQueueOptions options)
            => _connectionStringValidator.Validate(
                options.ConnectionString,
                options.QueueName);
    }
}
