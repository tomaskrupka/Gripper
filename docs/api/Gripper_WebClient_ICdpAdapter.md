#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient')
## ICdpAdapter Interface
Dependency inversion for BaristaLabs.chrome-dev-tools. Creates the ChromeSession for existing CDP client WS endpoint, then manages its lifetime. Tunnels the incoming CDP events and handles execution of CDP calls.   
```csharp
internal interface ICdpAdapter
```

Derived  
&#8627; [CdpAdapter](Gripper_WebClient_CdpAdapter 'Gripper.WebClient.CdpAdapter')  

| Properties | |
| :--- | :--- |
| [ChromeSession](Gripper_WebClient_ICdpAdapter_ChromeSession 'Gripper.WebClient.ICdpAdapter.ChromeSession') | Gets reference to the [ChromeSession](Gripper_WebClient_ICdpAdapter_ChromeSession 'Gripper.WebClient.ICdpAdapter.ChromeSession') singleton.<br/> |

| Methods | |
| :--- | :--- |
| [BindAsync(IBrowserManager)](Gripper_WebClient_ICdpAdapter_BindAsync(Gripper_WebClient_IBrowserManager) 'Gripper.WebClient.ICdpAdapter.BindAsync(Gripper.WebClient.IBrowserManager)') | Binds the instance to the websocket endpoint of a running instance of an [IBrowserManager](Gripper_WebClient_IBrowserManager 'Gripper.WebClient.IBrowserManager').<br/> |

| Events | |
| :--- | :--- |
| [WebClientEvent](Gripper_WebClient_ICdpAdapter_WebClientEvent 'Gripper.WebClient.ICdpAdapter.WebClientEvent') | Enables subscription to any CDP event.<br/> |
