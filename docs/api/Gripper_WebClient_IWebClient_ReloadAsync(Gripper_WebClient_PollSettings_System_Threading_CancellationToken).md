#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient')
## IWebClient.ReloadAsync(PollSettings, CancellationToken) Method
Reloads the browser window and awaits the resulting [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') of [WaitUntilFramesLoadedAsync(PollSettings, CancellationToken)](Gripper_WebClient_IWebClient_WaitUntilFramesLoadedAsync(Gripper_WebClient_PollSettings_System_Threading_CancellationToken) 'Gripper.WebClient.IWebClient.WaitUntilFramesLoadedAsync(Gripper.WebClient.PollSettings, System.Threading.CancellationToken)')  
using the provided [PollSettings](Gripper_WebClient_PollSettings 'Gripper.WebClient.PollSettings') and [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')
```csharp
System.Threading.Tasks.Task ReloadAsync(Gripper.WebClient.PollSettings pollSettings, System.Threading.CancellationToken cancellationToken);
```
#### Parameters
<a name='Gripper_WebClient_IWebClient_ReloadAsync(Gripper_WebClient_PollSettings_System_Threading_CancellationToken)_pollSettings'></a>
`pollSettings` [PollSettings](Gripper_WebClient_PollSettings 'Gripper.WebClient.PollSettings')  
Settings to pass as a parameter to the [WaitUntilFramesLoadedAsync(PollSettings, CancellationToken)](Gripper_WebClient_IWebClient_WaitUntilFramesLoadedAsync(Gripper_WebClient_PollSettings_System_Threading_CancellationToken) 'Gripper.WebClient.IWebClient.WaitUntilFramesLoadedAsync(Gripper.WebClient.PollSettings, System.Threading.CancellationToken)') call.
  
<a name='Gripper_WebClient_IWebClient_ReloadAsync(Gripper_WebClient_PollSettings_System_Threading_CancellationToken)_cancellationToken'></a>
`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')  
Token to cancel the [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task').
  
#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')  
A [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') that represents the reload.
