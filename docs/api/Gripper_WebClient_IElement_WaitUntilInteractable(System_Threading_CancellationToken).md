### [Gripper.WebClient](Gripper_WebClient.md 'Gripper.WebClient').[IElement](Gripper_WebClient_IElement.md 'Gripper.WebClient.IElement')
## IElement.WaitUntilInteractable(CancellationToken) Method
Blocks until the element can receive keyboard or mouse inputs.  
```csharp
System.Threading.Tasks.Task WaitUntilInteractable(System.Threading.CancellationToken cancellationToken);
```
#### Parameters
<a name='Gripper_WebClient_IElement_WaitUntilInteractable(System_Threading_CancellationToken)_cancellationToken'></a>
`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')  
A token to cancel the wait.
  
#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')  
A task representing the blocking operation.
