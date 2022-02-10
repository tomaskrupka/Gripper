using System.Diagnostics;

namespace Gripper.WebClient.Runtime
{

    /// <summary>
    /// Automatically kills all registered processes if the parent unexpectedly exits.
    /// This feature requires Windows 8 or greater. On Windows 7, nothing is done.</summary>
    /// <remarks>References:
    ///  <see href="https://stackoverflow.com/a/4657392/386091"/>, 
    ///  <see href="https://stackoverflow.com/a/9164742/386091"/> </remarks>
    public interface IChildProcessTracker
    {
        /// <summary>
        /// Add the process to be tracked and killed if the parent unexpectedly terminates.
        /// </summary>
        /// <param name="process">Process to be tracked.</param>
        void AddProcess(Process process);
    }
}