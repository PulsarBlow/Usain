namespace Usain.Slack.Models.Events
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Verifies ownership of an Events API Request URL
    /// This event does not require a specific OAuth scope or subscription.
    /// You'll automatically receive it whenever configuring an Events API Request URL.
    /// Once you receive the event, verify the request's authenticity and then
    /// respond in plaintext with the challenge attribute value
    /// </summary>
    public class UrlVerificationEvent : Event
    {
        internal const string ChallengeJsonName = "challenge";
        internal const string TokenJsonName = "token";

        /// <summary>
        /// Event type value for the <see cref="UrlVerificationEvent"/> event.
        /// </summary>
        public const string EventTypeValue = "url_verification";

        /// <summary>
        /// This deprecated verification token is proof that the request
        /// is coming from Slack on behalf of your application.
        /// </summary>
        /// <example>Jhj5dZrVaK7ZwHHjRyZWjbDl</example>
        [JsonPropertyName(TokenJsonName)]
        public string? Token { get; set; }

        /// <summary>
        /// A randomly generated string produced by Slack.
        /// </summary>
        [JsonPropertyName(ChallengeJsonName)]
        public string? Challenge { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlVerificationEvent"/> class.
        /// </summary>
        public UrlVerificationEvent()
            => EventType = EventTypeValue;
    }
}
