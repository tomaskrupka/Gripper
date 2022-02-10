#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient')
## IContext Interface
Represents a [browsing context](https://developer.mozilla.org/en-US/docs/Glossary/Browsing_context 'https://developer.mozilla.org/en-US/docs/Glossary/Browsing_context'), the environment in which the browser displays a Document.  
```csharp
public interface IContext
```

Derived  
&#8627; [CdtrContext](Gripper_WebClient_Cdtr_CdtrContext 'Gripper.WebClient.Cdtr.CdtrContext')  
### Remarks
For an iFrame with more than one execution contexts (e.g. background workers), this interface will bind the one that maps the DOM.  

| Properties | |
| :--- | :--- |
| [FrameInfo](Gripper_WebClient_IContext_FrameInfo 'Gripper.WebClient.IContext.FrameInfo') | Information about the Frame containing the context.<br/> |
| [Id](Gripper_WebClient_IContext_Id 'Gripper.WebClient.IContext.Id') | Id of the context as defined by the browser backend.<br/> |

| Methods | |
| :--- | :--- |
| [ExecuteScriptAsync(string, CancellationToken)](Gripper_WebClient_IContext_ExecuteScriptAsync(string_System_Threading_CancellationToken) 'Gripper.WebClient.IContext.ExecuteScriptAsync(string, System.Threading.CancellationToken)') | Executes a script in the global context.<br/> |
| [FindElementByCssSelectorAsync(string)](Gripper_WebClient_IContext_FindElementByCssSelectorAsync(string) 'Gripper.WebClient.IContext.FindElementByCssSelectorAsync(string)') | Finds an element by a CSS selector on the document node of the Frame.<br/> |
| [WaitUntilElementPresentAsync(string, PollSettings, CancellationToken)](Gripper_WebClient_IContext_WaitUntilElementPresentAsync(string_Gripper_WebClient_PollSettings_System_Threading_CancellationToken) 'Gripper.WebClient.IContext.WaitUntilElementPresentAsync(string, Gripper.WebClient.PollSettings, System.Threading.CancellationToken)') | Polls for an element defined by a specified CSS selector.<br/> |
