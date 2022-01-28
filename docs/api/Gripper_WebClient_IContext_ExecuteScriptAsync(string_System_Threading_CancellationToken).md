### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext')
## IContext.ExecuteScriptAsync(string, CancellationToken) Method
Executes a script in the global context.  
```csharp
System.Threading.Tasks.Task<Newtonsoft.Json.Linq.JToken> ExecuteScriptAsync(string script, System.Threading.CancellationToken cancellationToken);
```
#### Parameters
<a name='Gripper_WebClient_IContext_ExecuteScriptAsync(string_System_Threading_CancellationToken)_script'></a>
`script` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
The script to execute.
  
<a name='Gripper_WebClient_IContext_ExecuteScriptAsync(string_System_Threading_CancellationToken)_cancellationToken'></a>
`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')  
Token to cancel the [System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task').
  
#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Newtonsoft.Json.Linq.JToken](https://docs.microsoft.com/en-us/dotnet/api/Newtonsoft.Json.Linq.JToken 'Newtonsoft.Json.Linq.JToken')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A RemoteObject mapped to a [Newtonsoft.Json.Linq.JToken](https://docs.microsoft.com/en-us/dotnet/api/Newtonsoft.Json.Linq.JToken 'Newtonsoft.Json.Linq.JToken') that represents the result of the operation.
