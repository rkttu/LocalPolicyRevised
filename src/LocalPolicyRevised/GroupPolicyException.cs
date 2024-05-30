using System;

namespace LocalPolicyRevised
{
    /// <summary>
    /// Represents an exception that occurred while working with Group Policy.
    /// </summary>
    public class GroupPolicyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupPolicyException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception.
        /// </param>
    	public GroupPolicyException(string message, Exception innerException)
    		: base(message, innerException)
    	{
    	}

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupPolicyException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public GroupPolicyException(string message)
            : base(message)
        {
        }
    }
}
