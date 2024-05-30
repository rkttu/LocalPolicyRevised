# LocalPolicy

[![NuGet Version](https://img.shields.io/nuget/v/LocalPolicy)](https://www.nuget.org/packages/LocalPolicy/) ![Build Status](https://github.com/rkttu/LocalPolicy/actions/workflows/dotnet.yml/badge.svg) [![GitHub Sponsors](https://img.shields.io/github/sponsors/rkttu)](https://github.com/sponsors/rkttu/)

With this library, you can view or edit computer policies, user policies, and manage GPOs associated with Active Directory on the local computer in Windows OS.

## Original Author Notice

The original author of the code is Martin Eden.

The original code was taken from a historical copy of the Web Archive at [https://bitbucket.org/MartinEden/local-policy].

This code repository contains the code for the library that was ported to .NET Standard 2.0 after restoring the code from the local-policy BitBucket repository mentioned in the [https://stackoverflow.com/a/22673417](https://stackoverflow.com/a/22673417) thread.

## Configuration

The GuidAttribute needs be added to the assembly that runs this library.

```csharp
[assembly: Guid("00000000-0000-0000-0000-000000000000")]")]
```

If you are unable to add a GuidAttribute to your assembly, you must provide a separate GUID value that is hard-coded into the thisGuid parameter, which is provided as an optional argument to the Get, Set, and Delete functions. The GUID value you provide here has no special meaning and is used only for auditing purposes.

## Usage

### Querying a Policy

```csharp
var section = GroupPolicySection.Machine;
var registryKeyPath = @"Software\Policies\Microsoft\Windows\HomeGroup";
var registryValueName = "DisableHomeGroup";

var disableHomeGroup = ComputerGroupPolicyObject.GetPolicyValue(
    section, registryKeyPath, registryValueName, thisGuid);

if (disableHomeGroup == null)
	Console.WriteLine("The policy is not set.");
else
	Console.WriteLine("The policy is set to: " + disableHomeGroup);
```

### Setting a Policy

```csharp
var section = GroupPolicySection.Machine;
var registryKeyPath = @"Software\Policies\Microsoft\Windows\HomeGroup";
var registryValueName = "DisableHomeGroup";

ComputerGroupPolicyObject.SetPolicySetting(
    section, registryKeyPath, registryValueName, 0);
```

### Deleting a Policy

```csharp
var section = GroupPolicySection.Machine;
var registryKeyPath = @"Software\Policies\Microsoft\Windows\HomeGroup";
var registryValueName = "DisableHomeGroup";

ComputerGroupPolicyObject.DeletePolicySetting(
    section, registryKeyPath, registryValueName);
```

## License

See [LICENSE](./LICENSE) file for more information.
