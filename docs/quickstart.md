[← Home](index.md)

# Installation

You can find the latest package version, release notes and the `.nupkg` itself on Nuget as [Gripper.WebClient](https://www.nuget.org/packages/Gripper.WebClient/).

The current version of the package is a prerelease, don't forget the `--prerelease` flag.
{: .alert .alert-info}

Creating a project and adding a package reference to Gripper looks as follows.

```powershell
PS C:\Users\tomas\source\demos> dotnet new console -f net6.0 -n GripperDemo
The template "Console App" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on C:\Users\tomas\source\demos\GripperDemo\GripperDemo.csproj...
  Determining projects to restore...
  Restored C:\Users\tomas\source\demos\GripperDemo\GripperDemo.csproj (in 77 ms).
Restore succeeded.

PS C:\Users\tomas\source\demos> cd GripperDemo
PS C:\Users\tomas\source\demos\GripperDemo> dotnet add package Gripper.WebClient --prerelease
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
PS C:\Users\tomas\source\demos\GripperDemo> dotnet run
Hello, World!
PS C:\Users\tomas\source\demos\GripperDemo>
```