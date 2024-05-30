using LocalPolicy.Interop;
using LocalPolicy.Resources;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System;

namespace LocalPolicy
{
    /// <summary>
    /// Represents a Group Policy Object.
    /// </summary>
    public abstract class GroupPolicyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupPolicyObject"/> class.
        /// </summary>
        /// <param name="instance">
        /// The <see cref="IGroupPolicyObject"/> instance to wrap.
        /// </param>
        /// <param name="thisGuid">
        /// The unique identifier of this application.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="instance"/> is <see langword="null"/>.
        /// </exception>
        protected GroupPolicyObject(IGroupPolicyObject instance, Guid? thisGuid = default)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            _instance = instance;
            _thisGuid = thisGuid ?? Helpers.GetAssemblyGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupPolicyObject"/> class.
        /// </summary>
        /// <param name="thisGuid">
        /// The unique identifier of this application.
        /// </param>
        protected GroupPolicyObject(Guid? thisGuid = default)
            : this(Helpers.GetGroupPolicyObjectInstance(), thisGuid)
        { }

    	private IGroupPolicyObject _instance = null;
        private readonly Guid _thisGuid;

        /// <summary>
        /// Gets the <see cref="IGroupPolicyObject"/> instance.
        /// </summary>
        protected IGroupPolicyObject Instance => _instance;

        /// <summary>
        /// Gets the path to the Group Policy Object.
        /// </summary>
        public string Path => Helpers.GetString(_instance.GetPath, MessageResources.GetPathFailed);

        /// <summary>
        /// Gets the unique name of the Group Policy Object.
        /// </summary>
        public string UniqueName => Helpers.GetString(_instance.GetName, MessageResources.GetUniqueNameFailed);

        /// <summary>
        /// Gets the unique identifier of the this application.
        /// </summary>
        public Guid LocalGuid => _thisGuid;

        /// <summary>
        /// Gets or sets the display name of the Group Policy Object.
        /// </summary>
        public string DisplayName
    	{
    		get => Helpers.GetString(_instance.GetDisplayName, MessageResources.GetDisplayNameFailed);
            set
            {
                Helpers.TryCatch(
                    () => _instance.SetDisplayName(value),
                    MessageResources.SetDisplayNameFailed, value);
            }
    	}

        /// <summary>
        /// Gets or sets the options for the Group Policy Object.
        /// </summary>
    	public GroupPolicyObjectOptions Options
    	{
    		get
    		{
    			uint flag = 0u;
                Helpers.TryCatch(
                    () => _instance.GetOptions(out flag),
                    MessageResources.GetOptionsFailed);
    			return new GroupPolicyObjectOptions(flag);
    		}
            set
            {
                Helpers.TryCatch(
                    () => _instance.SetOptions(value.GetFlagValue(), 3u),
                    MessageResources.SetOptionsFailed);
            }
    	}

        /// <summary>
        /// Saves the Group Policy Object.
        /// </summary>
    	public void Save()
    	{
            Helpers.TryCatch(
                () => _instance.Save(machine: true, add: true, NativeMethods.RegistryExtension, LocalGuid),
                MessageResources.SaveMachineSettingsFailed);
            Helpers.TryCatch(
                () => _instance.Save(machine: false, add: true, NativeMethods.RegistryExtension, LocalGuid),
                MessageResources.SaveUserSettingsFailed);
    	}

        /// <summary>
        /// Deletes the Group Policy Object.
        /// </summary>
    	public void Delete()
    	{
            Helpers.TryCatch(
                () => _instance.Delete(),
                MessageResources.DeleteGpoFailed);
    		_instance = null;
    	}

        /// <summary>
        /// Gets the root registry key for the specified section.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
    	public RegistryKey GetRootRegistryKey(GroupPolicySection section)
    	{
    		var key = default(IntPtr);
            Helpers.TryCatch(
                () => _instance.GetRegistryKey((uint)section, out key),
                MessageResources.GetRootRegistryKeyFailed,
                Enum.GetName(typeof(GroupPolicySection), section));
    		return RegistryKey.FromHandle(new SafeRegistryHandle(key, ownsHandle: true));
    	}

        /// <summary>
        /// Gets the registry key path to a section of the Group Policy Object.
        /// </summary>
        /// <param name="section">
        /// The section to get the path to.
        /// </param>
        /// <returns>
        /// The registry key path to the section.
        /// </returns>
    	public abstract string GetRegistryKeyPathTo(GroupPolicySection section);
    }
}
