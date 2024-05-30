using Microsoft.Win32;

namespace LocalPolicy
{
    /// <summary>
    /// Represents a Group Policy value.
    /// </summary>
    public sealed class GroupPolicyValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupPolicyValue"/> class.
        /// </summary>
        /// <param name="groupPolicySection">
        /// The Group Policy section.
        /// </param>
        /// <param name="registryKeyPath">
        /// The registry key path.
        /// </param>
        /// <param name="registryValueName">
        /// The registry value name.
        /// </param>
        /// <param name="registryValue">
        /// The registry value.
        /// </param>
        /// <param name="registryValueKind">
        /// The registry value kind.
        /// </param>
        public GroupPolicyValue(
            GroupPolicySection groupPolicySection,
            string registryKeyPath,
            string registryValueName,
            object registryValue,
            RegistryValueKind registryValueKind)
        {
            _groupPolicySection = groupPolicySection;
            _registryKeyPath = registryKeyPath;
            _registryValueName = registryValueName;
            _registryValue = registryValue;
            _registryValueKind = registryValueKind;
        }

        private readonly GroupPolicySection _groupPolicySection;
        private readonly string _registryKeyPath;
        private readonly string _registryValueName;
        private readonly object _registryValue;
        private readonly RegistryValueKind _registryValueKind;

        /// <summary>
        /// Gets the Group Policy section.
        /// </summary>
        public GroupPolicySection GroupPolicySection => _groupPolicySection;

        /// <summary>
        /// Gets the registry key path.
        /// </summary>
        public string RegistryKeyPath => _registryKeyPath;

        /// <summary>
        /// Gets the registry value name.
        /// </summary>
        public string RegistryValueName => _registryValueName;

        /// <summary>
        /// Gets the registry value.
        /// </summary>
        public object RegistryValue => _registryValue;

        /// <summary>
        /// Gets the registry value kind.
        /// </summary>
        public RegistryValueKind RegistryValueKind => _registryValueKind;

        /// <summary>
        /// Returns a string representation of the Group Policy value.
        /// </summary>
        /// <returns>
        /// A string representation of the Group Policy value.
        /// </returns>
        public override string ToString()
            => $"{_groupPolicySection}\\{_registryKeyPath}!{_registryValueName} = {_registryValue} ({_registryValueKind})";
    }
}
