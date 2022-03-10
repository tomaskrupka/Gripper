[← Home](index.md) [About →](about.md) [Features→](features.md)

# Package

Gripper is packed as [Gripper.WebClient](https://www.nuget.org/packages/Gripper.WebClient/) on Nuget.
The standard implementation separated as `Gripper.WebClient.Cdtr` namespace within the same package.

The main [`Gripper.WebClient`](https://www.nuget.org/packages/Gripper.WebClient/) package depends on [`Gripper.ChromeDevTools`](https://www.nuget.org/packages/Gripper.ChromeDevTools/) for the CDP wrapper. Both packages are being kept up-to-date with the latest CDP API.


The dependencies of Gripper are:
- .NET6.0
- [chrome-dev-tools runtime](https://www.nuget.org/packages/BaristaLabs.ChromeDevTools.Runtime/)
- [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/)
- [Microsoft.Extensions.Logging](https://www.nuget.org/packages/Microsoft.Extensions.Logging/)

See the current list [here](https://www.nuget.org/packages/Gripper.WebClient/0.5.0-alpha#dependencies-tab).

# Deployment

To deploy Gripper, you'll need an installation of [.NET 6 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).
The basic installation—without any Desktop or ASP.NET Core components—will work just fine.

Gripper also works published as a [single-file app](https://docs.microsoft.com/en-us/dotnet/core/deploying/single-file) for deployment onto a target without a .NET Runtime installation.

# Installation

How to add Gripper to your .NET project.

## Windows

Note: The current version of the package is a prerelease. When installing from the CLI, don't forget the `--prerelease` flag. In the Visual Studio Nuget package manager, toggle the Prerelease checkbox.

Creating a console app, adding the `Gripper.WebClient` package reference and running it looks as follows.

```powershell
> dotnet new console -f net6.0 -n GripperDemo
The template "Console App" was created successfully.
Processing post-creation actions...
Running 'dotnet restore' on C:\Users\tomas\source\demos\GripperDemo\GripperDemo.csproj...
  Determining projects to restore...
  Restored C:\Users\tomas\source\demos\GripperDemo\GripperDemo.csproj (in 77 ms).
Restore succeeded.

> cd GripperDemo

> dotnet add package Gripper.WebClient --prerelease
  Determining projects to restore...
  Writing C:\Users\tomas\AppData\Local\Temp\tmp3442.tmp
info : Adding PackageReference for package 'Gripper.WebClient' into project 'C:\Users\tomas\source\demos\GripperDemo\GripperDemo.csproj'.
info :   CACHE https://api.nuget.org/v3/registration5-gz-semver2/gripper.webclient/index.json
info : Restoring packages for C:\Users\tomas\source\demos\GripperDemo\GripperDemo.csproj...
info : Package 'Gripper.WebClient' is compatible with all the specified frameworks in project 'C:\Users\tomas\source\demos\GripperDemo\GripperDemo.csproj'.
info : PackageReference for package 'Gripper.WebClient' version '0.4.7-alpha' added to file 'C:\Users\tomas\source\demos\GripperDemo\GripperDemo.csproj'.
info : Committing restore...
info : Generating MSBuild file C:\Users\tomas\source\demos\GripperDemo\obj\GripperDemo.csproj.nuget.g.props.
info : Writing assets file to disk. Path: C:\Users\tomas\source\demos\GripperDemo\obj\project.assets.json
log  : Restored C:\Users\tomas\source\demos\GripperDemo\GripperDemo.csproj (in 362 ms).

> dotnet run
Hello, World!
```

## Linux, MacOS

Soon™

# Hosting

Let's create a standalone service called ``GripperService`` that uses Gripper.
It can be built and packed separately depending just on the API package.

```csharp
// service lib; GripperService.cs

using Gripper.WebClient; // Gripper.WebClient.dll

namespace GripperService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IWebClient _webClient;

    public Worker(ILogger<Worker> logger, IWebClient webClient)
    {
        _logger = logger;
        _webClient = webClient;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await _webClient.ReloadAsync(PollSettings.FrameDetectionDefault, cancellationToken);
            var contexts = await _webClient.GetContextsAsync();

            _logger.LogInformation("Gripper found {contextsCount} contexts.", contexts.Count);

            await Task.Delay(TimeSpan.FromDays(1), cancellationToken);
        }
    }
}
```

The host then loads the ``GripperService`` and injects the standard Gripper implementation.

```csharp
// host app; main.cs

using Gripper.WebClient; // Gripper.WebClient.dll
using Gripper.WebClient.Cdtr; // Gripper.WebClient.Cdtr.dll
using GripperService; // GripperService lib

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
            .AddSingleton<IWebClient, CdtrChromeClient>()
            .AddOptions<WebClientSettings>().Configure(x =>
            {
                x.RemoteDebuggingPort = 9000;
                x.Proxy = new System.Net.WebProxy("localhost", 9001);
                // Many more explicit configuration options
            });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
```

# Configuration

In order to avoid hardcoding Gripper configuration explicitly,
you may prefer to take advantage of the built-in configuration extensions.

# Launching the browser