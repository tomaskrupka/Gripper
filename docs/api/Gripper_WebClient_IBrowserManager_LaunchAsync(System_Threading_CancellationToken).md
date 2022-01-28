### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IBrowserManager](Gripper_WebClient_IBrowserManager 'Gripper.WebClient.IBrowserManager')
## IBrowserManager.LaunchAsync(CancellationToken) Method
Launches a browser instance and sets the [DebuggerUrl](Gripper_WebClient_IBrowserManager_DebuggerUrl 'Gripper.WebClient.IBrowserManager.DebuggerUrl') and [BrowserProcess](Gripper_WebClient_IBrowserManager_BrowserProcess 'Gripper.WebClient.IBrowserManager.BrowserProcess') members.  
```csharp
System.Threading.Tasks.Task LaunchAsync(System.Threading.CancellationToken cancellationToken);
```
#### Parameters
<a name='Gripper_WebClient_IBrowserManager_LaunchAsync(System_Threading_CancellationToken)_cancellationToken'></a>
`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')  
  
#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')  
A [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')that completes when the [DebuggerUrl](Gripper_WebClient_IBrowserManager_DebuggerUrl 'Gripper.WebClient.IBrowserManager.DebuggerUrl') and the [BrowserProcess](Gripper_WebClient_IBrowserManager_BrowserProcess 'Gripper.WebClient.IBrowserManager.BrowserProcess') members have been initialized.
