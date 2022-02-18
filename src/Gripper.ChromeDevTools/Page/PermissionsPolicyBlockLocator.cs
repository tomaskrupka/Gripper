namespace Gripper.ChromeDevTools.Page
{
    using Newtonsoft.Json;

    /// <summary>
    /// PermissionsPolicyBlockLocator
    /// </summary>
    public sealed class PermissionsPolicyBlockLocator
    {
        /// <summary>
        /// frameId
        ///</summary>
        [JsonProperty("frameId")]
        public string FrameId
        {
            get;
            set;
        }
        /// <summary>
        /// blockReason
        ///</summary>
        [JsonProperty("blockReason")]
        public PermissionsPolicyBlockReason BlockReason
        {
            get;
            set;
        }
    }
}