namespace Gripper.ChromeDevTools.HeapProfiler
{
    using Newtonsoft.Json;

    /// <summary>
    /// Sampling Heap Profile node. Holds callsite information, allocation statistics and child nodes.
    /// </summary>
    public sealed class SamplingHeapProfileNode
    {
        /// <summary>
        /// Function location.
        ///</summary>
        [JsonProperty("callFrame")]
        public Runtime.CallFrame CallFrame
        {
            get;
            set;
        }
        /// <summary>
        /// Allocations size in bytes for the node excluding children.
        ///</summary>
        [JsonProperty("selfSize")]
        public double SelfSize
        {
            get;
            set;
        }
        /// <summary>
        /// Node id. Ids are unique across all profiles collected between startSampling and stopSampling.
        ///</summary>
        [JsonProperty("id")]
        public long Id
        {
            get;
            set;
        }
        /// <summary>
        /// Child nodes.
        ///</summary>
        [JsonProperty("children")]
        public SamplingHeapProfileNode[] Children
        {
            get;
            set;
        }
    }
}