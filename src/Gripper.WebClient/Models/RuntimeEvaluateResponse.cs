using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using Newtonsoft.Json;

namespace Gripper.WebClient.Models
{
    /// <summary>
    /// Evaluates expression on global object.
    /// </summary>
    public class RuntimeEvaluateResponse
    {
        /// <summary>
        /// Gets or sets Evaluation result.
        /// </summary>
        public RemoteObject? Result { get; set; }

        /// <summary>
        /// Gets or sets Exception details.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ExceptionDetails? ExceptionDetails { get; set; }
    }
    public static class RuntimeEvaluateResponseExtensions
    {
        public static RuntimeEvaluateResponse ToRuntimeEvaluateResponse(this EvaluateCommandResponse rawResponse)
        {
            var serialized = JsonConvert.SerializeObject(rawResponse, Formatting.None);
            return JsonConvert.DeserializeObject<RuntimeEvaluateResponse>(serialized);
        }
    }
}
