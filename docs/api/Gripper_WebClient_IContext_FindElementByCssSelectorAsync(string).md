#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext')
## IContext.FindElementByCssSelectorAsync(string) Method
Finds an element by a CSS selector on the document node of the Frame.  
```csharp
System.Threading.Tasks.Task<Gripper.WebClient.IElement?> FindElementByCssSelectorAsync(string cssSelector);
```
#### Parameters
<a name='Gripper_WebClient_IContext_FindElementByCssSelectorAsync(string)_cssSelector'></a>
`cssSelector` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
The CSS selector of the targeted element.
  
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[IElement](Gripper_WebClient_IElement 'Gripper.WebClient.IElement')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
The resulting [IElement](Gripper_WebClient_IElement 'Gripper.WebClient.IElement'), or null if no element was matched within the document of the iFrame.
### Remarks
This should not be implemented as a mapping of the DOM.querySelector call:  
[https://chromedevtools.github.io/devtools-protocol/tot/DOM/#method-querySelector](https://chromedevtools.github.io/devtools-protocol/tot/DOM/#method-querySelector 'https://chromedevtools.github.io/devtools-protocol/tot/DOM/#method-querySelector')  
which is unreliable as it only takes the NodeId (as opposed to the BackendNodeId) as a parameter.  
[https://github.com/ChromeDevTools/devtools-protocol/issues/72](https://github.com/ChromeDevTools/devtools-protocol/issues/72 'https://github.com/ChromeDevTools/devtools-protocol/issues/72')
