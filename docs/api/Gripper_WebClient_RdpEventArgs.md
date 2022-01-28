### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient')
## RdpEventArgs Class
The base event args container for any event from all CDP domains.  
```csharp
public class RdpEventArgs : System.EventArgs
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [System.EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs 'System.EventArgs') &#129106; RdpEventArgs  

| Properties | |
| :--- | :--- |
| [DomainName](Gripper_WebClient_RdpEventArgs_DomainName 'Gripper.WebClient.RdpEventArgs.DomainName') | The namespace of the domain where the event originated, e.g. "Network". The first character is always uppercase.<br/> |
| [EventData](Gripper_WebClient_RdpEventArgs_EventData 'Gripper.WebClient.RdpEventArgs.EventData') | Getter for the event data container.<br/> |
| [EventName](Gripper_WebClient_RdpEventArgs_EventName 'Gripper.WebClient.RdpEventArgs.EventName') | The name of the event, e.g. "requestWillBeSent". The first character is always lowercase.<br/> |
| [HasEventData](Gripper_WebClient_RdpEventArgs_HasEventData 'Gripper.WebClient.RdpEventArgs.HasEventData') | Defines whether this event carries some additional data.<br/> |
