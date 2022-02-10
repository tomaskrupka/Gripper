using System;

namespace Gripper.WebClient.Runtime
{
    [Flags]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum JOBOBJECTLIMIT : uint
    {
        JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 0x2000
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
