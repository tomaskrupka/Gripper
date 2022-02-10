using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Gripper.WebClient.Models
{
    /// <summary>
    /// Primitive value which cannot be JSON-stringified.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UnserializableValue
    {
        /// <summary>
        /// Value was unserializable because it was Infinity.
        /// </summary>
        Infinity,

        /// <summary>
        /// Value was unserializable because it was NaN.
        /// </summary>
        NaN,

        /// <summary>
        /// Value was unserializable because it was -Infinity.
        /// </summary>
        [EnumMember(Value = "-Infinity")]
        _Infinity,

        /// <summary>
        /// Value was unserializable because it was -0.
        /// </summary>
        [EnumMember(Value = "-0")]
        _0,
    }
}
