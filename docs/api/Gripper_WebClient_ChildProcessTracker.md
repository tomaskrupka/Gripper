### [Gripper.WebClient](Gripper_WebClient.md 'Gripper.WebClient')
## ChildProcessTracker Class
Allows processes to be automatically killed if this parent process unexpectedly quits.  
This feature requires Windows 8 or greater. On Windows 7, nothing is done.
```csharp
public static class ChildProcessTracker
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ChildProcessTracker  
### Remarks
References:  
             https://stackoverflow.com/a/4657392/386091  
             https://stackoverflow.com/a/9164742/386091 

| Methods | |
| :--- | :--- |
| [AddProcess(Process)](Gripper_WebClient_ChildProcessTracker_AddProcess(System_Diagnostics_Process).md 'Gripper.WebClient.ChildProcessTracker.AddProcess(System.Diagnostics.Process)') | Add the process to be tracked. If our current process is killed, the child processes<br/>that we are tracking will be automatically killed, too. If the child process terminates<br/>first, that's fine, too. |
