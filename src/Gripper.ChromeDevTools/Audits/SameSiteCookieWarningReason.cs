namespace Gripper.ChromeDevTools.Audits
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime.Serialization;

    /// <summary>
    /// SameSiteCookieWarningReason
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SameSiteCookieWarningReason
    {
        [EnumMember(Value = "WarnSameSiteUnspecifiedCrossSiteContext")]
        WarnSameSiteUnspecifiedCrossSiteContext,
        [EnumMember(Value = "WarnSameSiteNoneInsecure")]
        WarnSameSiteNoneInsecure,
        [EnumMember(Value = "WarnSameSiteUnspecifiedLaxAllowUnsafe")]
        WarnSameSiteUnspecifiedLaxAllowUnsafe,
        [EnumMember(Value = "WarnSameSiteStrictLaxDowngradeStrict")]
        WarnSameSiteStrictLaxDowngradeStrict,
        [EnumMember(Value = "WarnSameSiteStrictCrossDowngradeStrict")]
        WarnSameSiteStrictCrossDowngradeStrict,
        [EnumMember(Value = "WarnSameSiteStrictCrossDowngradeLax")]
        WarnSameSiteStrictCrossDowngradeLax,
        [EnumMember(Value = "WarnSameSiteLaxCrossDowngradeStrict")]
        WarnSameSiteLaxCrossDowngradeStrict,
        [EnumMember(Value = "WarnSameSiteLaxCrossDowngradeLax")]
        WarnSameSiteLaxCrossDowngradeLax,
    }
}