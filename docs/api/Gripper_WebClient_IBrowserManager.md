### [Gripper.WebClient](Gripper_WebClient.md 'Gripper.WebClient')
## IBrowserManager Interface
Provides methods and members to launch, manage, access and destroy a web browser instance.  
At mininum, implementations must configure connecting to the CDP endpoint of the browser and pre-startup and post-destroy cleanup.  
```csharp
public interface IBrowserManager :
System.IDisposable
```

Implements [System.IDisposable](https://docs.microsoft.com/en-us/dotnet/api/System.IDisposable 'System.IDisposable')  

| Properties | |
| :--- | :--- |
| [BrowserProcess](Gripper_WebClient_IBrowserManager_BrowserProcess.md 'Gripper.WebClient.IBrowserManager.BrowserProcess') | The handle of the browser OS process.<br/> |
| [DebuggerUrl](Gripper_WebClient_IBrowserManager_DebuggerUrl.md 'Gripper.WebClient.IBrowserManager.DebuggerUrl') | The URL of the WebSocket listener of the browser's CDP server.<br/> |

| Methods | |
| :--- | :--- |
| [LaunchAsync(CancellationToken)](Gripper_WebClient_IBrowserManager_LaunchAsync(System_Threading_CancellationToken).md 'Gripper.WebClient.IBrowserManager.LaunchAsync(System.Threading.CancellationToken)') | Launches a browser instance and sets the [DebuggerUrl](Gripper_WebClient_IBrowserManager_DebuggerUrl.md 'Gripper.WebClient.IBrowserManager.DebuggerUrl') and [BrowserProcess](Gripper_WebClient_IBrowserManager_BrowserProcess.md 'Gripper.WebClient.IBrowserManager.BrowserProcess') members.<br/> |
