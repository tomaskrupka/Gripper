### [Gripper.WebClient](Gripper_WebClient.md 'Gripper.WebClient').[IElement](Gripper_WebClient_IElement.md 'Gripper.WebClient.IElement')
## IElement.SendKeysAsync(string, TimeSpan) Method
Sends an array of keystrokes to the browser while keeping the element focused.  
```csharp
System.Threading.Tasks.Task SendKeysAsync(string keys, System.TimeSpan delayBetweenStrokes);
```
#### Parameters
<a name='Gripper_WebClient_IElement_SendKeysAsync(string_System_TimeSpan)_keys'></a>
`keys` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
Keys to send to the element.
  
<a name='Gripper_WebClient_IElement_SendKeysAsync(string_System_TimeSpan)_delayBetweenStrokes'></a>
`delayBetweenStrokes` [System.TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/System.TimeSpan 'System.TimeSpan')  
Delay to wait between strokes.
  
#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')  
A [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') representing the keystrokes operation.
