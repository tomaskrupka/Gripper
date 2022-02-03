#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IElement](Gripper_WebClient_IElement 'Gripper.WebClient.IElement')
## IElement.ClickAsync(int) Method
Dispatches a click event onto the area of the element.  
```csharp
System.Threading.Tasks.Task ClickAsync(int clickDurationMs);
```
#### Parameters
<a name='Gripper_WebClient_IElement_ClickAsync(int)_clickDurationMs'></a>
`clickDurationMs` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')  
Delay between the MouseDown and MouseUp events.
  
#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')  
A [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') representing the click operation
### Remarks
The implementations should take advantage of the   
[dispatch mouse event](https://chromedevtools.github.io/devtools-protocol/tot/Input/#method-dispatchMouseEvent 'https://chromedevtools.github.io/devtools-protocol/tot/Input/#method-dispatchMouseEvent').  
Do not use the DOM .click() or invoke the onclick() or other mouse events by evaluating a script.  
This is unreliable and easy to detect.  
