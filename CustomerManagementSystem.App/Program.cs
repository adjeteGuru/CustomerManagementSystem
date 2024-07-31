using AutoMapper;
using CustomerManagementSystem.App;
using CustomerManagementSystem.Core.Profiles;
using CustomerManagementSystem.Core.Providers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new CustomerProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7048/") });
builder.Services.AddScoped<ICustomersDataProvider, CustomersDataProvider>();
await builder.Build().RunAsync();
