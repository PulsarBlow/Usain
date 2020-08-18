namespace Usain.Slack.Models.Blocks.Elements
{
    using System.Text.Json.Serialization;

    public abstract class ActionElement : Element
    {
        internal const string ActionIdJsonName = "action_id";

        /// <summary>
        /// An identifier for the action triggered when a menu option is selected.
        /// You can use this when you receive an interaction payload to identify the source of the action.
        /// Should be unique among all other action_ids used elsewhere by your app.
        /// </summary>
        /// <remarks>Maximum length for this field is 255 characters.</remarks>
        [JsonPropertyName(ActionIdJsonName)]
        public string? ActionId { get; set; }
    }
}
