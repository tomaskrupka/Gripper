#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient')
## IElement Interface
Provides methods and members to interact with an HTML element on the page.  
[IElement](Gripper_WebClient_IElement 'Gripper.WebClient.IElement') can be mapped to a [Node](https://developer.mozilla.org/en-US/docs/Web/API/Node/nodeType 'https://developer.mozilla.org/en-US/docs/Web/API/Node/nodeType') of any type  
and throws a [System.NotSupportedException](https://docs.microsoft.com/en-us/dotnet/api/System.NotSupportedException 'System.NotSupportedException') for incompatible method calls on such nodes.  
```csharp
public interface IElement
```

| Methods | |
| :--- | :--- |
| [ClickAsync()](Gripper_WebClient_IElement_ClickAsync() 'Gripper.WebClient.IElement.ClickAsync()') | Dispatches a click event onto the area of the element.<br/> |
| [FocusAsync()](Gripper_WebClient_IElement_FocusAsync() 'Gripper.WebClient.IElement.FocusAsync()') | Focuses the element.<br/> |
| [GetTextContentAsync()](Gripper_WebClient_IElement_GetTextContentAsync() 'Gripper.WebClient.IElement.GetTextContentAsync()') | Returns the textContent of the element.<br/> |
| [SendKeysAsync(string, TimeSpan)](Gripper_WebClient_IElement_SendKeysAsync(string_System_TimeSpan) 'Gripper.WebClient.IElement.SendKeysAsync(string, System.TimeSpan)') | Sends an array of keystrokes to the browser while keeping the element focused.<br/> |
| [SendSpecialKeyAsync(SpecialKey)](Gripper_WebClient_IElement_SendSpecialKeyAsync(Gripper_WebClient_SpecialKey) 'Gripper.WebClient.IElement.SendSpecialKeyAsync(Gripper.WebClient.SpecialKey)') | Sends a [Gripper.WebClient.SpecialKey](https://docs.microsoft.com/en-us/dotnet/api/Gripper.WebClient.SpecialKey 'Gripper.WebClient.SpecialKey') to the browser right after focusing the element.<br/> |
| [WaitUntilInteractable(CancellationToken)](Gripper_WebClient_IElement_WaitUntilInteractable(System_Threading_CancellationToken) 'Gripper.WebClient.IElement.WaitUntilInteractable(System.Threading.CancellationToken)') | Blocks until the element can receive keyboard or mouse inputs.<br/> |
