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
    // https://github.com/dotnet/AspNetCore.Docs/blob/b1e1428d5899fda009f65e2c4e41dac6a60df7b6/aspnetcore/blazor/security/webassembly/additional-scenarios.md#customize-app-routes
    // doesn't work because of auto interactivity, see https://github.com/dotnet/aspnetcore/issues/58811
    options.AuthenticationPaths.LogInPath = "security/login";
	options.AuthenticationPaths.RegisterPath = "security/register";
});

await builder.Build().RunAsync();
