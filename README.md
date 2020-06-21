<p align="center">
  <img alt="Usain logo" src="assets/icons/icon.svg" width="90" />
</p>
<h1 align="center">
  Usain
</h1>

<h4 align="center">A fast and scalable .NET Core Slack events server.</h4>


# Introduction

There are plenty of existing Slack integration frameworks. Slack engineering team even provide its own implementation with Bolt (JS, Java).\
However, while trying to integrate Slack using a .NET Core stack, i didn't find any project that was simple to use, while being fast and scalable...

Usain is simple, fast and scalable.\
It provides the event ingesting and processing infrastructure required to create an reactive (interactive) Slack App.

## Simple

Usain addresses only two concerns:
- Ingesting your slack event as fast as possible.
- Processing each and every ingested event in a customizable and reliable way.

This limitation of concerns enforce simplicity.

Adding an _Event Reaction_ (how to handle an event) is as simple as providing an implementation of the `IEventReaction<>` to the DI container. This _Event Reaction_ will be automatically used by the Event Reaction pipeline.

Head down to the [Getting started](#Getting started) section to learn how to implement an EventReaction.

## Fast

### ASP.NET Core
ASP.NET Core is fast, really fast.

According to [Tech Empower Benchmarks](https://www.techempower.com/benchmarks/#section=data-r19&hw=ph&test=plaintext) ìt is one of the fastest web stack on the market as of today (jun 2020).

Surprisingly it is even faster than a pure Rust or C++ web server. This is probably due to a lack of maturity in these implementations.

That said, in term of pure raw performance, ASP.NET Core is at light years from a Node or Java web stack, which would probably be your choice if you had to use the genuine Slack SDK (Bolt).

### MVC free

Usain server is a low level ASP.NET Core implementation. It does not use ASP.NET Core MVC.

This saves us from layers of indirection and unnecessary interactions with the MVC pipeline. This helps us to earn extra performance bits.

### System.Text.Json

Usain uses [System.Text.Json](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview) for Json serialization operations.

 Compared to the legacy `Newtonsoft` library, `System.Text.Json` design emphasizes high performance and low memory allocation over an extensive feature set. Built-in UTF-8 support optimizes the process of reading and writing JSON text encoded as UTF-8, which is the most prevalent encoding for data on the web and files on disk.

 ### LoggerMessage

 Usain uses [LoggerMessage](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/loggermessage?view=aspnetcore-3.1) for logging operations.

 LoggerMessage creates cacheable delegates that require fewer object allocations and reduced computational overhead compared to logger extension methods, such as LogInformation and LogDebug. It is the recommended pattern for high-performance logging scenarios.

## Scalable

### De-coupled events ingestion and reaction
As recommended by Slack, event ingestion and processing should de-coupled. Slack server expects your event ingestion to be fast and error free otherwise it will block your endpoint and stop sending events. You don't want that to happen in a production scenario.

Usain has been designed to follow that recommendation from the ground up. It uses an intermediate Event Bus abstraction to de-couple event ingestion from event reaction.

We provide a default in-memory implementation for basic scenarios, but swapping this default for something more scalable is a matter of a single line of configuration.

### Extensible and flexible

Usain is extensible in almost every area of the Event ingestion and reaction pipeline.

We provide default implementations but you can substitute them with whatever your consider a better fit for use cases or infrastructure requirements.

Internally we use a CQRS pattern empowered by a mediated event design. This gives you a lot more flexibility when implementing your business workflows.

## Usain ? 🏃

In reference to Slack integration framework (JS, Java) which is named Bolt.

Yes, you got it, Usain vs Bolt... 🙄

# Getting started

TODO


# Attributions

Usain icons made by [Pixel perfect](https://www.flaticon.com/authors/pixel-perfect) from [www.flaticon.com](http://www.flaticon.com/)
