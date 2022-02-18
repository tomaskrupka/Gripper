namespace Gripper.ChromeDevTools.Runtime
{
    using Newtonsoft.Json;

    /// <summary>
    /// Issued when all executionContexts were cleared in browser
    /// </summary>
    public sealed class ExecutionContextsClearedEvent : IEvent
    {
    }
}