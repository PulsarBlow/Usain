namespace Usain.EventProcessor.Tests.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using Core.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Moq;
    using Slack.Models;
    using Xunit;

    public class EventProcessorBuilderExtensionsTest
    {
        private readonly Mock<IEventProcessorBuilder> _builderMock =
            new Mock<IEventProcessorBuilder>();
        private readonly ServiceCollection _serviceCollection =
            new ServiceCollection();

        public EventProcessorBuilderExtensionsTest()
        {
            _builderMock
                .SetupGet(x => x.Services)
                .Returns(_serviceCollection);
        }

        [Fact]
        public void
            AddEventQueue_Register_Singleton_If_Not_Already_Registered()
        {
            _serviceCollection.Add(
                new ServiceDescriptor(
                    typeof(IEventQueue<EventWrapper>),
                    typeof(EventQueueFirst),
                    ServiceLifetime.Singleton));
            var builder = _builderMock.Object;
            builder.AddEventQueue<EventQueueSecond>();

            Assert.Equal(
                1,
                _serviceCollection.Count(
                    x => x.Lifetime == ServiceLifetime.Singleton
                        && x.ServiceType == typeof(IEventQueue<EventWrapper>)
                        && x.ImplementationType == typeof(EventQueueFirst)));
        }

        [Fact]
        public void AddEventQueue_Registers_Singleton()
        {
            var builder = _builderMock.Object;
            builder.AddEventQueue<EventQueueFirst>();

            Assert.Equal(
                1,
                _serviceCollection.Count(
                    x => x.Lifetime == ServiceLifetime.Singleton
                        && x.ServiceType == typeof(IEventQueue<EventWrapper>)
                        && x.ImplementationType == typeof(EventQueueFirst)));
        }

        [Fact]
        public void
            AddEventQueue_Register_Singleton_With_ImplementationFactory()
        {
            var builder = _builderMock.Object;
            Func<IServiceProvider, EventQueueFirst> factory =
                sp => new EventQueueFirst();
            builder.AddEventQueue(factory);

            Assert.Equal(
                1,
                _serviceCollection.Count(
                    x => x.Lifetime == ServiceLifetime.Singleton
                        && x.ServiceType == typeof(IEventQueue<EventWrapper>)
                        && x.ImplementationFactory == factory));
        }

        [Fact]
        public void
            AddEventQueue_Register_Singleton_With_ImplementationFactory_If_Not_Already_Registered()
        {
            var builder = _builderMock.Object;
            Func<IServiceProvider, EventQueueFirst> factoryFirst =
                sp => new EventQueueFirst();
            _serviceCollection.Add(
                new ServiceDescriptor(
                    typeof(IEventQueue<EventWrapper>),
                    factoryFirst,
                    ServiceLifetime.Singleton));

            Func<IServiceProvider, EventQueueSecond> factorySecond =
                sp => new EventQueueSecond();
            builder.AddEventQueue(factorySecond);

            Assert.Equal(
                1,
                _serviceCollection.Count(
                    x => x.Lifetime == ServiceLifetime.Singleton
                        && x.ServiceType == typeof(IEventQueue<EventWrapper>)
                        && x.ImplementationFactory == factoryFirst));
        }

        [Fact]
        public void AddPlatformServices_Add_Configuration()
        {
            var builder = _builderMock.Object;
            builder.AddPlatformServices();

            Assert.Equal(
                1,
                _serviceCollection.Count(
                    x => x.Lifetime == ServiceLifetime.Singleton
                        && x.ServiceType
                        == typeof(
                            IConfigureOptions<EventProcessorOptions>)
                        && x.ImplementationType
                        == typeof(EventProcessorOptions)));
        }


        private class EventQueueFirst : IEventQueue<EventWrapper>
        {
            public Task EnqueueAsync(
                EventWrapper item,
                CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }

            public Task<bool> TryDequeueAsync(
                out EventWrapper item,
                CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }

        private class EventQueueSecond : IEventQueue<EventWrapper>
        {
            public Task EnqueueAsync(
                EventWrapper item,
                CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }

            public Task<bool> TryDequeueAsync(
                out EventWrapper item,
                CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
