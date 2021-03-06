namespace Gripper.ChromeDevTools.HeapProfiler
{
    using Newtonsoft.Json;

    /// <summary>
    /// Sampling profile.
    /// </summary>
    public sealed class SamplingHeapProfile
    {
        /// <summary>
        /// head
        ///</summary>
        [JsonProperty("head")]
        public SamplingHeapProfileNode Head
        {
            get;
            set;
        }
        /// <summary>
        /// samples
        ///</summary>
        [JsonProperty("samples")]
        public SamplingHeapProfileSample[] Samples
        {
            get;
            set;
        }
    }
}