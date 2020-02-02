using Microsoft.JSInterop;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorAspnetHostedOidcJS.Shared
{
   public class UserManagerInteropStrategy : IUserManagerInteropStrategy
    {
        private readonly IJSRuntime JSRuntime;
        public UserManagerInteropStrategy(IJSRuntime jSRuntime) {
            JSRuntime = jSRuntime;
        }
		public void Initialize(Action completed) =>
			new Timer(async state =>
			{
				DateTime hardFailTime = DateTime.UtcNow.AddSeconds(1);
				bool success = false;
				Exception lastError = null;
				while (!success)
				{
					// Try up to 2 times immediately as the 2nd attempt often works
					for (int attempt = 0; attempt < 2; attempt++)
					{
						try
						{
							success = await JSRuntime.InvokeAsync<bool>("tryInitializeUserManager");
							if (success)
								break;
						}
						catch (NullReferenceException)
						{
							// NullReferenceException means we are pre-rendering a Blazor v3.0 server-side
							// Blazor app, so do not initialise any JavaScript.
							return;
						}
						catch (InvalidOperationException)
						{
							// InvalidOperationException means we are pre-rendering a Blazor v3.1+ server-side
							// Blazor app, so do not initialise any JavaScript.
							return;
						}
						catch (JSException err)
						{
							// JSException means JSInterop is available but something went wrong when
							// executing the script. We will try for up to 1 second before giving up.
							lastError = err;
						}
						catch (Exception err)
						{
							throw new Exception("User Manager initialization error", err);
						}
					}
					// If not successful then pause before retrying
					if (!success)
					{
						// If we have run out of time, throw an exception
						if (DateTime.UtcNow >= hardFailTime)
							throw new Exception("User Manager initialization error", lastError);

						await Task.Yield();
					}
				}
				completed();
			}, null, 0, 0);
	}
}
