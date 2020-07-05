namespace Usain.EventListener.Tests.Infrastructure.Security
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using EventListener.Extensions;
    using EventListener.Infrastructure.Security;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Primitives;
    using Moq;
    using Slack.Security;
    using Xunit;

    public class RequestAuthenticatorTest
    {
        private readonly Mock<ISignatureVerifier> _signatureVerifierMock =
            new Mock<ISignatureVerifier>();
        private readonly Mock<ILogger<RequestAuthenticator>>
            _loggerMock = new Mock<ILogger<RequestAuthenticator>>();
        private readonly Mock<IOptionsMonitor<EventListenerOptions>>
            _optionsMonitorMock =
                new Mock<IOptionsMonitor<EventListenerOptions>>();
        private readonly EventListenerOptions _options =
            new EventListenerOptions();
        private readonly Mock<Stream> _requestBodyMock = new Mock<Stream>();
        private readonly Mock<HttpRequest> _requestMock =
            new Mock<HttpRequest>();

        public RequestAuthenticatorTest()
        {
            _optionsMonitorMock
                .SetupGet(x => x.CurrentValue)
                .Returns(_options);
            _requestBodyMock
                .SetupGet(x => x.CanSeek)
                .Returns(true);
            _requestMock
                .SetupGet(x => x.Headers)
                .Returns(
                    new HeaderDictionary(
                        new Dictionary<string, StringValues>
                        {
                            [HttpRequestExtensions.TimestampHeaderName] =
                                new StringValues("0"),
                            [HttpRequestExtensions.SignatureHeaderName] = "abc",
                        }));
            _requestMock.SetupGet(x => x.Body)
                .Returns(new MemoryStream(Encoding.UTF8.GetBytes("content")));
        }

        [Fact]
        public async Task
            IsAuthenticAsync_Returns_True_When_RequestAuthentication_Is_Disabled()
        {
            _options.IsRequestAuthenticationEnabled = false;

            var authenticator = CreateAuthenticator();

            Assert.True(
                await authenticator.IsAuthenticAsync(
                    _requestMock.Object,
                    CancellationToken.None));
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(
            false,
            false)]
        public async Task IsAuthentic_Returns_Expected_Value(
            bool verificationResult,
            bool expected)
        {
            _signatureVerifierMock
                .Setup(
                    x => x.Verify(
                        It.IsAny<string>(),
                        It.IsAny<long>(),
                        It.IsAny<string>()))
                .Returns(verificationResult);
            var authenticator = CreateAuthenticator();

            var actual = await authenticator.IsAuthenticAsync(
                _requestMock.Object,
                CancellationToken.None);

            Assert.Equal(expected, actual);
        }

        private RequestAuthenticator CreateAuthenticator()
        {
            return new RequestAuthenticator(
                _loggerMock.Object,
                _signatureVerifierMock.Object,
                _optionsMonitorMock.Object);
        }
    }
}
