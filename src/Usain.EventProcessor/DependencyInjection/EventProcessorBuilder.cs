// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public class EventProcessorBuilder : IEventProcessorBuilder
    {
        /// <summary>
        /// Gets the services
        /// </summary>
        /// <value>The services</value>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessorBuilder"/> class
        /// </summary>
        /// <param name="services"></param>
        public EventProcessorBuilder(
            IServiceCollection services)
        {
            Services = services;
        }
    }
}
