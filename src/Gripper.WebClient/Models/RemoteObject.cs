using Newtonsoft.Json;

namespace Gripper.WebClient.Models
{
    /// <summary>
    /// Mirror object referencing original JavaScript object.
    /// </summary>
    public class RemoteObject
    {
        /// <summary>
        /// Gets or sets Object type.
        /// </summary>
        public RemoteObjectType Type { get; set; }
        /// <summary>
        /// Gets or sets Object subtype hint. Specified for <code>object</code> type values only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RemoteObjectSubtype? Subtype { get; set; }
        /// <summary>
        /// Gets or sets Object class (constructor) name. Specified for <code>object</code> type values only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? ClassName { get; set; }
        /// <summary>
        /// Gets or sets Remote object value in case of primitive values or JSON values (if it was requested).
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object? Value { get; set; }
        /// <summary>
        /// Gets or sets Primitive value which can not be JSON-string?ified does not have <code>value</code>, but gets this property.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UnserializableValue? UnserializableValue { get; set; }
        /// <summary>
        /// Gets or sets string? representation of the object.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Description { get; set; }
        /// <summary>
        /// Gets or sets Unique object identifier (for non-primitive values).
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? ObjectId { get; set; }
        /// <summary>
        /// Gets or sets Preview containing abbreviated property values. Specified for <code>object</code> type values only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ObjectPreview? Preview { get; set; }
        /// <summary>
        /// Gets or sets CustomPreview
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CustomPreview? CustomPreview { get; set; }
    }
}
