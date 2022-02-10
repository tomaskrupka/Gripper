#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient.Cdtr](Gripper_WebClient_Cdtr 'Gripper.WebClient.Cdtr').[IContextManager](Gripper_WebClient_Cdtr_IContextManager 'Gripper.WebClient.Cdtr.IContextManager')
## IContextManager.GetContextDescriptionsAsync() Method
Gets a collection of active execution contexts.  
```csharp
internal System.Threading.Tasks.Task<System.Collections.Generic.ICollection<BaristaLabs.ChromeDevTools.Runtime.Runtime.ExecutionContextDescription>> GetContextDescriptionsAsync();
```
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Collections.Generic.ICollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[BaristaLabs.ChromeDevTools.Runtime.Runtime.ExecutionContextDescription](https://docs.microsoft.com/en-us/dotnet/api/BaristaLabs.ChromeDevTools.Runtime.Runtime.ExecutionContextDescription 'BaristaLabs.ChromeDevTools.Runtime.Runtime.ExecutionContextDescription')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A snapshot of the data structure tracking the CDP execution contexts.
