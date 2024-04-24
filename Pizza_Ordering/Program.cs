using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Pizza_Ordering.web;
using Pizza_Ordering.web.Services;
using Pizza_Ordering.web.Services.Contracts;
using Pizza_Ordering.web.Services.Cpntracts;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7113/") });
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
		builder.Services.AddBlazorDialog();

        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        await builder.Build().RunAsync();
     
 
    }
}