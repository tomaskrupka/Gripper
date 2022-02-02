using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Gripper.WebClient.Models
{
    /// <summary>
    /// Primitive value which cannot be JSON-string?ified.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UnserializableValue
    {
        Infinity,
        NaN,
        [EnumMember(Value = "-Infinity")]
        _Infinity,
        [EnumMember(Value = "-0")]
        _0,
    }
}
