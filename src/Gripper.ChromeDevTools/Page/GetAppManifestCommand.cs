namespace Gripper.ChromeDevTools.Page
{
    using Newtonsoft.Json;

    /// <summary>
    /// GetAppManifest
    /// </summary>
    public sealed class GetAppManifestCommand : ICommand
    {
        private const string ChromeRemoteInterface_CommandName = "Page.getAppManifest";
        
        [JsonIgnore]
        public string CommandName
        {
            get { return ChromeRemoteInterface_CommandName; }
        }

    }

    public sealed class GetAppManifestCommandResponse : ICommandResponse<GetAppManifestCommand>
    {
        /// <summary>
        /// Manifest location.
        ///</summary>
        [JsonProperty("url")]
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the errors
        /// </summary>
        [JsonProperty("errors")]
        public AppManifestError[] Errors
        {
            get;
            set;
        }
        /// <summary>
        /// Manifest content.
        ///</summary>
        [JsonProperty("data", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Data
        {
            get;
            set;
        }
        /// <summary>
        /// Parsed manifest properties
        ///</summary>
        [JsonProperty("parsed", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public AppManifestParsedProperties Parsed
        {
            get;
            set;
        }
    }
}