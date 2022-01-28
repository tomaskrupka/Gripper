### [Gripper.WebClient](Gripper_WebClient.md 'Gripper.WebClient')
## IContext Interface
Provides a 1-1 mapping to a global execution context of an iFrame that contains a document node.  
```csharp
public interface IContext
```
### Remarks
Implementations must ensure that if there are multiple contexts for an iFrame (e.g. a background worker thread and the main context),  
the main context is referenced.   

| Properties | |
| :--- | :--- |
| [FrameInfo](Gripper_WebClient_IContext_FrameInfo.md 'Gripper.WebClient.IContext.FrameInfo') | Information about the Frame mapped to the context.<br/> |
| [Id](Gripper_WebClient_IContext_Id.md 'Gripper.WebClient.IContext.Id') | Id of the context as defined by the browser backend.<br/> |

| Methods | |
| :--- | :--- |
| [ExecuteScriptAsync(string, CancellationToken)](Gripper_WebClient_IContext_ExecuteScriptAsync(string_System_Threading_CancellationToken).md 'Gripper.WebClient.IContext.ExecuteScriptAsync(string, System.Threading.CancellationToken)') | Executes a script in the global context.<br/> |
| [FindElementByCssSelectorAsync(string)](Gripper_WebClient_IContext_FindElementByCssSelectorAsync(string).md 'Gripper.WebClient.IContext.FindElementByCssSelectorAsync(string)') | Finds an element by a CSS selector on the document node of the Frame.<br/> |
| [WaitUntilElementPresentAsync(string, PollSettings, CancellationToken)](Gripper_WebClient_IContext_WaitUntilElementPresentAsync(string_Gripper_WebClient_PollSettings_System_Threading_CancellationToken).md 'Gripper.WebClient.IContext.WaitUntilElementPresentAsync(string, Gripper.WebClient.PollSettings, System.Threading.CancellationToken)') | Polls for an element defined by a specified CSS selector.<br/> |
