#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient.Cdtr](Gripper_WebClient_Cdtr 'Gripper.WebClient.Cdtr').[CdtrContext](Gripper_WebClient_Cdtr_CdtrContext 'Gripper.WebClient.Cdtr.CdtrContext')
## CdtrContext.CdtrContext(long, long, ILogger, IFrameInfo, ICdpAdapter, IElementFactory, IJsBuilder) Constructor
Ctor.   
```csharp
internal CdtrContext(long contextId, long documentBackendNodeId, Microsoft.Extensions.Logging.ILogger logger, Gripper.WebClient.IFrameInfo frameInfo, Gripper.WebClient.Cdtr.ICdpAdapter cdpAdapter, Gripper.WebClient.Cdtr.IElementFactory cdtrElementFactory, Gripper.WebClient.IJsBuilder jsBuilder);
```
#### Parameters
<a name='Gripper_WebClient_Cdtr_CdtrContext_CdtrContext(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_WebClient_IFrameInfo_Gripper_WebClient_Cdtr_ICdpAdapter_Gripper_WebClient_Cdtr_IElementFactory_Gripper_WebClient_IJsBuilder)_contextId'></a>
`contextId` [System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')  
  
<a name='Gripper_WebClient_Cdtr_CdtrContext_CdtrContext(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_WebClient_IFrameInfo_Gripper_WebClient_Cdtr_ICdpAdapter_Gripper_WebClient_Cdtr_IElementFactory_Gripper_WebClient_IJsBuilder)_documentBackendNodeId'></a>
`documentBackendNodeId` [System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')  
  
<a name='Gripper_WebClient_Cdtr_CdtrContext_CdtrContext(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_WebClient_IFrameInfo_Gripper_WebClient_Cdtr_ICdpAdapter_Gripper_WebClient_Cdtr_IElementFactory_Gripper_WebClient_IJsBuilder)_logger'></a>
`logger` [Microsoft.Extensions.Logging.ILogger](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger 'Microsoft.Extensions.Logging.ILogger')  
  
<a name='Gripper_WebClient_Cdtr_CdtrContext_CdtrContext(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_WebClient_IFrameInfo_Gripper_WebClient_Cdtr_ICdpAdapter_Gripper_WebClient_Cdtr_IElementFactory_Gripper_WebClient_IJsBuilder)_frameInfo'></a>
`frameInfo` [IFrameInfo](Gripper_WebClient_IFrameInfo 'Gripper.WebClient.IFrameInfo')  
  
<a name='Gripper_WebClient_Cdtr_CdtrContext_CdtrContext(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_WebClient_IFrameInfo_Gripper_WebClient_Cdtr_ICdpAdapter_Gripper_WebClient_Cdtr_IElementFactory_Gripper_WebClient_IJsBuilder)_cdpAdapter'></a>
`cdpAdapter` [ICdpAdapter](Gripper_WebClient_Cdtr_ICdpAdapter 'Gripper.WebClient.Cdtr.ICdpAdapter')  
  
<a name='Gripper_WebClient_Cdtr_CdtrContext_CdtrContext(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_WebClient_IFrameInfo_Gripper_WebClient_Cdtr_ICdpAdapter_Gripper_WebClient_Cdtr_IElementFactory_Gripper_WebClient_IJsBuilder)_cdtrElementFactory'></a>
`cdtrElementFactory` [IElementFactory](Gripper_WebClient_Cdtr_IElementFactory 'Gripper.WebClient.Cdtr.IElementFactory')  
  
<a name='Gripper_WebClient_Cdtr_CdtrContext_CdtrContext(long_long_Microsoft_Extensions_Logging_ILogger_Gripper_WebClient_IFrameInfo_Gripper_WebClient_Cdtr_ICdpAdapter_Gripper_WebClient_Cdtr_IElementFactory_Gripper_WebClient_IJsBuilder)_jsBuilder'></a>
`jsBuilder` [IJsBuilder](Gripper_WebClient_IJsBuilder 'Gripper.WebClient.IJsBuilder')  
  
### Remarks
Frame must be loaded when calling this ctor.  
