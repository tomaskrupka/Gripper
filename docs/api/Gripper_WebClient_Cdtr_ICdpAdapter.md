#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient.Cdtr](Gripper_WebClient_Cdtr 'Gripper.WebClient.Cdtr')
## ICdpAdapter Interface
Dependency inversion for BaristaLabs.chrome-dev-tools. Creates the ChromeSession for existing CDP client WS endpoint, then manages its lifetime. Tunnels the incoming CDP events and handles execution of CDP calls.   
```csharp
internal interface ICdpAdapter
```

Derived  
&#8627; [CdpAdapter](Gripper_WebClient_Cdtr_CdpAdapter 'Gripper.WebClient.Cdtr.CdpAdapter')  

| Properties | |
| :--- | :--- |
| [ChromeSession](Gripper_WebClient_Cdtr_ICdpAdapter_ChromeSession 'Gripper.WebClient.Cdtr.ICdpAdapter.ChromeSession') | Gets reference to the [ChromeSession](Gripper_WebClient_Cdtr_ICdpAdapter_ChromeSession 'Gripper.WebClient.Cdtr.ICdpAdapter.ChromeSession') singleton.<br/> |

| Methods | |
| :--- | :--- |
| [BindAsync(IBrowserManager)](Gripper_WebClient_Cdtr_ICdpAdapter_BindAsync(Gripper_WebClient_IBrowserManager) 'Gripper.WebClient.Cdtr.ICdpAdapter.BindAsync(Gripper.WebClient.IBrowserManager)') | Binds the instance to the websocket endpoint of a running instance of an [IBrowserManager](Gripper_WebClient_IBrowserManager 'Gripper.WebClient.IBrowserManager').<br/> |

| Events | |
| :--- | :--- |
| [WebClientEvent](Gripper_WebClient_Cdtr_ICdpAdapter_WebClientEvent 'Gripper.WebClient.Cdtr.ICdpAdapter.WebClientEvent') | Enables subscription to any CDP event.<br/> |
