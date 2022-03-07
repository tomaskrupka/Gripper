#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient')
## IWebClient.ExecuteCdpCommandAsync(string, JToken) Method
Tunnels a CDP command directly to the CDP client endpoint.   
```csharp
System.Threading.Tasks.Task<Newtonsoft.Json.Linq.JToken> ExecuteCdpCommandAsync(string commandName, Newtonsoft.Json.Linq.JToken commandParams);
```
#### Parameters
<a name='Gripper_WebClient_IWebClient_ExecuteCdpCommandAsync(string_Newtonsoft_Json_Linq_JToken)_commandName'></a>
`commandName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
Name of the command, e.g. 'Page.navigate'. [https://chromedevtools.github.io/devtools-protocol/](https://chromedevtools.github.io/devtools-protocol/ 'https://chromedevtools.github.io/devtools-protocol/')
  
<a name='Gripper_WebClient_IWebClient_ExecuteCdpCommandAsync(string_Newtonsoft_Json_Linq_JToken)_commandParams'></a>
`commandParams` [Newtonsoft.Json.Linq.JToken](https://docs.microsoft.com/en-us/dotnet/api/Newtonsoft.Json.Linq.JToken 'Newtonsoft.Json.Linq.JToken')  
  
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Newtonsoft.Json.Linq.JToken](https://docs.microsoft.com/en-us/dotnet/api/Newtonsoft.Json.Linq.JToken 'Newtonsoft.Json.Linq.JToken')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task') that represents the command execution.
### Remarks
Implementations should execute the command literally (no validation), and pass the result unmodified.  
