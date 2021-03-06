namespace Gripper.ChromeDevTools.WebAudio
{
    using Newtonsoft.Json;

    /// <summary>
    /// Notifies that existing BaseAudioContext has changed some properties (id stays the same)..
    /// </summary>
    public sealed class ContextChangedEvent : IEvent
    {
        /// <summary>
        /// Gets or sets the context
        /// </summary>
        [JsonProperty("context")]
        public BaseAudioContext Context
        {
            get;
            set;
        }
    }
}