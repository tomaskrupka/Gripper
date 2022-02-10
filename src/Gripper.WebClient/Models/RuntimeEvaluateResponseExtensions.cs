using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using Newtonsoft.Json;

namespace Gripper.WebClient.Models
{
    /// <summary>
    /// Utils for easier consumption of Runtime.Evaluate() calls responses.
    /// </summary>
    internal static class RuntimeEvaluateResponseExtensions
    {
        /// <summary>
        ///  Cast to custom type without Reflection. The objects are usually tiny.
        /// </summary>
        /// <param name="rawResponse"></param>
        /// <returns></returns>
        internal static RuntimeEvaluateResponse ToRuntimeEvaluateResponse(this EvaluateCommandResponse rawResponse)
        {
            var serialized = JsonConvert.SerializeObject(rawResponse, Formatting.None);
            return JsonConvert.DeserializeObject<RuntimeEvaluateResponse>(serialized);
        }
    }
}
