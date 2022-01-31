## Gripper.WebClient Namespace

| Classes | |
| :--- | :--- |
| [ChildProcessTracker](Gripper_WebClient_ChildProcessTracker 'Gripper.WebClient.ChildProcessTracker') | Allows processes to be automatically killed if this parent process unexpectedly quits.<br/>This feature requires Windows 8 or greater. On Windows 7, nothing is done. |
| [RdpEventArgs](Gripper_WebClient_RdpEventArgs 'Gripper.WebClient.RdpEventArgs') | The base event args container for any event from all CDP domains.<br/> |

| Structs | |
| :--- | :--- |
| [PollSettings](Gripper_WebClient_PollSettings 'Gripper.WebClient.PollSettings') | A data structure to provide settings to polling operations. Knows implicit conversions from/to (int periodMs, int timeoutMs).<br/> |

| Interfaces | |
| :--- | :--- |
| [IBrowserManager](Gripper_WebClient_IBrowserManager 'Gripper.WebClient.IBrowserManager') | Provides methods and members to launch, manage, access and destroy a web browser instance.<br/>At mininum, implementations must configure connecting to the CDP endpoint of the browser and pre-startup and post-destroy cleanup.<br/> |
| [IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext') | Provides a 1-1 mapping to a global execution context of an iFrame that contains a document node.<br/> |
| [IElement](Gripper_WebClient_IElement 'Gripper.WebClient.IElement') | Provides methods and members to interact with an HTML element on the page.<br/>[IElement](Gripper_WebClient_IElement 'Gripper.WebClient.IElement') can be mapped to a [Node](https://developer.mozilla.org/en-US/docs/Web/API/Node/nodeType 'https://developer.mozilla.org/en-US/docs/Web/API/Node/nodeType') of any type<br/>and throws a [System.NotSupportedException](https://docs.microsoft.com/en-us/dotnet/api/System.NotSupportedException 'System.NotSupportedException') for incompatible method calls on such nodes.<br/> |
| [IFrameInfo](Gripper_WebClient_IFrameInfo 'Gripper.WebClient.IFrameInfo') | Provides access to information about an iFrame on the page,<br/>as defined by the browser backend.<br/> |
| [IJsBuilder](Gripper_WebClient_IJsBuilder 'Gripper.WebClient.IJsBuilder') | Provides builders for repetitive JS expressions.<br/> |
| [IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient') | Enables interaction with the hooked web browser window.<br/> |

| Enums | |
| :--- | :--- |
| [TargetAttachmentMode](Gripper_WebClient_TargetAttachmentMode 'Gripper.WebClient.TargetAttachmentMode') | Configures response to the [ Chromium bug 924937 ](https://bugs.chromium.org/p/chromium/issues/detail?id=924937#c13 'https://bugs.chromium.org/p/chromium/issues/detail?id=924937#c13') <br/>which affects how targets (iFrames) are attached.<br/> |
