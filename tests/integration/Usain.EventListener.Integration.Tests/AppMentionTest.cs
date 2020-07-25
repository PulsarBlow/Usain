namespace Usain.EventListener.Integration.Tests
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Helpers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Hosting;
    using Slack.Models.CallbackEvents;
    using Xunit;

    public class AppMentionTest
    {
        [Fact]
        public async Task Post_Event_Returns_Status200OK()
        {
            var hostBuilder = HostBuilderFactory.CreateHostBuilder();

            // Build and start the IHost
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient to send requests to the TestServer
            var client = host.GetTestClient();

            var content = new StringContent(
                FakeBuilder.CreateCallbackEvent<AppMentionEvent>(
                    AppMentionEvent.EventType));

            var response = await client.PostAsync(
                "/events",
                content,
                CancellationToken.None);

            response.EnsureSuccessStatusCode();

            Assert.Equal(
                StatusCodes.Status200OK,
                (int) response.StatusCode);
        }
    }
}
