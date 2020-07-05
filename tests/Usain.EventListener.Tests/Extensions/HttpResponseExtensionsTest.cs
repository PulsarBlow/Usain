namespace Usain.EventListener.Tests.Extensions
{
    using System.Collections.Generic;
    using EventListener.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Microsoft.Net.Http.Headers;
    using Moq;
    using Xunit;

    public class HttpResponseExtensionsTest
    {
        [Fact]
        public void SetNoCache_Adds_CacheControl_Headers()
        {
            var headers = new HeaderDictionary();
            var response = Mock.Of<HttpResponse>(x => x.Headers == headers);

            response.SetNoCache();

            Assert.Collection(
                headers,
                item =>
                {
                    var (key, value) = item;
                    Assert.Equal(
                        HeaderNames.CacheControl,
                        key);
                    Assert.Equal(
                        HttpResponseExtensions.CacheControlValue,
                        value);
                },
                item =>
                {
                    var (key, value) = item;
                    Assert.Equal(
                        HeaderNames.Pragma,
                        key);
                    Assert.Equal(
                        HttpResponseExtensions.PragmaValue,
                        value);
                });
        }

        [Fact]
        public void SetNoCache_Updates_Existing_CacheValue()
        {
            var headers = new HeaderDictionary(
                new Dictionary<string, StringValues>
                {
                    [HeaderNames.CacheControl] = "test",
                });
            var response = Mock.Of<HttpResponse>(x => x.Headers == headers);

            response.SetNoCache();

            Assert.Equal(
                HttpResponseExtensions.CacheControlValue,
                response.Headers[HeaderNames.CacheControl]);
        }
    }
}
