﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Gripper.WebClient.Runtime
{ 
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class ChildProcessTracker : IChildProcessTracker
    {
        // Windows will automatically close any open job handles when our process terminates.
        //  This can be verified by using SysInternals' Handle utility. When the job handle
        //  is closed, the child processes will be killed.
        private readonly IntPtr _jobHandle;

        /// <summary>
        /// Add the process to be tracked. If our current process is killed, the child processes
        /// that we are tracking will be automatically killed, too. If the child process terminates
        /// first, that's fine, too.</summary>
        /// <param name="process"></param>
        public void AddProcess(Process process)
        {
            if (_jobHandle != IntPtr.Zero)
            {
                bool success = AssignProcessToJobObject(_jobHandle, process.Handle);
                if (!success && !process.HasExited)
                    throw new Win32Exception();
            }
        }

        public ChildProcessTracker()
        {
            // This feature requires Windows 8 or later. To support Windows 7 requires
            //  registry settings to be added if you are using Visual Studio plus an
            //  app.manifest change.
            //  https://stackoverflow.com/a/4232259/386091
            //  https://stackoverflow.com/a/9507862/386091
            if (Environment.OSVersion.Version < new Version(6, 2))
                return;

            // The job name is optional (and can be null) but it helps with diagnostics.
            //  If it's not null, it has to be unique. Use SysInternals' Handle command-line
            //  utility: handle -a ChildProcessTracker
            string jobName = "ChildProcessTracker" + Process.GetCurrentProcess().Id;
            _jobHandle = CreateJobObject(IntPtr.Zero, jobName);

            var info = new JOBOBJECT_BASIC_LIMIT_INFORMATION();

            // This is the key flag. When our process is killed, Windows will automatically
            //  close the job handle, and when that happens, we want the child processes to
            //  be killed, too.
            info.LimitFlags = JOBOBJECTLIMIT.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE;

            var extendedInfo = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION();
            extendedInfo.BasicLimitInformation = info;

            int length = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
            IntPtr extendedInfoPtr = Marshal.AllocHGlobal(length);
            try
            {
                Marshal.StructureToPtr(extendedInfo, extendedInfoPtr, false);

                if (!SetInformationJobObject(_jobHandle, JobObjectInfoType.ExtendedLimitInformation,
                    extendedInfoPtr, (uint)length))
                {
                    throw new Win32Exception();
                }
            }
            finally
            {
                Marshal.FreeHGlobal(extendedInfoPtr);
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr CreateJobObject(IntPtr lpJobAttributes, string name);

        [DllImport("kernel32.dll")]
        static extern bool SetInformationJobObject(IntPtr job, JobObjectInfoType infoType,
            IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AssignProcessToJobObject(IntPtr job, IntPtr process);

    }
}
