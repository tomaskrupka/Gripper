### [Gripper.WebClient](Gripper_WebClient.md 'Gripper.WebClient').[IWebClient](Gripper_WebClient_IWebClient.md 'Gripper.WebClient.IWebClient')
## IWebClient.WaitUntilFramesLoadedAsync(PollSettings, CancellationToken) Method
Blocks until either<br/>  
1. All of the following has happened:<br/>  
1.1. No frame has been added to the frame tree for one [PeriodMs](Gripper_WebClient_PollSettings_PeriodMs.md 'Gripper.WebClient.PollSettings.PeriodMs') period,<br/>  
1.2. All frames in the frame tree have received the Page.frameNavigated and Page.frameLoaded events,<br/>  
1.3. One [PeriodMs](Gripper_WebClient_PollSettings_PeriodMs.md 'Gripper.WebClient.PollSettings.PeriodMs') period has elapsed since the last Page.frameNavigated or Page.frameLoaded event,<br/>  
or<br/>  
2. [TimeoutMs](Gripper_WebClient_PollSettings_TimeoutMs.md 'Gripper.WebClient.PollSettings.TimeoutMs') has elapsed.<br/>  
or<br/>  
3. Task has been cancelled.<br/>
```csharp
System.Threading.Tasks.Task WaitUntilFramesLoadedAsync(Gripper.WebClient.PollSettings pollSettings, System.Threading.CancellationToken cancellationToken);
```
#### Parameters
<a name='Gripper_WebClient_IWebClient_WaitUntilFramesLoadedAsync(Gripper_WebClient_PollSettings_System_Threading_CancellationToken)_pollSettings'></a>
`pollSettings` [PollSettings](Gripper_WebClient_PollSettings.md 'Gripper.WebClient.PollSettings')  
Settings to control the polling for changes to the frame tree.
  
<a name='Gripper_WebClient_IWebClient_WaitUntilFramesLoadedAsync(Gripper_WebClient_PollSettings_System_Threading_CancellationToken)_cancellationToken'></a>
`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')  
Token to cancel the [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task').
  
#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')  
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') that represents the block.
