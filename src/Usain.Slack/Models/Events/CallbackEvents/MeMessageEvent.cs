namespace Usain.Slack.Models.Events.CallbackEvents
{
    /// <summary>
    /// A me_message is sent when a channel member performs an action using the /me command.
    /// </summary>
    public class MeMessageEvent : MessageEvent
    {
        /// <summary>
        /// Message subtype value for the <see cref="MessageEvent"/> event.
        /// </summary>
        public const string MessageSubTypeValue = "me_message";

        /// <summary>
        /// Initializes a new instance of the <see cref="MeMessageEvent"/> class.
        /// </summary>
        public MeMessageEvent()
            => MessageSubType = MessageSubTypeValue;
    }
}
