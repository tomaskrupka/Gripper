[← Home](index.md) [Source →](https://github.com/tomaskrupka/Gripper) [About →](about.md)

- [Packaging](#packaging)
- [Deployment](#deployment)
- [Installation](#installation)
  - [Windows](#windows)
  - [Linux, MacOS](#linux-macos)
- [Runtime](#runtime)
  - [Hosting](#hosting)
  - [Configuration](#configuration)
  - [Launching the browser](#launching-the-browser)

# Packaging

The Gripper interface is packed as [Gripper.WebClient](https://www.nuget.org/packages/Gripper.WebClient/) on Nuget.
The standard implementation is packed separately as [Gripper.WebClient.Cdtr](https://www.nuget.org/packages/Gripper.WebClient.Cdtr/).

Please refer to the nuget.org package pages for version info, release notes and the ``.nupkg``s.

Both packages are built against ``.NET 6.0``.
In the case of ``Gripper.WebClient`` it's the only dependency.
The ``Gripper.WebClient.Cdtr`` implementation package also depends on the [chrome-dev-tools-runtime](https://www.nuget.org/packages/BaristaLabs.ChromeDevTools.Runtime/). 

Note:
This is messy and will be fixed soon™ in a backward compatible way. When the implementation is free from dependencies, it will be packed into ``Gripper.WebClient`` together with the interface.

Note: 
This way, client services using Gripper can depend on its API without having to pull any dependency.
Only the DI host then needs to provide the implementation the dependency.

# Deployment

To deploy Gripper, you'll need an installation of [.NET 6 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).
The basic installation—without any Desktop or ASP.NET Core components—will work just fine.

Gripper also works published as a [single-file app](https://docs.microsoft.com/en-us/dotnet/core/deploying/single-file) for deployment onto a target without a .NET Runtime installation.

# Installation

How to add Gripper to your .NET project.

## Windows

Note: The current version of the package is a prerelease.
When installing from the CLI, don't forget the `--prerelease` flag.
In the Visual Studio Nuget package manager, toggle the Prerelease checkbox.

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

# Runtime

## Hosting

Let's create a mockup service for performing web-scraping operations. 

It can be built and packed separately depending just on the Gripper API package.

```cs
using Gripper.WebClient;

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

The host then loads and injects the standard Gripper implementation.

```cs
using Gripper.WebClient.Extensions;
using GripperService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
            .AddGripper()
            .AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
```

## Configuration

## Launching the browser