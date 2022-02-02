namespace Gripper.WebClient.Events
{
    public class Runtime_ExecutionContextDestroyedEventArgs : RdpEventArgs
    {
        public long ContextId { get; set; }
        public Runtime_ExecutionContextDestroyedEventArgs(long contextId) : base("Runtime", "executionContextDestroyed")
        {
            ContextId = contextId;
        }
    }
}
