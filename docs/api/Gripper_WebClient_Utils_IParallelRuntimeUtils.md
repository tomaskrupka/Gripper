#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient.Utils](Gripper_WebClient_Utils 'Gripper.WebClient.Utils')
## IParallelRuntimeUtils Interface
State container for running multiple [IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient') instances.  
```csharp
public interface IParallelRuntimeUtils
```
### Remarks
Instantiate as a singleton.  
This is a work-around the limitations of passing custom settings to transient [IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient') instances.  

| Methods | |
| :--- | :--- |
| [GetFreshTcpPort()](Gripper_WebClient_Utils_IParallelRuntimeUtils_GetFreshTcpPort() 'Gripper.WebClient.Utils.IParallelRuntimeUtils.GetFreshTcpPort()') | Gets unused TCP port to use as a CDP listener of a new browser instance.<br/> |
