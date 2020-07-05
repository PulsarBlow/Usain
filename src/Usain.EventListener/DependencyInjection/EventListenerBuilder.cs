// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public class EventListenerBuilder : IEventListenerBuilder
    {
        /// <summary>
        /// Gets the services
        /// </summary>
        /// <value>The services</value>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventListenerBuilder"/> class
        /// </summary>
        /// <param name="services"></param>
        public EventListenerBuilder(IServiceCollection services)
            => Services = services;
    }
}
