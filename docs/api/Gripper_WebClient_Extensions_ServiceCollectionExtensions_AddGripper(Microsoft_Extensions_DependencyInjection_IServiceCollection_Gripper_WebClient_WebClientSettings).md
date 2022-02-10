#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient.Extensions](Gripper_WebClient_Extensions 'Gripper.WebClient.Extensions').[ServiceCollectionExtensions](Gripper_WebClient_Extensions_ServiceCollectionExtensions 'Gripper.WebClient.Extensions.ServiceCollectionExtensions')
## ServiceCollectionExtensions.AddGripper(IServiceCollection, WebClientSettings) Method
Adds Gripper to service collection.  
```csharp
public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddGripper(this Microsoft.Extensions.DependencyInjection.IServiceCollection services, Gripper.WebClient.WebClientSettings settings);
```
#### Parameters
<a name='Gripper_WebClient_Extensions_ServiceCollectionExtensions_AddGripper(Microsoft_Extensions_DependencyInjection_IServiceCollection_Gripper_WebClient_WebClientSettings)_services'></a>
`services` [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')  
Service collection to load gripper into.
  
<a name='Gripper_WebClient_Extensions_ServiceCollectionExtensions_AddGripper(Microsoft_Extensions_DependencyInjection_IServiceCollection_Gripper_WebClient_WebClientSettings)_settings'></a>
`settings` [WebClientSettings](Gripper_WebClient_WebClientSettings 'Gripper.WebClient.WebClientSettings')  
A [WebClientSettings](Gripper_WebClient_WebClientSettings 'Gripper.WebClient.WebClientSettings') that is passed to the [IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient') as configuration object.
  
#### Returns
[Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')  
The same [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') instance so that additional calls can be chained.
