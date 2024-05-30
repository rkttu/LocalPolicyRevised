using LocalPolicy.Resources;
using Microsoft.Win32;
using System;
using System.Text;
using System.Threading;

namespace LocalPolicy
{
    /// <summary>
    /// Represents a Group Policy Object for the local computer.
    /// </summary>
    public class ComputerGroupPolicyObject : GroupPolicyObject
    {
        /// <summary>
        /// Gets a policy setting.
        /// </summary>
        /// <param name="section">
        /// The section of the GPO to get the policy setting from.
        /// </param>
        /// <param name="registryKeyPath">
        /// The path to the registry key of the policy setting.
        /// </param>
        /// <param name="registryValueName">
        /// The name of the registry value of the policy setting.
        /// </param>
        /// <param name="thisGuid">
        /// The unique identifier of this application. Set to <see langword="null"/> to use the executing assemblies GUID.
        /// </param>
        /// <returns>
        /// The value and value kind of the policy setting.
        /// </returns>
        public static GroupPolicyValue GetPolicySetting(GroupPolicySection section, string registryKeyPath, string registryValueName, Guid? thisGuid = default)
        {
            Exception exception = null;
            GroupPolicyValue result = null;

            var staThread = new Thread(() =>
            {
                try { result = Helpers.GetPolicySettingInternal(section, registryKeyPath, registryValueName, thisGuid); }
                catch (Exception ex) { exception = ex; }
            });

            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            if (exception != null)
                throw exception;

            return result;
        }

        /// <summary>
        /// Gets a policy setting.
        /// </summary>
        /// <param name="section">
        /// The section of the GPO to get the policy setting from.
        /// </param>
        /// <param name="registryKeyPath">
        /// The path to the registry key of the policy setting.
        /// </param>
        /// <param name="registryValueName">
        /// The name of the registry value of the policy setting.
        /// </param>
        /// <param name="thisGuid">
        /// The unique identifier of this application. Set to <see langword="null"/> to use the executing assemblies GUID.
        /// </param>
        /// <returns>
        /// The value of the policy setting.
        /// </returns>
        public static object GetPolicyValue(GroupPolicySection section, string registryKeyPath, string registryValueName, Guid? thisGuid = default)
            => GetPolicySetting(section, registryKeyPath, registryValueName, thisGuid).RegistryValue;

        /// <summary>
        /// Sets a policy setting.
        /// </summary>
        /// <param name="section">
        /// The section of the GPO to get the policy setting from.
        /// </param>
        /// <param name="registryKeyPath">
        /// The path to the registry key of the policy setting.
        /// </param>
        /// <param name="registryValueName">
        /// The name of the registry value of the policy setting.
        /// </param>
        /// <param name="newRegistryValue">
        /// The new value of the policy setting.
        /// </param>
        /// <param name="newRegistryValueKind">
        /// The new value kind of the policy setting. If you're not sure what value to specify, specify RegistryValueKind.Unknown or the default value of RegistryValueKind.
        /// </param>
        /// <param name="thisGuid">
        /// The unique identifier of this application. Set to <see langword="null"/> to use the executing assemblies GUID.
        /// </param>
        public static void SetPolicySetting(GroupPolicySection section, string registryKeyPath, string registryValueName, object newRegistryValue, RegistryValueKind newRegistryValueKind = default, Guid? thisGuid = default)
        {
            Exception exception = null;

            var staThread = new Thread(() =>
            {
                try { Helpers.SetPolicySettingInternal(section, registryKeyPath, registryValueName, newRegistryValue, newRegistryValueKind, thisGuid); }
                catch (Exception ex) { exception = ex; }
            });

            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            if (exception != null)
                throw exception;
        }

        /// <summary>
        /// Deletes a policy setting.
        /// </summary>
        /// <param name="section">
        /// The section of the GPO to delete the policy setting from.
        /// </param>
        /// <param name="registryKeyPath">
        /// The path to the registry key of the policy setting.
        /// </param>
        /// <param name="registryValueName">
        /// The name of the registry value of the policy setting.
        /// </param>
        /// <param name="thisGuid">
        /// The unique identifier of this application. Set to <see langword="null"/> to use the executing assemblies GUID.
        /// </param>
        public static void DeletePolicySetting(GroupPolicySection section, string registryKeyPath, string registryValueName, Guid? thisGuid = default)
        {
            Exception exception = null;

            var staThread = new Thread(() =>
            {
                try { Helpers.SetPolicySettingInternal(section, registryKeyPath, registryValueName, default, default, thisGuid); }
                catch (Exception ex) { exception = ex; }
            });

            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            if (exception != null)
                throw exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputerGroupPolicyObject"/> class.
        /// </summary>
        /// <param name="options">
        /// The settings to apply to the GPO.
        /// </param>
        /// <param name="thisGuid">
        /// The unique identifier of this application.
        /// </param>
        public ComputerGroupPolicyObject(GroupPolicyObjectSettings options = default, Guid? thisGuid = default)
            : base(thisGuid)
    	{
    		var computerGroupPolicyObject = this;
    		options = options ?? new GroupPolicyObjectSettings();
            Helpers.TryCatch(
                () => computerGroupPolicyObject.Instance.OpenLocalMachineGPO(options.GetFlagValue()),
                MessageResources.OpenLocalMachineGpoFailed);
    		_isLocal = true;
    	}

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputerGroupPolicyObject"/> class.
        /// </summary>
        /// <param name="computerName">
        /// The name of the computer to open the GPO on.
        /// </param>
        /// <param name="options">
        /// The settings to apply to the GPO.
        /// </param>
        /// <param name="thisGuid">
        /// The unique identifier of this application.
        /// </param>
    	public ComputerGroupPolicyObject(string computerName, GroupPolicyObjectSettings options = default, Guid? thisGuid = default)
            : base(thisGuid)
    	{
    		var computerGroupPolicyObject = this;
    		options = options ?? new GroupPolicyObjectSettings();
            Helpers.TryCatch(
                () => computerGroupPolicyObject.Instance.OpenRemoteMachineGPO(computerName, options.GetFlagValue()),
                MessageResources.OpenRemoteMachineGpoFailed, computerName);
    		_isLocal = false;
    	}

        private readonly bool _isLocal;

        /// <summary>
        /// Gets a value indicating whether the GPO is local.
        /// </summary>
        public bool IsLocal => _isLocal;

        /// <summary>
        /// Gets the name of the computer the GPO is on.
        /// </summary>
        public string ComputerName => UniqueName;

        /// <summary>
        /// Gets the path to a section of the GPO.
        /// </summary>
        /// <param name="section">
        /// The section to get the path to.
        /// </param>
        /// <returns>
        /// The path to the section.
        /// </returns>
        public override string GetRegistryKeyPathTo(GroupPolicySection section)
    	{
    		var sb = new StringBuilder(1024);
            Helpers.TryCatch(
                () => Instance.GetFileSysPath((uint)section, sb, 1024),
                MessageResources.GetRegistryKeyPathToFailed,
                Enum.GetName(typeof(GroupPolicySection), section));
    		return sb.ToString();
    	}
    }
}
