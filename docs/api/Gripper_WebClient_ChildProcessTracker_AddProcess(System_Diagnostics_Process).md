### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[ChildProcessTracker](Gripper_WebClient_ChildProcessTracker 'Gripper.WebClient.ChildProcessTracker')
## ChildProcessTracker.AddProcess(Process) Method
Add the process to be tracked. If our current process is killed, the child processes  
that we are tracking will be automatically killed, too. If the child process terminates  
first, that's fine, too.
```csharp
public static void AddProcess(System.Diagnostics.Process process);
```
#### Parameters
<a name='Gripper_WebClient_ChildProcessTracker_AddProcess(System_Diagnostics_Process)_process'></a>
`process` [System.Diagnostics.Process](https://docs.microsoft.com/en-us/dotnet/api/System.Diagnostics.Process 'System.Diagnostics.Process')  
  
