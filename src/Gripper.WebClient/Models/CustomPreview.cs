using Newtonsoft.Json;

namespace Gripper.WebClient.Models
{
    /// <summary>
    /// Custom preview.
    /// </summary>
    public class CustomPreview
    {
        /// <summary>
        /// Gets or sets Header
        /// </summary>
        public string? Header { get; set; }
        /// <summary>
        /// Gets or sets HasBody
        /// </summary>
        public bool HasBody { get; set; }
        /// <summary>
        /// Gets or sets FormatterObjectId
        /// </summary>
        public string? FormatterObjectId { get; set; }
        /// <summary>
        /// Gets or sets BindRemoteObjectFunctionId
        /// </summary>
        public string? BindRemoteObjectFunctionId { get; set; }
        /// <summary>
        /// Gets or sets ConfigObjectId
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? ConfigObjectId { get; set; }
    }
}
