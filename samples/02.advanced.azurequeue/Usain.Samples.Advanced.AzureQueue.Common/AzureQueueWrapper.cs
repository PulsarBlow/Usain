namespace Usain.Samples.Advanced.AzureQueue.Common
{
    using System;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Azure;
    using Azure.Core;
    using Azure.Storage.Queues;
    using Core.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Slack.Models;

    public class AzureQueueWrapper : IEventQueue<EventWrapper>
    {
        private readonly ILogger _logger;
        private readonly QueueClientOptions _queueClientOptions =
            new QueueClientOptions();
        private readonly QueueClient _queueClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions =
            new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = false,
            };

        public AzureQueueWrapper(
            ILogger<AzureQueueWrapper> logger,
            IOptions<AzureQueueOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            var currentOptions = options?.Value
                ?? throw new ArgumentNullException(nameof(options));
            _queueClientOptions.Retry.Mode = RetryMode.Exponential;
            _queueClientOptions.Retry.MaxRetries = 3;
            _queueClient = new QueueClient(
                currentOptions.ConnectionString,
                currentOptions.QueueName,
                _queueClientOptions);
        }

        public async Task EnqueueAsync(
            EventWrapper item,
            CancellationToken cancellationToken)
        {
            try
            {
                await _queueClient.CreateIfNotExistsAsync(
                    cancellationToken:
                    cancellationToken);
                var message = JsonSerializer.Serialize(
                    item,
                    _jsonSerializerOptions);
                await _queueClient.SendMessageAsync(
                    message,
                    cancellationToken);
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to enqueue event");
                throw;
            }
        }

        public async Task<EventWrapper> DequeueAsync(
            CancellationToken cancellationToken)
        {
            EventWrapper item = null;

            try
            {
                var response =
                    await _queueClient.ReceiveMessagesAsync(cancellationToken); // retrieves 1 message
                if (response.Value.Length == 0)
                {
                    _logger.LogDebug("Queue is empty");
                    return null;
                }

                _logger.LogInformation("An event has been dequeued");
                var messageText = response.Value[0].MessageText;
                _logger.LogDebug("Dequeued event message: {Message}", messageText);
                item = JsonSerializer.Deserialize<EventWrapper>(
                    response.Value[0]
                        .MessageText);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to dequeue event");
            }

            return item;
        }
    }
}
