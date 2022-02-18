#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient')
## IWebClient.WebClientEvent Event
An aggregate event handler for events from all CDP domains, all targets.  
```csharp
event WebClientEvent;
```
#### Event Type
[System.EventHandler&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.EventHandler-1 'System.EventHandler`1')[Gripper.ChromeDevTools.IEvent](https://docs.microsoft.com/en-us/dotnet/api/Gripper.ChromeDevTools.IEvent 'Gripper.ChromeDevTools.IEvent')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.EventHandler-1 'System.EventHandler`1')
### Remarks
Note that target attachment is handled automatically, see [WebClientSettings](Gripper_WebClient_WebClientSettings 'Gripper.WebClient.WebClientSettings') for configuration.  
Call [ExecuteCdpCommandAsync(string, JToken)](Gripper_WebClient_IWebClient_ExecuteCdpCommandAsync(string_Newtonsoft_Json_Linq_JToken) 'Gripper.WebClient.IWebClient.ExecuteCdpCommandAsync(string, Newtonsoft.Json.Linq.JToken)') commands 'domain.Subscribe()' and 'domain.Unsubscribe()' to a specific CDP domain to start/stop receiving these events.  
