namespace LocalPolicy
{
	/// <summary>
	/// Represents the settings for a Group Policy Object.
	/// </summary>
    public class GroupPolicyObjectSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GroupPolicyObjectSettings"/> class.
		/// </summary>
		/// <param name="loadRegistryInfo">
		/// <see langword="true"/> to load the registry information for the GPO; otherwise, <see langword="false"/>.
		/// </param>
		/// <param name="readOnly">
		/// <see langword="true"/> to make the GPO read-only; otherwise, <see langword="false"/>.
		/// </param>
        public GroupPolicyObjectSettings(bool loadRegistryInfo = true, bool readOnly = false)
        {
            _loadRegistryInformation = loadRegistryInfo;
            _readOnly = readOnly;
        }
        
		private readonly bool _loadRegistryInformation;
		private readonly bool _readOnly;

		/// <summary>
		/// Gets a value indicating whether to load the registry information for the GPO.
		/// </summary>
		public bool LoadRegistryInformation => _loadRegistryInformation;

		/// <summary>
		/// Gets a value indicating whether the GPO is read-only.
		/// </summary>
		public bool ReadOnly => _readOnly;

		internal uint GetFlagValue()
		{
			var flag = 0u;

			if (_loadRegistryInformation)
				flag |= 1u;

			if (_readOnly)
				flag |= 2u;

			return flag;
		}
	}
}
