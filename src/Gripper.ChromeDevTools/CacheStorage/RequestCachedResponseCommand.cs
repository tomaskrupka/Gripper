namespace Gripper.ChromeDevTools.CacheStorage
{
    using Newtonsoft.Json;

    /// <summary>
    /// Fetches cache entry.
    /// </summary>
    public sealed class RequestCachedResponseCommand : ICommand
    {
        private const string ChromeRemoteInterface_CommandName = "CacheStorage.requestCachedResponse";
        
        [JsonIgnore]
        public string CommandName
        {
            get { return ChromeRemoteInterface_CommandName; }
        }

        /// <summary>
        /// Id of cache that contains the entry.
        /// </summary>
        [JsonProperty("cacheId")]
        public string CacheId
        {
            get;
            set;
        }
        /// <summary>
        /// URL spec of the request.
        /// </summary>
        [JsonProperty("requestURL")]
        public string RequestURL
        {
            get;
            set;
        }
        /// <summary>
        /// headers of the request.
        /// </summary>
        [JsonProperty("requestHeaders")]
        public Header[] RequestHeaders
        {
            get;
            set;
        }
    }

    public sealed class RequestCachedResponseCommandResponse : ICommandResponse<RequestCachedResponseCommand>
    {
        /// <summary>
        /// Response read from the cache.
        ///</summary>
        [JsonProperty("response")]
        public CachedResponse Response
        {
            get;
            set;
        }
    }
}