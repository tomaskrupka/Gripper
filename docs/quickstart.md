[← Home](index.md)

Gripper is built against `.NET 6.0`, which is also its only dependency.
To deploy Gripper, you'll need an installation of [.NET 6 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).
The basic installation—without any Desktop or ASP.NET Core components—will work just fine.

Gripper also works published as a [single-file app](https://docs.microsoft.com/en-us/dotnet/core/deploying/single-file) for deployment onto a target without a .NET Runtime installation.

# Installation

## Windows

You can find the latest package version, release notes and the `.nupkg` itself on Nuget as [Gripper.WebClient](https://www.nuget.org/packages/Gripper.WebClient/).

<div class="panel panel-warning">
The current version of the package is a prerelease, don't forget the `--prerelease` flag.
{: .alert .alert-warning}
</div>

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

# Runtime

Gripper is meant to be used as a service.

# Launching the browser