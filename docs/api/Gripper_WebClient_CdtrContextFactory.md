#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient')
## CdtrContextFactory Class
```csharp
internal class CdtrContextFactory :
Gripper.WebClient.IContextFactory
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; CdtrContextFactory  

Implements [IContextFactory](Gripper_WebClient_IContextFactory 'Gripper.WebClient.IContextFactory')  

| Methods | |
| :--- | :--- |
| [GetDocumentBackendNodeIdAsync(long)](Gripper_WebClient_CdtrContextFactory_GetDocumentBackendNodeIdAsync(long) 'Gripper.WebClient.CdtrContextFactory.GetDocumentBackendNodeIdAsync(long)') | Get root node backend node id. If context has no DOM (e.g. background worker thread) or nothing has been loaded yet, returns null.<br/> |