namespace LocalPolicyRevised
{
	/// <summary>
	/// Represents the section of a Group Policy Object.
	/// </summary>
	public enum GroupPolicySection : int
	{
		/// <summary>
		/// The root section of the Group Policy Object.
		/// </summary>
		Root = default,

		/// <summary>
		/// The user section of the Group Policy Object.
		/// </summary>
		User,

		/// <summary>
		/// The machine section of the Group Policy Object.
		/// </summary>
		Machine
	}
}
