using System;
using System.Runtime.InteropServices;

namespace Gripper.WebClient.Runtime
{
    [StructLayout(LayoutKind.Sequential)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
    {
        public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
        public IO_COUNTERS IoInfo;
        public UIntPtr ProcessMemoryLimit;
        public UIntPtr JobMemoryLimit;
        public UIntPtr PeakProcessMemoryUsed;
        public UIntPtr PeakJobMemoryUsed;
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
