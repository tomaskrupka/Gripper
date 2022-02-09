#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient.Settings](Gripper_WebClient_Settings 'Gripper.WebClient.Settings').[WebClientSettingsGenerator](Gripper_WebClient_Settings_WebClientSettingsGenerator 'Gripper.WebClient.Settings.WebClientSettingsGenerator')
## WebClientSettingsGenerator.GetForUnitTesting() Method
Produces Lambda that overwrites the settings with fresh UserDataDir and Port. For bootstrapping of independent unit tests.  
```csharp
public static System.Action<Gripper.WebClient.WebClientSettings> GetForUnitTesting();
```
#### Returns
[System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[WebClientSettings](Gripper_WebClient_WebClientSettings 'Gripper.WebClient.WebClientSettings')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
