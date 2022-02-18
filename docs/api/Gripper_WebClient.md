#### [Gripper.WebClient](index 'index')
## Gripper.WebClient Namespace

| Classes | |
| :--- | :--- |
| [CdpAdapter](Gripper_WebClient_CdpAdapter 'Gripper.WebClient.CdpAdapter') |  |
| [ChromeClient](Gripper_WebClient_ChromeClient 'Gripper.WebClient.ChromeClient') | Instantiate as transient.<br/> |
| [Context](Gripper_WebClient_Context 'Gripper.WebClient.Context') |  |
| [ContextFactory](Gripper_WebClient_ContextFactory 'Gripper.WebClient.ContextFactory') |  |
| [WebClientSettings](Gripper_WebClient_WebClientSettings 'Gripper.WebClient.WebClientSettings') |  |

| Structs | |
| :--- | :--- |
| [PollSettings](Gripper_WebClient_PollSettings 'Gripper.WebClient.PollSettings') | A data structure to provide settings to polling operations. Knows implicit conversions from/to (int periodMs, int timeoutMs).<br/> |

| Interfaces | |
| :--- | :--- |
| [IBrowserManager](Gripper_WebClient_IBrowserManager 'Gripper.WebClient.IBrowserManager') | Provides methods and members to launch, manage, access and destroy a web browser instance.<br/>At mininum, implementations must configure connecting to the CDP endpoint of the browser and pre-startup and post-destroy cleanup.<br/> |
| [ICdpAdapter](Gripper_WebClient_ICdpAdapter 'Gripper.WebClient.ICdpAdapter') | Dependency inversion for BaristaLabs.chrome-dev-tools. Creates the ChromeSession for existing CDP client WS endpoint, then manages its lifetime. Tunnels the incoming CDP events and handles execution of CDP calls. <br/> |
| [IContext](Gripper_WebClient_IContext 'Gripper.WebClient.IContext') | Represents a [browsing context](https://developer.mozilla.org/en-US/docs/Glossary/Browsing_context 'https://developer.mozilla.org/en-US/docs/Glossary/Browsing_context'), the environment in which the browser displays a Document.<br/> |
| [IContextFactory](Gripper_WebClient_IContextFactory 'Gripper.WebClient.IContextFactory') | Facilitates 1-1-1 mapping between iFrame-Execution context-IContext.<br/> |
| [IContextManager](Gripper_WebClient_IContextManager 'Gripper.WebClient.IContextManager') | Maintains and provides interface to the up-to-date data structure representing the execution contexts on the page.<br/> |
| [IElement](Gripper_WebClient_IElement 'Gripper.WebClient.IElement') | Provides methods and members to interact with an HTML element on the page.<br/> |
| [IElementFactory](Gripper_WebClient_IElementFactory 'Gripper.WebClient.IElementFactory') | Dependency inversion vehicle for [Gripper.WebClient.Element](https://docs.microsoft.com/en-us/dotnet/api/Gripper.WebClient.Element 'Gripper.WebClient.Element') implementations.<br/> |
| [IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient') | Enables interaction with the hooked web browser window.<br/> |

| Enums | |
| :--- | :--- |
| [BrowserCleanupSettings](Gripper_WebClient_BrowserCleanupSettings 'Gripper.WebClient.BrowserCleanupSettings') | Provides options for pre-startup cleanup of the user profile directory.<br/> |
| [SpecialKey](Gripper_WebClient_SpecialKey 'Gripper.WebClient.SpecialKey') | Represents a special key as defined [here.](https://github.com/chromedp/chromedp/blob/fd310a9b849a/kb/keys.go 'https://github.com/chromedp/chromedp/blob/fd310a9b849a/kb/keys.go') |
| [TargetAttachmentMode](Gripper_WebClient_TargetAttachmentMode 'Gripper.WebClient.TargetAttachmentMode') | Configures response to the [ Chromium bug 924937 ](https://bugs.chromium.org/p/chromium/issues/detail?id=924937#c13 'https://bugs.chromium.org/p/chromium/issues/detail?id=924937#c13') <br/>which affects how targets (iFrames) are attached.<br/> |
