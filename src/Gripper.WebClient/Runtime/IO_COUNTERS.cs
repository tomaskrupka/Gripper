using System.Runtime.InteropServices;

namespace Gripper.WebClient.Runtime
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [StructLayout(LayoutKind.Sequential)]
    public struct IO_COUNTERS
    {
        public ulong ReadOperationCount;
        public ulong WriteOperationCount;
        public ulong OtherOperationCount;
        public ulong ReadTransferCount;
        public ulong WriteTransferCount;
        public ulong OtherTransferCount;
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
