using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using CashFlowAnalyzer.Client.FinancialData;
using CashFlowAnalyzer.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddHttpClient<ITransactionProvider, ClientTransactionProvider>(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Services
builder.Services.AddScoped<IAccountService, AccountService>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CashFlowAnalyzer.ServerAPI"));

builder.Services.AddApiAuthorization(options =>
{
    options.AuthenticationPaths.LogInPath = "security/login";
	options.AuthenticationPaths.RegisterPath = "security/register";
});

await builder.Build().RunAsync();
