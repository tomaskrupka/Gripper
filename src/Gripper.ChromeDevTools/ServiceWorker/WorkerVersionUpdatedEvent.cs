namespace Gripper.ChromeDevTools.ServiceWorker
{
    using Newtonsoft.Json;

    /// <summary>
    /// WorkerVersionUpdated
    /// </summary>
    public sealed class WorkerVersionUpdatedEvent : IEvent
    {
        /// <summary>
        /// Gets or sets the versions
        /// </summary>
        [JsonProperty("versions")]
        public ServiceWorkerVersion[] Versions
        {
            get;
            set;
        }
    }
}