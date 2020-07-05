namespace Usain.EventListener.Infrastructure.Hosting.Endpoints.Results.Responses
{
    public class UrlVerificationResponse
    {
        public string Challenge { get; }

        public UrlVerificationResponse(
            string challenge)
        {
            Challenge = challenge;
        }
    }
}
