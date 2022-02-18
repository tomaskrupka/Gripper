namespace Gripper.ChromeDevTools.Audits
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime.Serialization;

    /// <summary>
    /// SameSiteCookieOperation
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SameSiteCookieOperation
    {
        [EnumMember(Value = "SetCookie")]
        SetCookie,
        [EnumMember(Value = "ReadCookie")]
        ReadCookie,
    }
}