using LocalPolicyRevised.Resources;
using System;

namespace LocalPolicyRevised
{
	/// <summary>
	/// Represents an exception that occurs when the library requires a single-threaded apartment.
	/// </summary>
	public class RequiresSingleThreadedApartmentException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RequiresSingleThreadedApartmentException"/> class.
		/// </summary>
		public RequiresSingleThreadedApartmentException() 
			: base(MessageResources.RequiresSingleThreadedApartmentExceptionMessage)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="RequiresSingleThreadedApartmentException"/> class.
		/// </summary>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception.
		/// </param>
		public RequiresSingleThreadedApartmentException(Exception innerException)
			: base(MessageResources.RequiresSingleThreadedApartmentExceptionMessage, innerException)
		{ }
	}
}
