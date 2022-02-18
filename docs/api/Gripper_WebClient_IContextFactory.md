#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient')
## IContextFactory Interface
Facilitates 1-1-1 mapping between iFrame-Execution context-IContext.  
```csharp
public interface IContextFactory
```

Derived  
&#8627; [CdtrContextFactory](Gripper_WebClient_CdtrContextFactory 'Gripper.WebClient.CdtrContextFactory')  

| Methods | |
| :--- | :--- |
| [CreateContextAsync(Frame)](Gripper_WebClient_IContextFactory_CreateContextAsync(Gripper_ChromeDevTools_Page_Frame) 'Gripper.WebClient.IContextFactory.CreateContextAsync(Gripper.ChromeDevTools.Page.Frame)') | Tries to find the DOM execution context of an iFrame and create an [IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext') representation of it.<br/>If an iFrame has more than one execution contexts, matches the one with access to the DOM.<br/>If an iFrame has no execution contexts, returns null.<br/> |
