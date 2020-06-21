namespace Usain.EventListener.Infrastructure.Hosting.Endpoints
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class Endpoint
    {
        public string Name { get; }
        public PathString Path { get; }
        public Type Handler { get; }

        public Endpoint(string name, string path, Type handlerType)
        {
            Name = name;
            Path = path;
            Handler = handlerType;
        }
    }
}
