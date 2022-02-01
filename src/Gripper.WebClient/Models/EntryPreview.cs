using Newtonsoft.Json;

namespace Gripper.WebClient.Models
{
    /// <summary>
    /// Entry preview object
    /// </summary>
    public class EntryPreview
    {
        /// <summary>
        /// Gets or sets Preview of the key. Specified for map-like collection entries.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ObjectPreview? Key { get; set; }
        /// <summary>
        /// Gets or sets Preview of the value.
        /// </summary>
        public ObjectPreview? Value { get; set; }
    }
}
