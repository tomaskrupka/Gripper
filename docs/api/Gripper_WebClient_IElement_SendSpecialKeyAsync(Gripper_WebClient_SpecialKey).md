### [Gripper.WebClient](Gripper_WebClient.md 'Gripper.WebClient').[IElement](Gripper_WebClient_IElement.md 'Gripper.WebClient.IElement')
## IElement.SendSpecialKeyAsync(SpecialKey) Method
Sends a [Gripper.WebClient.SpecialKey](https://docs.microsoft.com/en-us/dotnet/api/Gripper.WebClient.SpecialKey 'Gripper.WebClient.SpecialKey') to the browser right after focusing the element.  
```csharp
System.Threading.Tasks.Task SendSpecialKeyAsync(Gripper.WebClient.SpecialKey key);
```
#### Parameters
<a name='Gripper_WebClient_IElement_SendSpecialKeyAsync(Gripper_WebClient_SpecialKey)_key'></a>
`key` [Gripper.WebClient.SpecialKey](https://docs.microsoft.com/en-us/dotnet/api/Gripper.WebClient.SpecialKey 'Gripper.WebClient.SpecialKey')  
The [Gripper.WebClient.SpecialKey](https://docs.microsoft.com/en-us/dotnet/api/Gripper.WebClient.SpecialKey 'Gripper.WebClient.SpecialKey') to send to the element.
  
#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')  
A [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') representing the keystroke operation.
