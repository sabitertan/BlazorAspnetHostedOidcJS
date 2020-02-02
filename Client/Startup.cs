using BlazorAspnetHostedOidcJS.Shared;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAspnetHostedOidcJS.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserManagerInteropStrategy, UserManagerInteropStrategy>();
            services.AddScoped<UserManagerInterop>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
