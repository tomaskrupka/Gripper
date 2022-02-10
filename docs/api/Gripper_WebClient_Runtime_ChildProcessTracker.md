#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient.Runtime](Gripper_WebClient_Runtime 'Gripper.WebClient.Runtime')
## ChildProcessTracker Class
Automatically kills all registered processes if the parent unexpectedly exits.  
This feature requires Windows 8 or greater. On Windows 7, nothing is done.
```csharp
public class ChildProcessTracker :
Gripper.WebClient.Runtime.IChildProcessTracker
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ChildProcessTracker  

Implements [IChildProcessTracker](Gripper_WebClient_Runtime_IChildProcessTracker 'Gripper.WebClient.Runtime.IChildProcessTracker')  
### Remarks
References:  
             [https://stackoverflow.com/a/4657392/386091](https://stackoverflow.com/a/4657392/386091 'https://stackoverflow.com/a/4657392/386091'),   
             [https://stackoverflow.com/a/9164742/386091](https://stackoverflow.com/a/9164742/386091 'https://stackoverflow.com/a/9164742/386091')

| Methods | |
| :--- | :--- |
| [AddProcess(Process)](Gripper_WebClient_Runtime_ChildProcessTracker_AddProcess(System_Diagnostics_Process) 'Gripper.WebClient.Runtime.ChildProcessTracker.AddProcess(System.Diagnostics.Process)') | Add the process to be tracked. If our current process is killed, the child processes<br/>that we are tracking will be automatically killed, too. If the child process terminates<br/>first, that's fine, too. |
