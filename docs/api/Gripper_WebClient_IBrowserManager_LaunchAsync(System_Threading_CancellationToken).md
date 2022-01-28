### [Gripper.WebClient](Gripper_WebClient.md 'Gripper.WebClient').[IBrowserManager](Gripper_WebClient_IBrowserManager.md 'Gripper.WebClient.IBrowserManager')
## IBrowserManager.LaunchAsync(CancellationToken) Method
Launches a browser instance and sets the [DebuggerUrl](Gripper_WebClient_IBrowserManager_DebuggerUrl.md 'Gripper.WebClient.IBrowserManager.DebuggerUrl') and [BrowserProcess](Gripper_WebClient_IBrowserManager_BrowserProcess.md 'Gripper.WebClient.IBrowserManager.BrowserProcess') members.  
```csharp
System.Threading.Tasks.Task LaunchAsync(System.Threading.CancellationToken cancellationToken);
```
#### Parameters
<a name='Gripper_WebClient_IBrowserManager_LaunchAsync(System_Threading_CancellationToken)_cancellationToken'></a>
`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')  
  
#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')  
A [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')that completes when the [DebuggerUrl](Gripper_WebClient_IBrowserManager_DebuggerUrl.md 'Gripper.WebClient.IBrowserManager.DebuggerUrl') and the [BrowserProcess](Gripper_WebClient_IBrowserManager_BrowserProcess.md 'Gripper.WebClient.IBrowserManager.BrowserProcess') members have been initialized.
