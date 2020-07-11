namespace Usain.Samples.Advanced.AzureQueue.Common
{
    using Microsoft.Extensions.Options;

    public interface IAzureQueueConnectionStringValidator
    {
        ValidateOptionsResult Validate(
            string connectionString,
            string queueName);
    }
}
