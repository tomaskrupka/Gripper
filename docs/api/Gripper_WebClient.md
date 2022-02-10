#### [Gripper.WebClient](index 'index')
## Gripper.WebClient Namespace

| Classes | |
| :--- | :--- |
| [RdpEventArgs](Gripper_WebClient_RdpEventArgs 'Gripper.WebClient.RdpEventArgs') | The base event args container for any event from all CDP domains.<br/> |
| [WebClientSettings](Gripper_WebClient_WebClientSettings 'Gripper.WebClient.WebClientSettings') |  |

| Structs | |
| :--- | :--- |
| [PollSettings](Gripper_WebClient_PollSettings 'Gripper.WebClient.PollSettings') | A data structure to provide settings to polling operations. Knows implicit conversions from/to (int periodMs, int timeoutMs).<br/> |

| Interfaces | |
| :--- | :--- |
| [IBrowserManager](Gripper_WebClient_IBrowserManager 'Gripper.WebClient.IBrowserManager') | Provides methods and members to launch, manage, access and destroy a web browser instance.<br/>At mininum, implementations must configure connecting to the CDP endpoint of the browser and pre-startup and post-destroy cleanup.<br/> |
| [IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext') | Represents a [browsing context](https://developer.mozilla.org/en-US/docs/Glossary/Browsing_context 'https://developer.mozilla.org/en-US/docs/Glossary/Browsing_context'), the environment in which the browser displays a Document.<br/> |
| [IContextFactory](Gripper_WebClient_IContextFactory 'Gripper.WebClient.IContextFactory') | Facilitates 1-1-1 mapping between iFrame-Execution context-IContext.<br/> |
| [IElement](Gripper_WebClient_IElement 'Gripper.WebClient.IElement') | Provides methods and members to interact with an HTML element on the page.<br/> |
| [IFrameInfo](Gripper_WebClient_IFrameInfo 'Gripper.WebClient.IFrameInfo') | Provides access to information about an iFrame on the page,<br/>as defined by the browser backend.<br/> |
| [IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient') | Enables interaction with the hooked web browser window.<br/> |

| Enums | |
| :--- | :--- |
| [TargetAttachmentMode](Gripper_WebClient_TargetAttachmentMode 'Gripper.WebClient.TargetAttachmentMode') | Configures response to the [ Chromium bug 924937 ](https://bugs.chromium.org/p/chromium/issues/detail?id=924937#c13 'https://bugs.chromium.org/p/chromium/issues/detail?id=924937#c13') <br/>which affects how targets (iFrames) are attached.<br/> |
