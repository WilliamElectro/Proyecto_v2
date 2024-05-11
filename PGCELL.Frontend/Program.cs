using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PGCELL.Frontend;
using PGCELL.Frontend.AuthenticationProviders;
using PGCELL.Frontend.Repositories;
using PGCELL.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// TODO: To change enviroments
//var urlBack = "{Your URL}";
var urlBack = "https://backendpgcell.azurewebsites.net/";

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(urlBack) });
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSweetAlert2();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddBlazoredModal();
builder.Services.AddMudServices();

await builder.Build().RunAsync();