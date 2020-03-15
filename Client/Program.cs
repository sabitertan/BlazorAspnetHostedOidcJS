using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorAspnetHostedOidcJS.Shared;

namespace BlazorAspnetHostedOidcJS.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddScoped<IUserManagerInteropStrategy, UserManagerInteropStrategy>();
            builder.Services.AddScoped<UserManagerInterop>();
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
