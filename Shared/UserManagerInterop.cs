using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace BlazorAspnetHostedOidcJS.Shared
{
   public class UserManagerInterop : IUserManagerInterop
    {
        private IUserManagerInteropStrategy UserManagerInteropStrategy;
        private bool HasActivatedUserManager;
        private readonly TaskCompletionSource<bool> InitializedCompletionSource = new TaskCompletionSource<bool>();
        public Task Initialized => InitializedCompletionSource.Task;
		private IJSRuntime _jsRuntime;
        public UserManagerInterop(IUserManagerInteropStrategy userManagerInteropStrategy, IJSRuntime jsRuntime) {
            UserManagerInteropStrategy = userManagerInteropStrategy;
			_jsRuntime = jsRuntime;
        }
		public RenderFragment Initialize()
		{
			if (HasActivatedUserManager)
				return builder => { };

			UserManagerInteropStrategy.Initialize(ActivateUserManager);
			return (RenderTreeBuilder renderer) =>
			{
				var scriptBuilder = new StringBuilder();
				scriptBuilder.AppendLine("if (window.canInitializeUserManagerInterop) {");
				{
					scriptBuilder.AppendLine("delete window.canInitializeUserManagerInterop;");
				}
				scriptBuilder.AppendLine("}");

				string script = scriptBuilder.ToString();
				renderer.OpenElement(1, "script");
				renderer.AddAttribute(2, "id", "initializeUserManagerInterop");
				renderer.AddMarkupContent(3, script);
				renderer.CloseElement();
			};
		}
		private void ActivateUserManager()
		{
			if (HasActivatedUserManager)
				return;

			HasActivatedUserManager = true;
			InitializedCompletionSource.SetResult(true);
		}
		public async Task Login() {
		
			var success = await _jsRuntime.InvokeAsync<string>("authenticationService.login");

		}
		public async Task<string> GetUser()
		{

			var success = await _jsRuntime.InvokeAsync<string>("authenticationService.getUser");
			return success;
		}

	}
}
