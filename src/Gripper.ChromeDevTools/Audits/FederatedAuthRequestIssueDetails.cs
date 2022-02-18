namespace Gripper.ChromeDevTools.Audits
{
    using Newtonsoft.Json;

    /// <summary>
    /// FederatedAuthRequestIssueDetails
    /// </summary>
    public sealed class FederatedAuthRequestIssueDetails
    {
        /// <summary>
        /// federatedAuthRequestIssueReason
        ///</summary>
        [JsonProperty("federatedAuthRequestIssueReason")]
        public FederatedAuthRequestIssueReason FederatedAuthRequestIssueReason
        {
            get;
            set;
        }
    }
}