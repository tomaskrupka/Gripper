#### [Gripper.WebClient](index 'index')
### [Gripper.WebClient.Extensions](Gripper_WebClient_Extensions 'Gripper.WebClient.Extensions').[ServiceCollectionExtensions](Gripper_WebClient_Extensions_ServiceCollectionExtensions 'Gripper.WebClient.Extensions.ServiceCollectionExtensions')
## ServiceCollectionExtensions.AddGripper(IServiceCollection, Action&lt;WebClientSettings&gt;) Method
Adds Gripper to service collection.  
```csharp
public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddGripper(this Microsoft.Extensions.DependencyInjection.IServiceCollection services, System.Action<Gripper.WebClient.WebClientSettings> configureOptions);
```
#### Parameters
<a name='Gripper_WebClient_Extensions_ServiceCollectionExtensions_AddGripper(Microsoft_Extensions_DependencyInjection_IServiceCollection_System_Action_Gripper_WebClient_WebClientSettings_)_services'></a>
`services` [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')  
Service collection to load gripper into.
  
<a name='Gripper_WebClient_Extensions_ServiceCollectionExtensions_AddGripper(Microsoft_Extensions_DependencyInjection_IServiceCollection_System_Action_Gripper_WebClient_WebClientSettings_)_configureOptions'></a>
`configureOptions` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[WebClientSettings](Gripper_WebClient_WebClientSettings 'Gripper.WebClient.WebClientSettings')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')  
Lambda to run on a [WebClientSettings](Gripper_WebClient_WebClientSettings 'Gripper.WebClient.WebClientSettings') instance before it is passed to the [IWebClient](Gripper_WebClient_IWebClient 'Gripper.WebClient.IWebClient') as configuration object.
  
#### Returns
[Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')  
The same [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') instance so that additional calls can be chained.
