namespace Usain.Samples.Advanced.AzureQueue.Common
{
    using System;
    using Azure.Storage.Queues;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    // Azure StorageConnectionString is now internal ..
    // we don't have access to Azure SDK internal validation logic (eg. StorageConnectionString.Parse)
    // This class just serves as a fast fail check when validating options
    // rather than waiting for a AzureQueueClient instance initialization (late in the pipeline)
    // Please don't use that in production
    public class AzureQueueConnectionStringValidator
        : IAzureQueueConnectionStringValidator
    {
        private const string ErrorMessage =
            "Azure Queue connection string is not valid. Check your configuration";
        private readonly ILogger _logger;

        public AzureQueueConnectionStringValidator(
            ILogger<AzureQueueConnectionStringValidator> logger)
            => _logger = logger;

        public ValidateOptionsResult Validate(
            string connectionString,
            string queueName)
        {
            try
            {
                new QueueClient(
                    connectionString,
                    queueName);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    ErrorMessage);
                return ValidateOptionsResult.Fail(ErrorMessage);
            }

            return ValidateOptionsResult.Success;
        }
    }
}
