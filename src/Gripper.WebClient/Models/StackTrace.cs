using Newtonsoft.Json;

namespace Gripper.WebClient.Models
{
    /// <summary>
    /// Call frames for assertions or error messages.
    /// </summary>
    public class StackTrace
    {
        /// <summary>
        /// Gets or sets string? label of this stack trace. For async traces this may be a name of the function that initiated the async call.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Description { get; set; }
        /// <summary>
        /// Gets or sets JavaScript function name.
        /// </summary>
        public CallFrame[]? CallFrames { get; set; }
        /// <summary>
        /// Gets or sets Asynchronous JavaScript stack trace that preceded this stack, if available.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public StackTrace? Parent { get; set; }
        /// <summary>
        /// Gets or sets Creation frame of the Promise which produced the next synchronous trace when resolved, if available.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CallFrame? PromiseCreationFrame { get; set; }
    }
}
