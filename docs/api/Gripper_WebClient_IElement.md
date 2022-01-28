### [Gripper.WebClient](Gripper_WebClient.md 'Gripper.WebClient')
## IElement Interface
Provides methods and members to interact with an HTML element on the page.  
[IElement](Gripper_WebClient_IElement.md 'Gripper.WebClient.IElement') can be mapped to a [Node](https://developer.mozilla.org/en-US/docs/Web/API/Node/nodeType 'https://developer.mozilla.org/en-US/docs/Web/API/Node/nodeType') of any type  
and throws a [System.NotSupportedException](https://docs.microsoft.com/en-us/dotnet/api/System.NotSupportedException 'System.NotSupportedException') for incompatible method calls on such nodes.  
```csharp
public interface IElement
```

| Methods | |
| :--- | :--- |
| [ClickAsync()](Gripper_WebClient_IElement_ClickAsync().md 'Gripper.WebClient.IElement.ClickAsync()') | Dispatches a click event onto the area of the element.<br/> |
| [FocusAsync()](Gripper_WebClient_IElement_FocusAsync().md 'Gripper.WebClient.IElement.FocusAsync()') | Focuses the element.<br/> |
| [GetTextContentAsyhc()](Gripper_WebClient_IElement_GetTextContentAsyhc().md 'Gripper.WebClient.IElement.GetTextContentAsyhc()') | Returns the textContent of the element.<br/> |
| [SendKeysAsync(string, TimeSpan)](Gripper_WebClient_IElement_SendKeysAsync(string_System_TimeSpan).md 'Gripper.WebClient.IElement.SendKeysAsync(string, System.TimeSpan)') | Sends an array of keystrokes to the browser while keeping the element focused.<br/> |
| [SendSpecialKeyAsync(SpecialKey)](Gripper_WebClient_IElement_SendSpecialKeyAsync(Gripper_WebClient_SpecialKey).md 'Gripper.WebClient.IElement.SendSpecialKeyAsync(Gripper.WebClient.SpecialKey)') | Sends a [Gripper.WebClient.SpecialKey](https://docs.microsoft.com/en-us/dotnet/api/Gripper.WebClient.SpecialKey 'Gripper.WebClient.SpecialKey') to the browser right after focusing the element.<br/> |
| [WaitUntilInteractable(CancellationToken)](Gripper_WebClient_IElement_WaitUntilInteractable(System_Threading_CancellationToken).md 'Gripper.WebClient.IElement.WaitUntilInteractable(System.Threading.CancellationToken)') | Blocks until the element can receive keyboard or mouse inputs.<br/> |
