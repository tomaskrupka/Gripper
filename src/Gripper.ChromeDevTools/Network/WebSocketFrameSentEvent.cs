namespace Gripper.ChromeDevTools.Network
{
    using Newtonsoft.Json;

    /// <summary>
    /// Fired when WebSocket message is sent.
    /// </summary>
    public sealed class WebSocketFrameSentEvent : IEvent
    {
        /// <summary>
        /// Request identifier.
        /// </summary>
        [JsonProperty("requestId")]
        public string RequestId
        {
            get;
            set;
        }
        /// <summary>
        /// Timestamp.
        /// </summary>
        [JsonProperty("timestamp")]
        public double Timestamp
        {
            get;
            set;
        }
        /// <summary>
        /// WebSocket response data.
        /// </summary>
        [JsonProperty("response")]
        public WebSocketFrame Response
        {
            get;
            set;
        }
    }
}