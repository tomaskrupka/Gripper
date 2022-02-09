#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient.Settings](Gripper_WebClient_Settings 'Gripper.WebClient.Settings')
## WebClientSettingsGenerator Class
Provides predefined default settings and methods for creating new ones.  
```csharp
public static class WebClientSettingsGenerator
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; WebClientSettingsGenerator  

| Fields | |
| :--- | :--- |
| [DefaultSettings](Gripper_WebClient_Settings_WebClientSettingsGenerator_DefaultSettings 'Gripper.WebClient.Settings.WebClientSettingsGenerator.DefaultSettings') | Out-of-the-box settings that can be used on Windows to bootstrap a singleton Gripper. Launches own Chrome automatically. Uses http://localhost:9000 for Chrome-Gripper connection.<br/> |

| Methods | |
| :--- | :--- |
| [GetForTransient()](Gripper_WebClient_Settings_WebClientSettingsGenerator_GetForTransient() 'Gripper.WebClient.Settings.WebClientSettingsGenerator.GetForTransient()') |  |
| [GetForUnitTesting()](Gripper_WebClient_Settings_WebClientSettingsGenerator_GetForUnitTesting() 'Gripper.WebClient.Settings.WebClientSettingsGenerator.GetForUnitTesting()') | Produces Lambda that overwrites the settings with fresh UserDataDir and Port. For bootstrapping of independent unit tests.<br/> |
