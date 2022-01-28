### [Gripper.WebClient](Gripper_WebClient.md 'Gripper.WebClient')
## TargetAttachmentMode Enum
Configures response to the [ Chromium bug 924937 ](https://bugs.chromium.org/p/chromium/issues/detail?id=924937#c13 'https://bugs.chromium.org/p/chromium/issues/detail?id=924937#c13')   
which affects how targets (iFrames) are attached.  
```csharp
public enum TargetAttachmentMode

```
#### Fields
<a name='Gripper_WebClient_TargetAttachmentMode_Auto'></a>
`Auto` 1  
The [IBrowserManager](Gripper_WebClient_IBrowserManager.md 'Gripper.WebClient.IBrowserManager') will set --disable-features flags on the features that cause this bug when launching the browser.  
This will probably disable the IsolateOrigins and the site-per-process features.  
The runtime manager will then set up automatic target binding at the startup.  
  
<a name='Gripper_WebClient_TargetAttachmentMode_Default'></a>
`Default` 0  
Let the implementation decide.  
  
<a name='Gripper_WebClient_TargetAttachmentMode_SeekAndAttach'></a>
`SeekAndAttach` 2  
Launch browser without disabling any features.  
Try to actively discover and bind targets.  
  
