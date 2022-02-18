namespace Gripper.ChromeDevTools.DOMSnapshot
{
    using Newtonsoft.Json;

    /// <summary>
    /// RareBooleanData
    /// </summary>
    public sealed class RareBooleanData
    {
        /// <summary>
        /// index
        ///</summary>
        [JsonProperty("index")]
        public long[] Index
        {
            get;
            set;
        }
    }
}