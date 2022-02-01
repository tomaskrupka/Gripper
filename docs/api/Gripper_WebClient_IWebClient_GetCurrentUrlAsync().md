#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient')
## IWebClient.GetCurrentUrlAsync() Method
Gets the current URL of the top page.  
```csharp
System.Threading.Tasks.Task<string?> GetCurrentUrlAsync();
```
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
Current URL of the top page or null if the window is not displaying a response to any Http request   
            (e.g. if last request was 4XX or if the top frame is in a state between the Page.frameStartedLoading and Page.frameNavigated events.
