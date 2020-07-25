namespace Usain.EventListener.Integration.Tests.Helpers
{
    using System.Dynamic;
    using System.IO;
    using System.Text;
    using System.Text.Json;

    internal static class FakeBuilder
    {
        public static string CreateEvent(
            string eventType)
        {
            var eventJson =
                File.ReadAllText($"Fakes/{eventType}.json");
            return eventJson;
        }

        public static string CreateCallbackEvent<TEvent>(
            string eventType)
        {
            // Get callback event template
            var callbackEventJson =
                File.ReadAllText($"Fakes/CallbackEvents/{eventType}.json");
            return BuildCallbackEvent<TEvent>(callbackEventJson);
        }

        private static string BuildCallbackEvent<TEvent>(string callbackEventContent)
        {
            // Get the event wrapper template
            var eventWrapperJson = File.ReadAllText("Fakes/event_wrapper.json", Encoding.UTF8);

            // Add an event property dynamically
            dynamic expando = JsonSerializer.Deserialize<ExpandoObject>(eventWrapperJson);
            expando.@event =
                JsonSerializer.Deserialize<TEvent>(callbackEventContent);

            return JsonSerializer.Serialize<ExpandoObject>(expando);
        }
    }
}
