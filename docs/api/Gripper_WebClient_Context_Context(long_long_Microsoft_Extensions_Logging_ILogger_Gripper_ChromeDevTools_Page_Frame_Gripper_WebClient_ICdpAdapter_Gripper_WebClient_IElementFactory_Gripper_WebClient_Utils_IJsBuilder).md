#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient](Gripper_WebClient 'Gripper.WebClient').[Context](Gripper_WebClient_Context 'Gripper.WebClient.Context')
## Context.Context(long, long, ILogger, Frame, ICdpAdapter, IElementFactory, IJsBuilder) Constructor
Ctor.   
```csharp
internal Context(long contextId, long documentBackendNodeId, Microsoft.Extensions.Logging.ILogger logger, Gripper.ChromeDevTools.Page.Frame frame, Gripper.WebClient.ICdpAdapter cdpAdapter, Gripper.WebClient.IElementFactory cdtrElementFactory, Gripper.WebClient.Utils.IJsBuilder jsBuilder);
```
#### Parameters
<a name='Gripper_WebClient_Context_Context(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_ChromeDevTools_Page_Frame_Gripper_WebClient_ICdpAdapter_Gripper_WebClient_IElementFactory_Gripper_WebClient_Utils_IJsBuilder)_contextId'></a>
`contextId` [System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')  
  
<a name='Gripper_WebClient_Context_Context(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_ChromeDevTools_Page_Frame_Gripper_WebClient_ICdpAdapter_Gripper_WebClient_IElementFactory_Gripper_WebClient_Utils_IJsBuilder)_documentBackendNodeId'></a>
`documentBackendNodeId` [System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')  
  
<a name='Gripper_WebClient_Context_Context(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_ChromeDevTools_Page_Frame_Gripper_WebClient_ICdpAdapter_Gripper_WebClient_IElementFactory_Gripper_WebClient_Utils_IJsBuilder)_logger'></a>
`logger` [Microsoft.Extensions.Logging.ILogger](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger 'Microsoft.Extensions.Logging.ILogger')  
  
<a name='Gripper_WebClient_Context_Context(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_ChromeDevTools_Page_Frame_Gripper_WebClient_ICdpAdapter_Gripper_WebClient_IElementFactory_Gripper_WebClient_Utils_IJsBuilder)_frame'></a>
`frame` [Gripper.ChromeDevTools.Page.Frame](https://docs.microsoft.com/en-us/dotnet/api/Gripper.ChromeDevTools.Page.Frame 'Gripper.ChromeDevTools.Page.Frame')  
  
<a name='Gripper_WebClient_Context_Context(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_ChromeDevTools_Page_Frame_Gripper_WebClient_ICdpAdapter_Gripper_WebClient_IElementFactory_Gripper_WebClient_Utils_IJsBuilder)_cdpAdapter'></a>
`cdpAdapter` [ICdpAdapter](Gripper_WebClient_ICdpAdapter 'Gripper.WebClient.ICdpAdapter')  
  
<a name='Gripper_WebClient_Context_Context(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_ChromeDevTools_Page_Frame_Gripper_WebClient_ICdpAdapter_Gripper_WebClient_IElementFactory_Gripper_WebClient_Utils_IJsBuilder)_cdtrElementFactory'></a>
`cdtrElementFactory` [IElementFactory](Gripper_WebClient_IElementFactory 'Gripper.WebClient.IElementFactory')  
  
<a name='Gripper_WebClient_Context_Context(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_ChromeDevTools_Page_Frame_Gripper_WebClient_ICdpAdapter_Gripper_WebClient_IElementFactory_Gripper_WebClient_Utils_IJsBuilder)_jsBuilder'></a>
`jsBuilder` [IJsBuilder](Gripper_WebClient_Utils_IJsBuilder 'Gripper.WebClient.Utils.IJsBuilder')  
  
### Remarks
Frame must be loaded when calling this ctor.  
