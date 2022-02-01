using Newtonsoft.Json;

namespace Gripper.WebClient.Models
{
    /// <summary>
    /// Details of exception.
    /// </summary>
    public class ExceptionDetails
    {
        /// <summary>
        /// Gets or sets Exception id.
        /// </summary>
        public long ExceptionId { get; set; }
        /// <summary>
        /// Gets or sets Exception text, which should be used together with exception object when available.
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// Gets or sets Line number of the exception location (0-based).
        /// </summary>
        public long LineNumber { get; set; }
        /// <summary>
        /// Gets or sets Column number of the exception location (0-based).
        /// </summary>
        public long ColumnNumber { get; set; }
        /// <summary>
        /// Gets or sets Script ID of the exception location.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? ScriptId { get; set; }
        /// <summary>
        /// Gets or sets URL of the exception location, to be used when the script was not reported.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Url { get; set; }
        /// <summary>
        /// Gets or sets JavaScript stack trace if available.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public StackTrace? StackTrace { get; set; }
        /// <summary>
        /// Gets or sets Exception object if available.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RemoteObject? Exception { get; set; }
        /// <summary>
        /// Gets or sets Identifier of the context where exception happened.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? ExecutionContextId { get; set; }
    }
}
