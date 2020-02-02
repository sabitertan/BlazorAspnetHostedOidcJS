using System;

namespace BlazorAspnetHostedOidcJS.Shared
{
	/// <summary>
	/// A strategy pattern for initialising an User Manager Interop
	/// </summary>
	public interface IUserManagerInteropStrategy
	{
		/// <summary>
		/// Initialises the User Manager Interop
		/// </summary>
		/// <param name="completed">The action to call back once the User Manager Interop is ready to be initialised</param>
		void Initialize(Action completed);
	}
}