using LocalPolicyRevised.Interop;
using LocalPolicyRevised.Resources;
using System;
using System.Text;

namespace LocalPolicyRevised
{
    /// <summary>
    /// Represents a Group Policy Object in Active Directory.
    /// </summary>
    public class ActiveDirectoryGroupPolicyObject : GroupPolicyObject
    {
        /// <summary>
        /// Creates a new Group Policy Object in Active Directory.
        /// </summary>
        /// <param name="activeDirectoryPath">
        /// The Active Directory path where the GPO will be created.
        /// </param>
        /// <param name="displayName">
        /// The display name of the GPO.
        /// </param>
        /// <param name="settings">
        /// The settings to apply to the GPO.
        /// </param>
        /// <returns>
        /// A new <see cref="ActiveDirectoryGroupPolicyObject"/> instance.
        /// </returns>
        public static ActiveDirectoryGroupPolicyObject Create(
            string activeDirectoryPath, string displayName, GroupPolicyObjectSettings settings = null)
        {
            settings = new GroupPolicyObjectSettings();
            var instance = Helpers.GetGroupPolicyObjectInstance();
            Helpers.TryCatch(
                () => instance.New(activeDirectoryPath, displayName, settings.GetFlagValue()),
                MessageResources.CreateGpoAtActiveDirectoryFailed,
                activeDirectoryPath, displayName);
            return new ActiveDirectoryGroupPolicyObject(instance);
        }

        /// <summary>
        /// Opens an existing Group Policy Object in Active Directory.
        /// </summary>
        /// <param name="activeDirectoryPath">
        /// The Active Directory path where the GPO is located.
        /// </param>
        /// <param name="settings">
        /// The settings to apply to the GPO.
        /// </param>
        /// <param name="thisGuid">
        /// The unique identifier of this application.
        /// </param>
        public ActiveDirectoryGroupPolicyObject(string activeDirectoryPath, GroupPolicyObjectSettings settings = default, Guid? thisGuid = default)
            : base(thisGuid)
    	{
    		var activeDirectoryGroupPolicyObject = this;
    		settings = new GroupPolicyObjectSettings();
            Helpers.TryCatch(
                () => activeDirectoryGroupPolicyObject.Instance.OpenDSGPO(activeDirectoryPath, settings.GetFlagValue()),
                MessageResources.OpenActiveDirectoryGpoFailed, activeDirectoryPath);
    	}

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveDirectoryGroupPolicyObject"/> class.
        /// </summary>
        /// <param name="instance">
        /// The <see cref="IGroupPolicyObject"/> instance to wrap.
        /// </param>
        /// <param name="thisGuid">
        /// The unique identifier of this application.
        /// </param>
    	protected ActiveDirectoryGroupPolicyObject(IGroupPolicyObject instance, Guid? thisGuid = default)
            : base(instance, thisGuid)
    	{ }

        /// <summary>
        /// Gets the GUID name of the GPO.
        /// </summary>
        public Guid GuidName => new Guid(UniqueName);

        /// <summary>
        /// Gets the Active Directory path to the GPO.
        /// </summary>
        /// <param name="section">
        /// The Group Policy section.
        /// </param>
        /// <returns>
        /// The Active Directory path to the GPO.
        /// </returns>
    	public override string GetRegistryKeyPathTo(GroupPolicySection section)
    	{
    		var sb = new StringBuilder(1024);
            Helpers.TryCatch(
                () => Instance.GetDSPath((uint)section, sb, 1024),
                MessageResources.GetRegistryKeyPathToFailed, Enum.GetName(typeof(GroupPolicySection), section));
    		return sb.ToString();
    	}
    }
}
