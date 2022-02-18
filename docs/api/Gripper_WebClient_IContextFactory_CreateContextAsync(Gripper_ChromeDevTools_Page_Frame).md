#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IContextFactory](Gripper_WebClient_IContextFactory 'Gripper.WebClient.IContextFactory')
## IContextFactory.CreateContextAsync(Frame) Method
Tries to find the DOM execution context of an iFrame and create an [IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext') representation of it.  
If an iFrame has more than one execution contexts, matches the one with access to the DOM.  
If an iFrame has no execution contexts, returns null.  
```csharp
System.Threading.Tasks.Task<Gripper.WebClient.IContext?> CreateContextAsync(Gripper.ChromeDevTools.Page.Frame frame);
```
#### Parameters
<a name='Gripper_WebClient_IContextFactory_CreateContextAsync(Gripper_ChromeDevTools_Page_Frame)_frame'></a>
`frame` [Gripper.ChromeDevTools.Page.Frame](https://docs.microsoft.com/en-us/dotnet/api/Gripper.ChromeDevTools.Page.Frame 'Gripper.ChromeDevTools.Page.Frame')  
Frame to find the execution context for.
  
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
Resulting [IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext') object, or null if no context was matched.
