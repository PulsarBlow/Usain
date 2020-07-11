# Usain Samples - Advanced - Azure Queue

## Introduction

## Two hosts
This sample demonstrates how to run an EventListener and an EventProcessor in two different hosts.


* EventListener: hosted in a WebHost (ASP.NET Core)
* EventProcessor: hosted in a generic Host (Console App)

Both rely on the [HostBuilder] infrastructure.
A host is an object that encapsulates an app's resources, such as:

* Dependency injection (DI)
* Logging
* Configuration
* IHostedService implementations

When a host starts, it calls IHostedService.StartAsync on each implementation of IHostedService registered in the service container's collection of hosted services.

This part is useful for us because UsainEventProcessor main processing task is an implementation of the IHostedService interface.

## Azure Queue
The listener and the processor exchange background work (event to process) throught the [IEventQueue] interface.\
In that sample, we use an [Azure Queue] as the main queue infrastructure.

[AzureQueueWrapper] class implements the IEventQueue interface. It is a wrapper around an Azure QueueClient.

# Getting started

To run that sample, you need to create an Azure Storage Account and retrieve the Storage connection string.
Use that connection string to udpdate the value of the AzureQueue:ConnectionString settings in the appsettings.json file of the EventListener and the EventProcessor.

You can then run them both in 2 terminals:

_Terminal 1_

```shell
cd ./samples/O2.advanced.azurequeue
dotnet run -p Usain.Samples.Advanced.AzureQueue.EventListener
```
_Terminal 2_

```shell
cd ./samples/O2.advanced.azurequeue
dotnet run -p Usain.Samples.Advanced.AzureQueue.EventProcessor
```

[HostBuilder]: <https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1>
[IEventQueue]: <../../src/Usain.Core/Infrastructure/IEventQueue.cs>
[Azure Queue]: <https://docs.microsoft.com/en-us/azure/storage/queues/storage-queues-introduction>
[AzureQueueWrapper]: <Usain.Samples.Advanced.AzureQueue.Common/AzureQueueWrapper.cs>



