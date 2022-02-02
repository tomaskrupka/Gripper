#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient')
## IWebClient Interface
Enables interaction with the hooked web browser window.  
```csharp
public interface IWebClient :
System.IDisposable
```

Implements [System.IDisposable](https://docs.microsoft.com/en-us/dotnet/api/System.IDisposable 'System.IDisposable')  

| Properties | |
| :--- | :--- |
| [MainContext](Gripper_WebClient_IWebClient_MainContext 'Gripper.WebClient.IWebClient.MainContext') | The execution context that corresponds to the root of the page iFrame tree DOM.<br/> |

| Methods | |
| :--- | :--- |
| [ExecuteCdpCommandAsync(string, JToken)](Gripper_WebClient_IWebClient_ExecuteCdpCommandAsync(string_Newtonsoft_Json_Linq_JToken) 'Gripper.WebClient.IWebClient.ExecuteCdpCommandAsync(string, Newtonsoft.Json.Linq.JToken)') | Tunnels a CDP command directly to the CDP client endpoint. <br/> |
| [GetAllCookiesAsync()](Gripper_WebClient_IWebClient_GetAllCookiesAsync() 'Gripper.WebClient.IWebClient.GetAllCookiesAsync()') | Gets all cookies stored by the browser.<br/> |
| [GetContextsAsync()](Gripper_WebClient_IWebClient_GetContextsAsync() 'Gripper.WebClient.IWebClient.GetContextsAsync()') | Gets an [System.Collections.Generic.IReadOnlyCollection&lt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1') of interactable contexts.<br/>There is no guarantee w.r.t. the lifespan of the resulting [IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext')s.<br/> |
| [GetCurrentUrlAsync()](Gripper_WebClient_IWebClient_GetCurrentUrlAsync() 'Gripper.WebClient.IWebClient.GetCurrentUrlAsync()') | Gets the current URL of the top page.<br/> |
| [NavigateAsync(string, PollSettings, CancellationToken)](Gripper_WebClient_IWebClient_NavigateAsync(string_Gripper_WebClient_PollSettings_System_Threading_CancellationToken) 'Gripper.WebClient.IWebClient.NavigateAsync(string, Gripper.WebClient.PollSettings, System.Threading.CancellationToken)') | Navigates to the address and awaits the resulting [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') of [WaitUntilFramesLoadedAsync(PollSettings, CancellationToken)](Gripper_WebClient_IWebClient_WaitUntilFramesLoadedAsync(Gripper_WebClient_PollSettings_System_Threading_CancellationToken) 'Gripper.WebClient.IWebClient.WaitUntilFramesLoadedAsync(Gripper.WebClient.PollSettings, System.Threading.CancellationToken)')<br/>using the provided [PollSettings](Gripper_WebClient_PollSettings 'Gripper.WebClient.PollSettings') and [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken') |
| [ReloadAsync(PollSettings, CancellationToken)](Gripper_WebClient_IWebClient_ReloadAsync(Gripper_WebClient_PollSettings_System_Threading_CancellationToken) 'Gripper.WebClient.IWebClient.ReloadAsync(Gripper.WebClient.PollSettings, System.Threading.CancellationToken)') | Reloads the browser window and awaits the resulting [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') of [WaitUntilFramesLoadedAsync(PollSettings, CancellationToken)](Gripper_WebClient_IWebClient_WaitUntilFramesLoadedAsync(Gripper_WebClient_PollSettings_System_Threading_CancellationToken) 'Gripper.WebClient.IWebClient.WaitUntilFramesLoadedAsync(Gripper.WebClient.PollSettings, System.Threading.CancellationToken)')<br/>using the provided [PollSettings](Gripper_WebClient_PollSettings 'Gripper.WebClient.PollSettings') and [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken') |
| [WaitUntilFramesLoadedAsync(PollSettings, CancellationToken)](Gripper_WebClient_IWebClient_WaitUntilFramesLoadedAsync(Gripper_WebClient_PollSettings_System_Threading_CancellationToken) 'Gripper.WebClient.IWebClient.WaitUntilFramesLoadedAsync(Gripper.WebClient.PollSettings, System.Threading.CancellationToken)') | Blocks until either<br/><br/>1. All of the following has happened:<br/><br/>1.1. No frame has been added to the frame tree for one [PeriodMs](Gripper_WebClient_PollSettings_PeriodMs 'Gripper.WebClient.PollSettings.PeriodMs') period,<br/><br/>1.2. All frames in the frame tree have received the Page.frameNavigated and Page.frameLoaded events,<br/><br/>1.3. One [PeriodMs](Gripper_WebClient_PollSettings_PeriodMs 'Gripper.WebClient.PollSettings.PeriodMs') period has elapsed since the last Page.frameNavigated or Page.frameLoaded event,<br/><br/>or<br/><br/>2. [TimeoutMs](Gripper_WebClient_PollSettings_TimeoutMs 'Gripper.WebClient.PollSettings.TimeoutMs') has elapsed.<br/><br/>or<br/><br/>3. Task has been cancelled.<br/> |

| Events | |
| :--- | :--- |
| [WebClientEvent](Gripper_WebClient_IWebClient_WebClientEvent 'Gripper.WebClient.IWebClient.WebClientEvent') | An aggregate event handler for events from all CDP domains, all targets.<br/> |
