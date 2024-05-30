namespace LocalPolicy
{
    /// <summary>
    /// Represents the options for a Group Policy Object.
    /// </summary>
    public class GroupPolicyObjectOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupPolicyObjectOptions"/> struct.
        /// </summary>
        /// <param name="userEnabled">
        /// A value indicating whether the user settings are enabled.
        /// </param>
        /// <param name="machineEnabled">
        /// A value indicating whether the machine settings are enabled.
        /// </param>
        public GroupPolicyObjectOptions(bool userEnabled = true, bool machineEnabled = true)
        {
            _userEnabled = userEnabled;
            _machineEnabled = machineEnabled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupPolicyObjectOptions"/> struct.
        /// </summary>
        /// <param name="flag">
        /// The flag value to initialize the options with.
        /// </param>
        public GroupPolicyObjectOptions(uint flag)
        {
            _userEnabled = (flag & 1) == 0;
            _machineEnabled = (flag & 2) == 0;
        }

        private readonly bool _userEnabled;
    	private readonly bool _machineEnabled;

        /// <summary>
        /// Gets a value indicating whether the user settings are enabled.
        /// </summary>
        public bool UserEnabled => _userEnabled;

        /// <summary>
        /// Gets a value indicating whether the machine settings are enabled.
        /// </summary>
        public bool MachineEnabled => _machineEnabled;

        internal uint GetFlagValue()
        {
            var flag = 0u;

            if (!_userEnabled)
                flag |= 1u;
            
            if (!_machineEnabled)
                flag |= 2u;

            return flag;
        }
    }
}
