namespace Gripper.ChromeDevTools.Page
{
    using Newtonsoft.Json;

    /// <summary>
    /// BackForwardCacheNotRestoredExplanation
    /// </summary>
    public sealed class BackForwardCacheNotRestoredExplanation
    {
        /// <summary>
        /// Type of the reason
        ///</summary>
        [JsonProperty("type")]
        public BackForwardCacheNotRestoredReasonType Type
        {
            get;
            set;
        }
        /// <summary>
        /// Not restored reason
        ///</summary>
        [JsonProperty("reason")]
        public BackForwardCacheNotRestoredReason Reason
        {
            get;
            set;
        }
    }
}