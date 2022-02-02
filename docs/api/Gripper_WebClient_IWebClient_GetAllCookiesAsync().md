#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient')
## IWebClient.GetAllCookiesAsync() Method
Gets all cookies stored by the browser.  
```csharp
System.Threading.Tasks.Task<System.Net.CookieContainer> GetAllCookiesAsync();
```
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.Net.CookieContainer](https://docs.microsoft.com/en-us/dotnet/api/System.Net.CookieContainer 'System.Net.CookieContainer')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [System.Net.CookieContainer](https://docs.microsoft.com/en-us/dotnet/api/System.Net.CookieContainer 'System.Net.CookieContainer') that can be plugged as-is into an [System.Net.Http.HttpClientHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Net.Http.HttpClientHandler 'System.Net.Http.HttpClientHandler')
