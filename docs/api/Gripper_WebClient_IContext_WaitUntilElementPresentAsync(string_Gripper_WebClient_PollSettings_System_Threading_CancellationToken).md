### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext')
## IContext.WaitUntilElementPresentAsync(string, PollSettings, CancellationToken) Method
Polls for an element defined by a specified CSS selector.  
```csharp
System.Threading.Tasks.Task<Gripper.WebClient.IElement?> WaitUntilElementPresentAsync(string cssSelector, Gripper.WebClient.PollSettings pollSettings, System.Threading.CancellationToken cancellationToken);
```
#### Parameters
<a name='Gripper_WebClient_IContext_WaitUntilElementPresentAsync(string_Gripper_WebClient_PollSettings_System_Threading_CancellationToken)_cssSelector'></a>
`cssSelector` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
The CSS selector of the targeted element.
  
<a name='Gripper_WebClient_IContext_WaitUntilElementPresentAsync(string_Gripper_WebClient_PollSettings_System_Threading_CancellationToken)_pollSettings'></a>
`pollSettings` [PollSettings](Gripper_WebClient_PollSettings 'Gripper.WebClient.PollSettings')  
Settings that control the polling for changes to the DOM.
  
<a name='Gripper_WebClient_IContext_WaitUntilElementPresentAsync(string_Gripper_WebClient_PollSettings_System_Threading_CancellationToken)_cancellationToken'></a>
`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')  
Token to cancel the [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task').
  
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[IElement](Gripper_WebClient_IElement 'Gripper.WebClient.IElement')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
The resulting [IElement](Gripper_WebClient_IElement 'Gripper.WebClient.IElement'),  
            or null if no element was matched within the document of the iFrame before the [TimeoutMs](Gripper_WebClient_PollSettings_TimeoutMs 'Gripper.WebClient.PollSettings.TimeoutMs') period elapsed.
