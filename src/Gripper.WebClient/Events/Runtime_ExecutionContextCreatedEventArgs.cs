using BaristaLabs.ChromeDevTools.Runtime.Runtime;

namespace Gripper.WebClient.Events
{
    public class Runtime_ExecutionContextCreatedEventArgs : RdpEventArgs
    {
        public long ContextId { get; set; }
        public ExecutionContextDescription Description { get; set; }
        public Runtime_ExecutionContextCreatedEventArgs(ExecutionContextDescription description) : base("Runtime", "executionContextCreated")
        {
            ContextId = description.Id;
            Description = description;
        }
    }
}
