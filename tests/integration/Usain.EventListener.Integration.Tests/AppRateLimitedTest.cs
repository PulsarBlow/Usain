namespace Usain.EventListener.Integration.Tests
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Helpers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Hosting;
    using Slack.Models.Events;
    using Xunit;

    public class AppRateLimitedTest
    {
        [Fact]
        public async Task Post_Event_Returns_Status200OK()
        {
            var hostBuilder = HostBuilderFactory.CreateHostBuilder();
            var host = await hostBuilder.StartAsync();
            var client = host.GetTestClient();

            var content = new StringContent(
                FakeBuilder.CreateEvent(AppRateLimitedEvent.EventTypeValue));

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
