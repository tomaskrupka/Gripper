using System;
using System.Runtime.InteropServices;

namespace Gripper.WebClient.Runtime
{
    [StructLayout(LayoutKind.Sequential)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public struct JOBOBJECT_BASIC_LIMIT_INFORMATION
    {
        public long PerProcessUserTimeLimit;
        public long PerJobUserTimeLimit;
        public JOBOBJECTLIMIT LimitFlags;
        public UIntPtr MinimumWorkingSetSize;
        public UIntPtr MaximumWorkingSetSize;
        public uint ActiveProcessLimit;
        public long Affinity;
        public uint PriorityClass;
        public uint SchedulingClass;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
