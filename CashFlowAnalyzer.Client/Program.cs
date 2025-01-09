using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using CashFlowAnalyzer.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddHttpClient<IFinancialDataService, FinancialDataService>(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<SpreadsheetReader>();
builder.Services.AddScoped<IFinancialDataService, FinancialDataService>();
builder.Services.AddScoped<AuthenticationInfoProvider>();
builder.Services.AddBlazorBootstrap();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CashFlowAnalyzer.ServerAPI"));

await builder.Build().RunAsync();
