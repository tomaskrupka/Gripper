#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient.Cdtr](Gripper_WebClient_Cdtr 'Gripper.WebClient.Cdtr').[CdtrContextFactory](Gripper_WebClient_Cdtr_CdtrContextFactory 'Gripper.WebClient.Cdtr.CdtrContextFactory')
## CdtrContextFactory.GetDocumentBackendNodeIdAsync(long) Method
Get root node backend node id. If context has no DOM (e.g. background worker thread) or nothing has been loaded yet, returns null.  
```csharp
private System.Threading.Tasks.Task<System.Nullable<long>> GetDocumentBackendNodeIdAsync(long contextId);
```
#### Parameters
<a name='Gripper_WebClient_Cdtr_CdtrContextFactory_GetDocumentBackendNodeIdAsync(long)_contextId'></a>
`contextId` [System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')  
Id of context to try and find the root node id for.
  
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
#document backend node id or null if irrelevant.
